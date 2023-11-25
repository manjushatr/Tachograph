using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwsDriverServicesApi.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Address { get; set; }= null!;
        public string MobileNumber { get; set; }=null!;
        public string Nationality { get; set; }=null !;
        public string LicenceNumber { get; set; } = null!;
        }
}
