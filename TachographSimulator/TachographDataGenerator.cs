using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachographSimulator
{

    internal class TachographDataGenerator
    {
        // Dictionary to keep track of driving hours for each driver per day
        private static Dictionary<string, Dictionary<DateTime, TimeSpan>> DrivingHoursPerDriver = new Dictionary<string, Dictionary<DateTime, TimeSpan>>();
        // Maximum allowed driving hours per day
        private const int MaxDrivingHoursPerDay = 12;
        // Maximum allowed driving hours per week
        private const int MaxDrivingHoursPerWeek = 60;
        private readonly Random _random = new Random();

        public List<TachographData> GenerateDriverData(string driverId, DateTime date)
        {
            List<TachographData> data = new List<TachographData>();

            // Check for driving hour violations
            if (HasDrivingHourViolations(driverId))
            {
                Console.WriteLine($"Driving hour violations detected for Driver {driverId}. No more files will be generated for this driver.");
                
            }

            DateTime currentDate = DateTime.Now.Date;

            // Check if the driver already has an activity recorded on the same day
            if (!DrivingHoursPerDriver.ContainsKey(driverId))
            {
                DrivingHoursPerDriver[driverId] = new Dictionary<DateTime, TimeSpan>();
            }

            if (DrivingHoursPerDriver[driverId].ContainsKey(currentDate))
            {
                Console.WriteLine($"Driver {driverId} already has an activity recorded on {currentDate}. No more files will be generated for this driver on this day.");
                
            }

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

        public List<TachographData> GenerateAllDriverData(List<string> driverIds, DateTime date)
        {
            List<TachographData> allData = new List<TachographData>();

            foreach (var driverId in driverIds)
            {
                // Assume each driver starts at 08:00:00
                DateTime currentTime = date.AddHours(8);

                while (currentTime < date.AddHours(12)) // Assuming a 12-hour limit
                {
                    // Generate driving data
                    TimeSpan drivingDuration = TimeSpan.FromMinutes(_random.Next(30, 240)); // 30 minutes to 4 hours
                    allData.Add(new TachographData(driverId, currentTime, currentTime.Add(drivingDuration), "Driving"));
                    currentTime = currentTime.Add(drivingDuration);

                    // Generate rest data
                    TimeSpan restDuration = TimeSpan.FromMinutes(_random.Next(30, 45)); // 30 minutes to 45 minutes
                    allData.Add(new TachographData(driverId, currentTime, currentTime.Add(restDuration), "Rest"));
                    currentTime = currentTime.Add(restDuration);
                }
            }

            return allData;
        }

        public TimeSpan CalculateTotalDrivingHours(List<TachographData> simulatedData)
        {
            // Calculate total driving hours from simulated data
            TimeSpan totalDrivingHours = TimeSpan.Zero;

            foreach (var data in simulatedData)
            {
                if (data.Activity.Equals("Driving", StringComparison.OrdinalIgnoreCase))
                {
                    // Assuming the timestamp is in a valid format, parse it to DateTime
                    DateTime timestamp = DateTime.ParseExact(Convert.ToString(data.EndTime), "yyyy-MM-dd HH:mm:ss", null);
                    totalDrivingHours += TimeSpan.FromHours(1); // Assuming each driving activity is for 1 hour, adjust as needed
                }
            }

            return totalDrivingHours;
        }
        public double CalculateTotalWeeklyDrivingHours(string driverId)
        {
            // Calculate total weekly driving hours for the driver
            double totalWeeklyDrivingHours = 0;

            foreach (var entry in DrivingHoursPerDriver[driverId])
            {
                totalWeeklyDrivingHours += entry.Value.TotalHours;
            }

            return totalWeeklyDrivingHours;
        }

        public void UpdateDrivingHours(string driverId, DateTime currentDate, List<TachographData> simulatedData)
        {
            // Calculate total driving hours for the simulated data
            TimeSpan totalDrivingHours = CalculateTotalDrivingHours(simulatedData);

            // Update driving hours for the driver on the specified day
            if (!DrivingHoursPerDriver[driverId].ContainsKey(currentDate))
            {
                DrivingHoursPerDriver[driverId][currentDate] = TimeSpan.Zero;
            }

            DrivingHoursPerDriver[driverId][currentDate] += totalDrivingHours;
        }
        public bool HasDrivingHourViolations(string driverId)
        {
            // Check if the driver has exceeded the maximum allowed driving hours per day or per week
            if (DrivingHoursPerDriver.ContainsKey(driverId))
            {
                var currentDate = DateTime.Now.Date;

                // Check for daily violation
                if (DrivingHoursPerDriver[driverId].ContainsKey(currentDate) && DrivingHoursPerDriver[driverId][currentDate].TotalHours > MaxDrivingHoursPerDay)
                {
                    Console.WriteLine($"Daily driving hour violation detected for Driver {driverId} on {currentDate}.");
                    return true;
                }

                // Check for weekly violation
                var totalWeeklyDrivingHours = CalculateTotalWeeklyDrivingHours(driverId);
                if (totalWeeklyDrivingHours > MaxDrivingHoursPerWeek)
                {
                    Console.WriteLine($"Weekly driving hour violation detected for Driver {driverId}.");
                    return true;
                }
            }

            return false;
        }


    }
}
