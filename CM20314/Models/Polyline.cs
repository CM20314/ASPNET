using CM20314.Models.Database;
using System;
namespace CM20314.Models
{
    /// <summary>
    /// A list of coordinates that successively form a line
    /// </summary>
    public class Polyline
    {
        public List<Coordinate> Coordinates { get; set; }

        public Polyline(IEnumerable<Coordinate> coordinates)
        {
            this.Coordinates = coordinates.ToList();
        }

        public List<Coordinate> getCoordinates() { return Coordinates; }

        //public int getBuildingID() { return ?; }
        public override string ToString()
        {
            return string.Join(",", Coordinates.Select(c => c.Id));
        }
    }
}

