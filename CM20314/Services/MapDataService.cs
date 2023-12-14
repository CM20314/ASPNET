using System;
using CM20314.Data;
using CM20314.Models;
using CM20314.Models.Database;

namespace CM20314.Services
{
	public class MapDataService
	{
		private MapResponseData mapResponseData = new MapResponseData();

        public void Initialise(IServiceProvider serviceProvider)
        {
            ApplicationDbContext dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            
            // Set on startup, only intialised once across system
            mapResponseData = new MapResponseData();
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

