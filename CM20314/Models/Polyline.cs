using CM20314.Models.Database;
using System;
namespace CM20314.Models
{
    public class Polyline
    {
        private List<Coordinate> coordinates;

        public Polyline(IEnumerable<Coordinate> coordinates)
        {
            this.coordinates = coordinates.ToList();
        }

        public List<Coordinate> getCoordinates() { return coordinates; }

        //public int getBuildingID() { return ?; }
        public override string ToString()
        {
            return string.Join(",", coordinates.Select(c => c.Id));
        }
    }
}

