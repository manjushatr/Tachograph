using AwsDriverServicesApi.Models;

namespace AwsDriverServicesApi.Services
{
    public interface IDriverService
    {
        Task<IEnumerable<Driver>> GetAllDrivers();
        Task<Driver> GetDriver(int DriverId);
        Task AddDriver(Driver driver);
        Task UpdateDriver(Driver driver, int driverId);
        Task DeleteDriver(int driverId);
    }
}
