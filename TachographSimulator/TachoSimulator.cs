using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TachographSimulator
{
    internal class TachoSimulator
    {
        //private readonly TachographDataStorage _dataStorage;

        //public TachoSimulator(TachographDataStorage dataStorage)
        //{
        //    _dataStorage = dataStorage;
        //}
        List<TachographData> simulatedData;
        public List<TachographData> StartSimulation()
        {
            // Start generating simulated data for up to 100 drivers
            // Follow the specified rules for driver activities and violations
            // Save generated data to the database
            
                var generator = new TachographDataGenerator();
                var driverId = "Driver A";
                var date = new DateTime(2023, 11, 01);

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
