using System;
namespace CM20314.Models.Database
{
	public class Settings
	{
        public bool RequiresAccessibleRoute { get; set; }
        public bool RequiresHighContrast { get; set; }

        public Settings(bool accessibleRoute, bool highContrast)
		{
			RequiresAccessibleRoute = accessibleRoute;
			RequiresHighContrast    = highContrast;
		}
	}
}

