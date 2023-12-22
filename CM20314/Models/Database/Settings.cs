using System;
namespace CM20314.Models.Database
{
	public class Settings
	{
		private bool requiresAccessibleRoute;
		private bool requiresHighContrast;

		public Settings(bool accessibleRoute, bool highContrast)
		{
			requiresAccessibleRoute = accessibleRoute;
			requiresHighContrast    = highContrast;
		}

		public bool getAccessibleRoute() { return requiresAccessibleRoute; }

		public bool getHighContrast() {  return requiresHighContrast; }
	}
}

