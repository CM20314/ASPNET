using System;
using CM20314.Models;
using CM20314.Models.Database;

namespace CM20314.Services
{
	public class MapDataService
	{
		private MapResponseData mapResponseData;

        public MapDataService()
		{
			mapResponseData = new MapResponseData(); // Set on startup, only intialised once across system
		}

        public MapResponseData GetMapData()
        {
            return mapResponseData;
        }

        public List<Container> SearchContainers()
        {
            return new List<Container>();
        }
    }
}

