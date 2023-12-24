using System;
namespace CM20314.Models.Database
{
    public class Container : Entity
    {
        private string shortName;
        private string longName;
        private string polylineIds;

        public Container()
        {
            
        }
        public Container(string shortName, string longName, string polylineIds)
        {
            this.shortName = shortName;
            this.longName = longName;
            this.polylineIds = polylineIds;
        }

        //public ? getEntranceNodes() { }

        public string getShortName() { return this.shortName; }
        public string getLongName() { return this.longName; }
        public string getPolylineIds() { return this.polylineIds; }
    }
}

