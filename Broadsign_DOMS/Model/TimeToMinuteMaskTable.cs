using System;
using System.Collections.Generic;
using System.Text;

namespace Broadsign_DOMS.Model
{
    public class TimeToMinuteMaskTable
    {
        string day;
        string hour;
        string minuteMask;

        public string Day { get => day; set => day = value; }
        public string Hour { get => hour; set => hour = value; }
        public string MinuteMask { get => minuteMask; set => minuteMask = value; }
    }
}
