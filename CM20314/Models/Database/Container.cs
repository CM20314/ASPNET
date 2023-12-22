using System;
namespace CM20314.Models.Database
{
    public class Container : Entity
    {
        private string shortName;
        private string longName;
        private Polyline polyline;

        public Container()
        {
            
        }
        public Container(string shortName, string longName, Polyline containerPolyline)
        {
            this.shortName = shortName;
            this.longName = longName;
            polyline = containerPolyline;
        }

        //public ? getEntranceNodes() { }

        public string getShortName() { return this.shortName; }
        public string getLongName() { return this.longName; }

        public Polyline getPolyline() { return this.polyline; }
    }
}

