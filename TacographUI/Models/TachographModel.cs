namespace TacographUI.Models
{
    public class TachographModel
    {
        public int TrackId { get; set; }
        public string Timestamp { get; set; } = null!;
        public string DriverId { get; set; } = null!;
        public string Activity { get; set; } = null!;

    }
}
