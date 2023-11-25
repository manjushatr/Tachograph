using AwsDriverServicesApi.Models;
using AwsDriverServicesApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AwsDriverServicesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService ?? throw new ArgumentNullException(nameof(driverService));
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllDrivers()
        {

            var res = await _driverService.GetAllDrivers().ConfigureAwait(false);
            return Ok(res);
        }

        [HttpGet("{driverId}")]
        public async Task<IActionResult> GetDriver(int driverId)
        {
            var res = await _driverService.GetDriver(driverId).ConfigureAwait(false);
            return Ok(res);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddDriver([FromBody] Driver driver)
        {
            await _driverService.AddDriver(driver).ConfigureAwait(false);
            return Ok("Driver Addition Successful");
        }

        [HttpPut("{driverId}")]
        public async Task<IActionResult> UpdateDriver([FromBody] Driver driver, int driverId)
        {
            await _driverService.UpdateDriver(driver, driverId).ConfigureAwait(false);
            return Ok("Driver Update Successful");
        }

        [HttpDelete("{driverId}")]
        public async Task<IActionResult> DeleteDriver(int driverId)
        {
            // Use student object to delete the student in DB
            await _driverService.DeleteDriver(driverId).ConfigureAwait(false);
            return Ok("Deleted the Driver");
        }
    }
}
