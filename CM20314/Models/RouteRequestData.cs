using CM20314.Models.Database;

namespace CM20314.Models
{
    /// <summary>
    /// Represents a client route request
    /// </summary>
    public class RouteRequestData
    {
        public Coordinate? StartCoordinate { get; set; }
        public string EndContainerName { get; set; } = "";
        public AccessibilityLevel AccessibilityLevel { get; set; }
    }
}
