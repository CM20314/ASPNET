using System;
namespace CM20314.Models.Database
{
	public class Polyline
	{
		private Coordinate[] coordinateList;

		public Polyline(Coordinate[] coordinates)
		{
			coordinateList = coordinates;
		}

		//public int getBuildingID() { return ?; }
	}
}

