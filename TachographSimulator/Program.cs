using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TachographSimulator
{
    internal class Program
    {
        //private static readonly List<string> DriverIds = new List<string> { "DriverA", "DriverB", "DriverC" }; // Add your driver IDs here
        private static readonly string OutputDirectory = "SimulatedData"; // Directory to store simulated data files
        static void Main(string[] args)
        {
            // Create the output directory if it doesn't exist
            Directory.CreateDirectory(OutputDirectory);

            // Create an instance of the TachographSimulator
            var simulator = new TachoSimulator();

            // Flag to indicate whether the simulation is running
            bool isSimulationRunning = false;
            while (true)
            {
                Console.WriteLine("1. Start Simulation");
                Console.WriteLine("2. Stop Simulation");
                Console.WriteLine("3. Exit");

                Console.Write("Choose an option: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        if (!isSimulationRunning)
                        {
                            isSimulationRunning = true;
                            Console.WriteLine("Simulation started. Press any key to stop...");
                            var day = DateTime.Now.Day;
                            var hour = DateTime.Now.Hour;
                            var minute = DateTime.Now.Minute;
                            var second = DateTime.Now.Second;
                            var month = DateTime.Now.Month;
                            var year = DateTime.Now.Year;
                            List<TachographData> simulatedData = null;
                            // Run the simulation in a separate thread
                            Console.Write("Enter Driver ID: ");
                            string driverId = Console.ReadLine();

                            var simulationThread = new Thread(() =>
                            {
                                while (isSimulationRunning)
                                {
                                    // Generate simulated data (adjust as needed)
                                    simulatedData = simulator.StartSimulation(driverId, DateTime.Now.Date);

                                    // Print or process the simulated data as needed
                                    foreach (var dataPoint in simulatedData)
                                    {
                                        Console.WriteLine($"Driver: {dataPoint.DriverId}" + "\n"+
                                                          $"Start Time: {dataPoint.StartTime} " + "\n" +
                                                          $"End Time: {dataPoint.EndTime} " + "\n" +
                                                          $"Activity: {dataPoint.Activity}" + "\n")  ;
                                    }

                                    // Adjust the interval based on your requirements
                                    Thread.Sleep(1000);
                                }
                            });

                            simulationThread.Start();
                            Console.ReadKey(); // Wait for user input to stop simulation
                            isSimulationRunning = false;

                            simulationThread.Join(); // Ensure the simulation thread completes
                                                     // Save simulated data to a JSON file for each driver
                                                     // Save simulated data to a JSON file in the output directory

                            string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                            string folderName = Path.Combine(projectPath, "SimulatedData");
                            System.IO.Directory.CreateDirectory(folderName);
                            string subdir = Path.Combine(folderName, $"{day}{month}{year}");

                            System.IO.Directory.CreateDirectory(folderName);
                            
                           
                            System.IO.Directory.CreateDirectory(subdir);

                            string outputFilePath = $"{driverId}_SimulatedData.json";
                            //string outputFilePath = $"{driverId}{date1}{date2}{date3}{date4}{date5}_SimulatedData.json";
                            TachographDataWriter.WriteToJsonFile(simulatedData, subdir);

                            Console.WriteLine($"Simulated data  saved to {outputFilePath}");
                            DateTime currentDate = DateTime.Now.Date;
                            new TachographDataGenerator().UpdateDrivingHours(driverId, currentDate, simulatedData);
                        }
                        else
                        {
                            Console.WriteLine("Simulation is already running.");
                        }
                        break;

                    case "2":
                        if (isSimulationRunning)
                        {
                            isSimulationRunning = false;
                            Console.WriteLine("Simulation stopped.");
                        }
                        else
                        {
                            Console.WriteLine("Simulation is not running.");
                        }
                        break;

                    case "3":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        
        }
    
}
