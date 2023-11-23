using System.ComponentModel.DataAnnotations.Schema;

namespace TacographData.API.Models
{
    [Table("DriverInfo.TachoDriver")]
    public class TachoData
    {         
        [Column("trackid")]
        public int TrackId { get; set; }

        [Column("timestamp")]
        public string Timestamp { get; set; } = null!;

        [Column("driverid")]
        public string DriverId { get; set; }=null!;

        [Column("activity")]

        public string Activity { get; set; } = null!;

    }
}


