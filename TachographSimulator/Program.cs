using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TachographSimulator
{
    internal class Program
    {
        private static readonly List<string> DriverIds = new List<string> { "DriverA", "DriverB", "DriverC" }; // Add your driver IDs here
        static void Main(string[] args)
        {
            //List<TachographData> myList = new List<TachographData>();
            //TachoSimulator sObj = new TachoSimulator();
            //myList=sObj.StartSimulation();
            //foreach (var dataPoint in myList)
            //{
            //    Console.WriteLine($"Driver: {dataPoint.DriverId}");
            //    Console.WriteLine($"Date: {dataPoint.StartTime.ToShortDateString()}");
            //    Console.WriteLine($"Time Start: {dataPoint.StartTime.ToLongTimeString()}");
            //    Console.WriteLine($"Time End: {dataPoint.EndTime.ToLongTimeString()}");
            //    Console.WriteLine($"Activity: {dataPoint.Activity}");
            //    Console.WriteLine();
            //}

            // Create an instance of the TachographDataGenerator
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

                            // Run the simulation in a separate thread
                            var simulationThread = new Thread(() =>
                            {
                                while (isSimulationRunning)
                                {

                                    // Generate simulated data (adjust as needed)
                                    var simulatedData = simulator.StartSimulation("Driver A", DateTime.Now.Date);

                                    // Print or process the simulated data as needed
                                    foreach (var dataPoint in simulatedData)
                                    {
                                        Console.WriteLine($"Driver: {dataPoint.DriverId}, " +
                                                          $"Start Time: {dataPoint.StartTime}, " +
                                                          $"End Time: {dataPoint.EndTime}, " +
                                                          $"Activity: {dataPoint.Activity}");
                                    }

                                    // Adjust the interval based on your requirements
                                    Thread.Sleep(1000);
                                }
                            });

                            simulationThread.Start();
                            Console.ReadKey(); // Wait for user input to stop simulation
                            isSimulationRunning = false;
                            simulationThread.Join(); // Ensure the simulation thread completes
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
