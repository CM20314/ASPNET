using System;
namespace CM20314.Models.Database
{
	public class Polyline
	{
		private string coordinateIdList;

        public Polyline(string coordinateIds)
        {
            coordinateIdList = coordinateIds;
        }
        public Polyline(IEnumerable<int> coordinateIds)
        {
            coordinateIdList = string.Join(',', coordinateIds);
        }

        //public int getBuildingID() { return ?; }
    }
}

