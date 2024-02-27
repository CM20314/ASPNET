using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace CM20314.Models.Database
{
    public class Coordinate : Entity
    {
        public double X { get; set; }
        public double Y { get; set; }

        [NotMapped]
        public int Z { get; set; }
        [NotMapped]
        public string MatchHandle { get; set; }

        public Coordinate()
        {

        }

        public Coordinate(double x, double y, int z = 0, string matchHandle = "")
        {
            this.X = Math.Round(x, 3);
            this.Y = Math.Round(y, 3); ;
            this.Z = z;
            this.MatchHandle = matchHandle;
        }

        public static double CalculateEucilidianDistance(Coordinate c1, Coordinate c2)
        {
            return Math.Sqrt(Math.Pow(c1.X - c2.X, 2) + Math.Pow(c1.Y - c2.Y, 2));
        }

        public override bool Equals(object? obj)
        {
            Coordinate coordinate = obj as Coordinate ?? new Coordinate(-1, -1);
            return coordinate.X == this.X && coordinate.Y == this.Y;
        }
        public bool Equals(Coordinate coordinate)
        {
            return coordinate.X == this.X && coordinate.Y == this.Y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

