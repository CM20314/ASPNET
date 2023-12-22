using System;
namespace CM20314.Models.Database
{
	public class Coordinate : Entity
	{
		private float x;
		private float y;

		public Coordinate(float xCoordinate, float yCoordinate)
		{
			x = xCoordinate;
			y = yCoordinate;
		}

		public void setX(float newX) { this.x = newX; }

		public void setY(float newY) { this.y = newY; }

		public float getX() { return this.x; }

		public float getY() { return this.y; }
	}
}

