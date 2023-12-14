using CM20314.Models.Database;

namespace CM20314.Models
{
    public class RouteRequestData
    {
        public Coordinate? StartCoordinate { get; set; }
        public Node? StartNode { get; set; }
        public Container EndContainer { get; set; }
        public AccessibilityLevel AccessibilityLevel { get; set; }

        public RouteRequestData(
            Coordinate startCoordinate,
            Node startNode, 
            Container endContainer, 
            AccessibilityLevel accessibilityLevel)
        {
            StartCoordinate = startCoordinate;
            StartNode = startNode;
            EndContainer = endContainer;
            AccessibilityLevel = accessibilityLevel;
        }
    }
}
