using System;
namespace CM20314.Models.Database
{
	public class Container : Entity
	{
		private string   name;
		private Polyline polyline;

		public Container(string containerName, Polyline containerPolyline)
		{
			name     = containerName;
			polyline = containerPolyline;
		}

		//public ? getEntranceNodes() { }

		public string getName() { return this.name; }

		public Polyline getPolyline() { return this.polyline; }
	}
}

