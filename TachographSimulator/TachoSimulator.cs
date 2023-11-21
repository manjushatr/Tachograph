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
        public List<TachographData> StartSimulation(List<string> driverIds,DateTime date)
        {
            // Start generating simulated data for up to 100 drivers
            // Follow the specified rules for driver activities and violations
            // Save generated data to the database
            
               var generator = new TachographDataGenerator();
               simulatedData = generator.GenerateAllDriverData(driverIds, date);
               
               return simulatedData;
            
            
        }

        public void StopSimulation()
        {
            // Stop the data simulation
            // ...
        }

    }
}
