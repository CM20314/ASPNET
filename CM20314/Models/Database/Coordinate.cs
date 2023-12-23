using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace CM20314.Models.Database
{
    public class Coordinate : Entity
    {
        private double x;
        private double y;

        [NotMapped]
        private int z;

        public Coordinate()
        {

        }

        public Coordinate(double x, double y, int z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void setX(double newX) { this.x = newX; }

        public void setY(double newY) { this.y = newY; }

        public double getX() { return this.x; }

        public double getY() { return this.y; }
        public int getZ() { return this.z; }

        public static double CalculateEucilidianDistance(Coordinate c1, Coordinate c2)
        {
            return Math.Sqrt(Math.Pow(c1.getX() - c2.getX(), 2) + Math.Pow(c1.getY() - c2.getY(), 2));
        }

        public override bool Equals(object? obj)
        {
            Coordinate coordinate = obj as Coordinate ?? new Coordinate(-1, -1);
            return coordinate.x == this.x && coordinate.y == this.y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

