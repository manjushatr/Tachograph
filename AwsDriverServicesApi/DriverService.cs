
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AwsDriverServicesApi.Models;

namespace AwsDriverServicesApi
{
    public class DriverService
    {
        IDynamoDBContext DDBContext { get; set; }

        public DriverService()
        {
            var config = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
            this.DDBContext = new DynamoDBContext(
                           new AmazonDynamoDBClient("aws-access-key-id", "aws-secret-access-key", RegionEndpoint.APSouth1), config);
        }

        public async Task AddDriver(Driver driver)
        {
            await DDBContext.SaveAsync<Driver>(driver).ConfigureAwait(false);
        }

        /// <summary>
        /// Soft Delete of the existing Student
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        public async Task DeleteDriver(int driverId)
        {
            var existingDriver = await GetDriver(driverId).ConfigureAwait(false);
            
            await UpdateDriver(existingDriver, driverId);
        }


        public async Task<IEnumerable<Driver>> GetAllDrivers()
        {
            var search = DDBContext.ScanAsync<Driver>(null);
            var allDrivers = await search.GetNextSetAsync();
            return allDrivers;
        }

        public async Task<Driver> GetDriver(int driverId)
        {
            List<ScanCondition> conditions = new List<ScanCondition>();
            conditions.Add(new ScanCondition("Id", ScanOperator.Equal, driverId));
            var search = DDBContext.ScanAsync<Driver>(conditions);
            var driver = await search.GetNextSetAsync().ConfigureAwait(false);
            return driver.SingleOrDefault(x => x.Id == driverId);
        }

        public async Task UpdateDriver(Driver driver, int driverId)
        {
            var existingDriver = await DDBContext.LoadAsync<Driver>(driverId);
            existingDriver.FirstName = driver.FirstName;
            existingDriver.LastName = driver.LastName;
            existingDriver.Address = driver.Address;
            existingDriver.MobileNumber= driver.MobileNumber;
            existingDriver.LicenceNumber= driver.LicenceNumber;
            await DDBContext.SaveAsync<Driver>(existingDriver);
        }
    }
}
