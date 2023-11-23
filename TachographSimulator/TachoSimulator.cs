using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachographSimulator
{
    internal class TachoSimulator
    {
       
        List<TachographData> simulatedData;
        // Dictionary to keep track of unique drivers and their activities for each day
        private static Dictionary<DateTime, HashSet<string>> UniqueDriversPerDay = new Dictionary<DateTime, HashSet<string>>();
        // Maximum unique driver count
        private const int MaxUniqueDriverCount = 30;

        public List<TachographData> StartSimulation(string driverId,DateTime date)
        {
            // Start generating simulated data for up to 100 drivers
            // Follow the specified rules for driver activities and violations
            // Save generated data to the database
            // Check if the maximum unique driver count is reached
            if (UniqueDriversPerDay.Count >= MaxUniqueDriverCount)
            {
                Console.WriteLine($"Maximum unique driver count reached. No more files will be generated for Driver {driverId}.");
                
            }

            DateTime currentDate = DateTime.Now.Date;

            // Check if the driver already has an activity recorded on the same day
            if (!UniqueDriversPerDay.ContainsKey(currentDate))
            {
                UniqueDriversPerDay[currentDate] = new HashSet<string>();
            }

            if (UniqueDriversPerDay[currentDate].Contains(driverId))
            {
                Console.WriteLine($"Driver {driverId} already has an activity recorded on {currentDate}. No more files will be generated for this driver on this day.");
               
            }

            var generator = new TachographDataGenerator();
               simulatedData = generator.GenerateDriverData(driverId, date);
               
               return simulatedData;
            
            
        }

        public void StopSimulation()
        {
            // Stop the data simulation
            // ...
        }

    }
}
