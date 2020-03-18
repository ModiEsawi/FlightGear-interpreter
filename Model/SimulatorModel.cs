using proj12.Model.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj12.Model
{
    class SimulatorModel : ISimulatorModel
    {
        Dictionary<int, string> genericsTable = new Dictionary<int, string>()
            {
                {0,"/instrumentation/airspeed-indicator/indicated-speed-kt"},
                {1,"/sim/time/warp"},
                {2,"/controls/switches/magnetos"},
                {3,"/instrumentation/heading-indicator/offset-deg"},
                {4,"/instrumentation/altimeter/indicated-altitude-ft"},
                {5,"/instrumentation/altimeter/pressure-alt-ft"},
                {6,"/instrumentation/attitude-indicator/indicated-pitch-deg"},
                {7,"/instrumentation/attitude-indicator/indicated-roll-deg"},
                {8,"/instrumentation/attitude-indicator/internal-pitch-deg"},
                {9,"/instrumentation/attitude-indicator/internal-roll-deg"},
                {10,"/instrumentation/encoder/indicated-altitude-ft"},
                {11,"/instrumentation/encoder/pressure-alt-ft"},
                {12,"/instrumentation/gps/indicated-altitude-ft"},
                {13,"/instrumentation/gps/indicated-ground-speed-kt"},
                {14,"/instrumentation/gps/indicated-vertical-speed"},
                {15,"/instrumentation/heading-indicator/indicated-heading-deg"},
                {16,"/instrumentation/magnetic-compass/indicated-heading-deg"},
                {17,"/instrumentation/slip-skid-ball/indicated-slip-skid"},
                {18,"/instrumentation/turn-indicator/indicated-turn-rate"},
                {19,"/instrumentation/vertical-speed-indicator/indicated-speed-fpm"},
                {20,"/controls/flight/aileron"},
                {21,"/controls/flight/elevator"},
                {22,"/controls/flight/rudder"},
                {23,"/controls/flight/flaps"},
                {24,"/controls/engines/engine/throttle"},
                {25,"/controls/engines/current-engine/throttle"},
                {26,"/controls/switches/master-avionics"},
                {27,"/controls/switches/starter"},
                {28,"/engines/active-engine/auto-start"},
                {29,"/controls/flight/speedbrake"},
                {30,"/sim/model/c172p/brake-parking"},
                {31,"/controls/engines/engine/primer"},
                {32,"/controls/engines/current-engine/mixture"},
                {33,"/controls/switches/master-bat"},
                {34,"/controls/switches/master-alt"},
                {35,"/engines/engine/rpm"},
            };

        public event PropertyChangedEventHandler PropertyChanged;

        private OpenServer openServer = null;
        private CurrentTime currentTime = null;
        private string time;
        public string Time
        {
            set
            {
                this.time = value;
                NotifyPropertyChanged("Time");
            }
            get
            {
                return this.currentTime.Time;
            }
        }

        private double indicated_heading_deg;
        private double gps_indicated_vertical_speed;
        private double gps_indicated_ground_speed_kt;
        private double airspeed_indicator_indicated_speed_kt;
        private double gps_indicated_altitude_ft;
        private double attitude_indicator_internal_roll_deg;
        private double attitude_indicator_internal_pitch_deg;
        private double altimeter_indicated_altitude_ft;


        public double Indicated_heading_deg { get { return openServer.getValue("/instrumentation/heading-indicator/indicated-heading-deg"); } set { indicated_heading_deg = value; NotifyPropertyChanged("Indicated_heading_deg"); } }
        public double Gps_indicated_vertical_speed { get { return openServer.getValue("/instrumentation/gps/indicated-vertical-speed"); } set { gps_indicated_vertical_speed = value; NotifyPropertyChanged("Gps_indicated_vertical_speed"); } }
        public double Gps_indicated_ground_speed_kt { get { return openServer.getValue("/instrumentation/gps/indicated-ground-speed-kt"); } set { gps_indicated_ground_speed_kt = value; NotifyPropertyChanged("Gps_indicated_ground_speed_kt"); } }
        public double Airspeed_indicator_indicated_speed_kt { get { return openServer.getValue("/instrumentation/airspeed-indicator/indicated-speed-kt"); } set { airspeed_indicator_indicated_speed_kt = value; NotifyPropertyChanged("Airspeed_indicator_indicated_speed_kt"); } }
        public double Gps_indicated_altitude_ft { get { return openServer.getValue("/instrumentation/gps/indicated-altitude-ft"); } set { gps_indicated_altitude_ft = value; NotifyPropertyChanged("Gps_indicated_altitude_ft"); } }
        public double Attitude_indicator_internal_roll_deg { get { return openServer.getValue("/instrumentation/attitude-indicator/internal-roll-deg"); } set { attitude_indicator_internal_roll_deg = value; NotifyPropertyChanged("Attitude_indicator_internal_roll_deg"); } }
        public double Attitude_indicator_internal_pitch_deg { get { return openServer.getValue("/instrumentation/attitude-indicator/internal-pitch-deg"); } set { attitude_indicator_internal_pitch_deg = value; NotifyPropertyChanged("Attitude_indicator_internal_pitch_deg"); } }
        public double Altimeter_indicated_altitude_ft { get { return openServer.getValue("/instrumentation/altimeter/indicated-altitude-ft"); } set { altimeter_indicated_altitude_ft = value; NotifyPropertyChanged("Altimeter_indicated_altitude_ft"); } }
        [Obsolete]
        public SimulatorModel()
        {
            this.currentTime = new CurrentTime();
            this.currentTime.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                this.Time = currentTime.Time;
            };
            this.openServer = new OpenServer(genericsTable);
            this.openServer.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                this.Indicated_heading_deg = openServer.getValue("/instrumentation/heading-indicator/indicated-heading-deg");
                this.Gps_indicated_vertical_speed = openServer.getValue("/instrumentation/gps/indicated-vertical-speed");
                this.Gps_indicated_ground_speed_kt = openServer.getValue("/instrumentation/gps/indicated-ground-speed-kt");
                this.Airspeed_indicator_indicated_speed_kt = openServer.getValue("/instrumentation/airspeed-indicator/indicated-speed-kt");
                this.Gps_indicated_altitude_ft = openServer.getValue("/instrumentation/gps/indicated-altitude-ft");
                this.Attitude_indicator_internal_roll_deg = openServer.getValue("/instrumentation/attitude-indicator/internal-roll-deg");
                this.Attitude_indicator_internal_pitch_deg = openServer.getValue("/instrumentation/attitude-indicator/internal-pitch-deg");
                this.Altimeter_indicated_altitude_ft = openServer.getValue("/instrumentation/altimeter/indicated-altitude-ft");
            };
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void StartServer()
        {
            this.openServer.StartServer();
        }
    }
}
