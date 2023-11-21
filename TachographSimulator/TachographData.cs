using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachographSimulator
{
    internal class TachographData
    {
        public string DriverId { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public string Activity { get; }

        public TachographData(string driverId, DateTime startTime, DateTime endTime, string activity)
        {
            DriverId = driverId;
            StartTime = startTime;
            EndTime = endTime;
            Activity = activity;
        }

    }
}
