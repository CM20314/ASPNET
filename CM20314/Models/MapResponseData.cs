using CM20314.Models.Database;

namespace CM20314.Models
{
    /// <summary>
    /// Represents map data to return to the client
    /// </summary>
	public class MapResponseData
	{
        public List<Building> Buildings { get; set; }
        public List<Room> Rooms { get; set; }
        public List<NodeArc> Paths { get; set; }

        public MapResponseData(List<Building> buildings, List<Room> rooms, List<NodeArc> paths) 
        {
            Buildings = buildings;
            Rooms = rooms;
            Paths = paths;
        }

    }
}

