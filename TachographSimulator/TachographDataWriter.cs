using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachographSimulator
{
    internal class TachographDataWriter
    {
        //public static void WriteSimulatedDataToFile(string driverId, List<TachographData> simulatedData)
        //{
        //    // Save simulated data to a JSON file for each driver
        //    string outputFilePath = $"{driverId}_SimulatedData.json";
        //    WriteToJsonFile(simulatedData, outputFilePath);

        //    Console.WriteLine($"Simulated data for Driver {driverId} saved to {outputFilePath}");
        //}
        private static Dictionary<DateTime, int> FilesPerDayCount = new Dictionary<DateTime, int>();

        
        public static void WriteToJsonFile<T>(T data, string filePath)
        {
            // Check if the maximum files per day limit is reached
            DateTime currentDate = DateTime.Now.Date;
            if (!FilesPerDayCount.ContainsKey(currentDate))
            {
                FilesPerDayCount[currentDate] = 1;
            }
            else
            {
                FilesPerDayCount[currentDate]++;
            }

            if (FilesPerDayCount[currentDate] <= 100)
            {
                // Serialize the data to JSON and write to a file
                string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);


                // Write to the specified file path
                System.IO.File.WriteAllText(filePath, jsonData);
                // Update driving hours for the driver
                
            }
            else
            {
                Console.WriteLine($"Maximum files per day limit reached for {currentDate}. No more files will be generated for Driver on this day.");
            }
        }
    }
}
