using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachographSimulator
{
    internal class TachographDataGenerator
    {
        private readonly Random _random = new Random();

        public List<TachographData> GenerateDriverData(string driverId, DateTime date)
        {
            List<TachographData> data = new List<TachographData>();

            // Assume driver drives for 4 hours and then rests for 45 minutes
            DateTime currentTime = date.AddHours(8); // Start at 08:00:00

            while (currentTime < date.AddHours(12)) // Assuming a 12-hour limit
            {
                // Generate driving data
                TimeSpan drivingDuration = TimeSpan.FromMinutes(_random.Next(30, 240)); // 30 minutes to 4 hours
                data.Add(new TachographData(driverId, currentTime, currentTime.Add(drivingDuration), "Driving"));
                currentTime = currentTime.Add(drivingDuration);

                // Generate rest data
                TimeSpan restDuration = TimeSpan.FromMinutes(_random.Next(30, 45)); // 30 minutes to 45 minutes
                data.Add(new TachographData(driverId, currentTime, currentTime.Add(restDuration), "Rest"));
                currentTime = currentTime.Add(restDuration);
            }

            return data;
        }

    }
}
