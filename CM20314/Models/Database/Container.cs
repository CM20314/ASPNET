using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace CM20314.Models.Database
{
    public class Container : Entity
    {
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string PolylineIds { get; set; }

        [NotMapped]
        public Polyline Polyline { get; set; } = new Polyline(new List<Coordinate>());

        public Container()
        {
            
        }
        public Container(string shortName, string longName, string polylineIds)
        {
            ShortName = shortName;
            LongName = longName;
            PolylineIds = polylineIds;
        }
    }
}

