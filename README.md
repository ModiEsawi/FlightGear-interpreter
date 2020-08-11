# Advanced-Programming 1 (FlightGear)

The following project implements many paradigms and patterns such as client<->server, script interpreter that utlizes shunting-yard algorithm and follows the basic interpretation phases.

This project is used in order to pilot flights using FlightGear simulator by writing a custom script that automates it.

## Configuring FlightGear

First add `generic_small.xml` into FlightGear/data/protocl

Then open FlightGear -> Settings -> in Additional Settings add the following
```
--generic=socket,out,10,127.0.0.1,5400,tcp,generic_small
--telnet=socket,in,10,127.0.0.1,5402,tcp
```

## Download

Click on the download button on the top right corner and extract it.

Or just run the following
```
git clone https://github.com/ModiEsawi/flightSimulator
```

## Compiling and running
First compile using
```
g++ -std=c++14 *.cpp *.h  -Wall -Wextra -Wshadow -Wnon-virtual-dtor -pedantic -o a.out -pthread
```
Then run 
```
./a.out fly.txt
```
Or
```
./a.out fly_with_func.txt
```
Or use your own custom script.

Then launch FlightGear

## Documentation
In order to open a server for FlightGear
```
openDataServer(PORT)
```

In order to connect to FlightGear
```
connectControlClient("IP",PORT)
```

In order to bind outgoing params (used for controlling the plane)
```
var PARAM -> sim("/PATH/TO/PARAM")
```

In order to bind inbound params (used for determining current situation and react accordingly)
```
var PARAM <- sim("/PATH/TO/PARAM")
```

To print a message to console
```
Print("Hello!")
```

In order to set an outbound value
```
PARAM = 1
```

To copy variables (take current value of param and sets copy_param)
```
var COPY_PARAM = PARAM
```

Loops
```
while CONDITION {
    COMMANDS...
}
```

To sleep
```
Sleep(MSECONDS)
```

To create functions
```
FUNCTION_NAME(var PARAM){
    COMMANDS...
}

# and call it like this

FUNCTION_NAME(VALUE)
```

## Example code for flying the plane
```
openDataServer(5400)
connectControlClient("127.0.0.1",5402)
var warp -> sim("/sim/time/warp")
var magnetos -> sim("/controls/switches/magnetos")
var throttle -> sim("/controls/engines/current-engine/throttle")
var mixture -> sim("/controls/engines/current-engine/mixture")
var masterbat -> sim("/controls/switches/master-bat")
var masterlat -> sim("/controls/switches/master-alt")
var masteravionics -> sim("/controls/switches/master-avionics")
var brakeparking -> sim("/sim/model/c172p/brake-parking")
var primer -> sim("/controls/engines/engine/primer")
var starter -> sim("/controls/switches/starter")
var autostart -> sim("/engines/active-engine/auto-start")
var breaks -> sim("/controls/flight/speedbrake")
var heading <- sim("/instrumentation/heading-indicator/offset-deg")
var airspeed <- sim("/instrumentation/airspeed-indicator/indicated-speed-kt")
var roll <- sim("/instrumentation/attitude-indicator/indicated-roll-deg")
var pitch <- sim("/instrumentation/attitude-indicator/internal-pitch-deg")
var rudder -> sim("/controls/flight/rudder")
var aileron -> sim("/controls/flight/aileron")
var elevator -> sim("/controls/flight/elevator")
var alt <- sim("/instrumentation/altimeter/indicated-altitude-ft")
var rpm <- sim("/engines/engine/rpm")
Print("waiting 2 minutes for gui")
Sleep(120000)
Print("let's start")
warp = 32000
Sleep(1000)
magnetos = 3
throttle = 0.2
mixture = 0.949
masterbat = 1
masterlat = 1
masteravionics = 1
brakeparking = 0
primer = 3
starter = 1
autostart = 1
Print("engine is warming...")
Print(rpm)
while rpm <= 750 {
	Print(rpm)
}
Sleep(5000)
Print("let's fly")
var h0 = heading
breaks = 0
throttle = 1
while alt < 1000 {
	rudder = (h0 - heading)/80
	aileron = -roll / 70
	elevator = pitch / 50
	Sleep(250)
}
Print("done")

--------- Or with functions --------- (code handels function commands)
...
takeoff(var x) {
   Print(x)
   while alt < x {
	rudder = (h0 - heading)/80
	aileron = -roll / 70
	elevator = pitch / 50
        Print(alt)
	Sleep(250)
   }
}
takeoff(1000)
Print("done")
```

## Explanation

What's happening here is that once we run our executable, first it will read the script file that is provided as a parameter, parses it, and starts interpreting it, in our case the script file contains some commands which will be used in order to open a server and connect to FlightGear. 
After that, it'll create a TCP server that listens on port 5400, at the same time the client (C++) will try to connect to FlightGear on port 5402.

Once FlightGear connects to the server (C++) and the client (C++) connects to FlightGear, FlightGear will start streaming data through the TCP connection, where the server will then parses it and store it in memory for use with the script later.

Then at some port it'll wait for a total of 120 seconds (to make sure that it's loaded first), after that it'll setup the plane parameters to the correct values in order to let it fly, then it goes full throttle and flies the plane until it reaches a specific height, and then the script finishes.
