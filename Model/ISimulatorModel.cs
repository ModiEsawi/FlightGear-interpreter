﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj12.Model
{
    public interface ISimulatorModel : INotifyPropertyChanged
    {
        string Time { set; get; }
        void StartServer();

        //{"indicated-heading-deg","/instrumentation/heading-indicator/indicated-heading-deg" },
        //   {"gps_indicated-vertical-speed","/instrumentation/gps/indicated-vertical-speed" },
        //   {"gps_indicated-ground-speed-kt","/instrumentation/gps/indicated-ground-speed-kt" },
        //   {"airspeed-indicator_indicated-speed-kt","/instrumentation/airspeed-indicator/indicated-speed-kt" },
        //   {"gps_indicated-altitude-ft","/instrumentation/gps/indicated-altitude-ft" },
        //   {"attitude-indicator_internal-roll-deg","/instrumentation/attitude-indicator/internal-roll-deg" },
        //   {"attitude-indicator_internal-pitch-deg","/instrumentation/attitude-indicator/internal-pitch-deg" },
        //   {"altimeter_indicated-altitude-ft","/instrumentation/altimeter/indicated-altitude-ft" },

        double Indicated_heading_deg { set; get; }
        double Gps_indicated_vertical_speed { set; get; }
        double Gps_indicated_ground_speed_kt { set; get; }
        double Airspeed_indicator_indicated_speed_kt { set; get; }
        double Gps_indicated_altitude_ft { set; get; }
        double Attitude_indicator_internal_roll_deg { set; get; }
        double Attitude_indicator_internal_pitch_deg { set; get; }
        double Altimeter_indicated_altitude_ft { set; get; }


    }
}