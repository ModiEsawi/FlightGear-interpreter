using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace proj12
{

    public partial class App : Application
    {
        public static Dictionary<string, string> dashBoardVars = new Dictionary<string, string>
        {
            {"indicated-heading-deg","/instrumentation/heading-indicator/indicated-heading-deg" },
            {"gps_indicated-vertical-speed","/instrumentation/gps/indicated-vertical-speed" },
            {"gps_indicated-ground-speed-kt","/instrumentation/gps/indicated-ground-speed-kt" },
            {"airspeed-indicator_indicated-speed-kt","/instrumentation/airspeed-indicator/indicated-speed-kt" },
            {"gps_indicated-altitude-ft","/instrumentation/gps/indicated-altitude-ft" },
            {"attitude-indicator_internal-roll-deg","/instrumentation/attitude-indicator/internal-roll-deg" },
            {"attitude-indicator_internal-pitch-deg","/instrumentation/attitude-indicator/internal-pitch-deg" },
            {"altimeter_indicated-altitude-ft","/instrumentation/altimeter/indicated-altitude-ft" },
        };

        public static Dictionary<string, string> GetDashboardVars()
        {
            return dashBoardVars;
        }
    }
}
