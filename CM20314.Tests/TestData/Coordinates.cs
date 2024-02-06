using CM20314.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM20314.Tests.TestData
{
    public static class Coordinates
    {
        public static List<Coordinate> coordinates = new List<Coordinate>();

        public static void Initialise()
        {
            coordinates.Add(new Coordinate(4, 5));
            coordinates.Add(new Coordinate(96, 2));
            coordinates.Add(new Coordinate(50, 50));
            coordinates.Add(new Coordinate(-4, -10));
            coordinates.Add(new Coordinate(0, 0));
        }
    }
}
