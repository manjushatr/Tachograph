using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachographSimulator.Entities
{
    public class ParsedData
    {
        public string Timestamp { get; set; }
        public string DriverId { get; set; }
        public string Activity { get; set; }
    }
}
