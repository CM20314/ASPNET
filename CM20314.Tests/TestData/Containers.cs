using CM20314.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM20314.Tests.TestData
{
    public static class Containers
    {
        public static List<Building> buildings = new List<Building>();
        public static List<Room> rooms = new List<Room>();

        public static void Initialise()
        {
            buildings.Add(new Building("10W", "10 West", "", new List<int>() { }));
            buildings.Add(new Building("1E", "1 East", "", new List<int>() { }));
            buildings.Add(new Building("1S", "1 South", "", new List<int>() { }));
            buildings.Add(new Building("1W", "1 West", "", new List<int>() { }));
            buildings.Add(new Building("1WN", "1 West North", "", new List<int>() { }));
            buildings.Add(new Building("2E", "2 East", "", new List<int>() { }));
            buildings.Add(new Building("2S", "2 South", "", new List<int>() { }));
            buildings.Add(new Building("2W", "2 West", "", new List<int>() { }));
            buildings.Add(new Building("3E", "3 East", "", new List<int>() { }));
            buildings.Add(new Building("3G", "3G Football Pitch", "", new List<int>() { }));
            buildings.Add(new Building("3S", "3 South", "", new List<int>() { }));
            buildings.Add(new Building("3W", "3 West", "", new List<int>() { }));
            buildings.Add(new Building("3WA", "3 West (Addition)", "", new List<int>() { }));
            buildings.Add(new Building("3WN", "3 West North", "", new List<int>() { }));
            buildings.Add(new Building("4E", "4 East", "", new List<int>() { }));
            buildings.Add(new Building("4ES", "4 East South", "", new List<int>() { }));
            buildings.Add(new Building("4S", "4 South", "", new List<int>() { }));
            buildings.Add(new Building("4SA", "4 South (Addition)", "", new List<int>() { }));
            buildings.Add(new Building("4W", "4 West", "", new List<int>() { }));
            buildings.Add(new Building("4W Cafe", "4W Cafe", "", new List<int>() { }));
            buildings.Add(new Building("5S", "5 South", "", new List<int>() { }));
            buildings.Add(new Building("5W", "5 West", "", new List<int>() { }));
            buildings.Add(new Building("6E", "6 East", "", new List<int>() { }));
            buildings.Add(new Building("6W", "6 West", "", new List<int>() { }));
            buildings.Add(new Building("6WS", "6 West South", "", new List<int>() { }));
            buildings.Add(new Building("7W", "7 West", "", new List<int>() { }));
            buildings.Add(new Building("8E", "8 East", "", new List<int>() { }));
            buildings.Add(new Building("8W", "8 West (Main)", "", new List<int>() { }));
            buildings.Add(new Building("8W", "8W (Addition)", "", new List<int>() { }));
            buildings.Add(new Building("9W", "9 West", "", new List<int>() { }));
            buildings.Add(new Building("Astro", "Astro Pitch", "", new List<int>() { }));
            buildings.Add(new Building("Track", "Athletics Track", "", new List<int>() { }));
            buildings.Add(new Building("Volleyball", "Beach Volleyball Court", "", new List<int>() { }));
            buildings.Add(new Building("Bobsleigh Track", "Bobsleigh Track", "", new List<int>() { }));
            buildings.Add(new Building("CB", "Chancellor's Building", "", new List<int>() { }));
            buildings.Add(new Building("Chaplaincy", "Chaplaincy", "", new List<int>() { }));
            buildings.Add(new Building("Clay", "Clay Court", "", new List<int>() { }));
            buildings.Add(new Building("EB", "East Building", "", new List<int>() { }));
            buildings.Add(new Building("Eastwood Pitches", "Eastwood Football Pitches", "", new List<int>() { }));
            buildings.Add(new Building("Estates", "Department of Campus Infrastructure", "", new List<int>() { }));
            buildings.Add(new Building("FH", "Foundler's Hall", "", new List<int>() { }));
            buildings.Add(new Building("Hockey", "Hockey Pitch", "", new List<int>() { }));
            buildings.Add(new Building("Library", "The Library", "", new List<int>() { }));
            buildings.Add(new Building("Limekiln Pitches", "Limekiln Football Pitches", "", new List<int>() { }));
            buildings.Add(new Building("Medical Centre", "Medical Centre", "", new List<int>() { }));
            buildings.Add(new Building("Norwood House", "Norwood House", "", new List<int>() { }));
            buildings.Add(new Building("Tennis Courts", "Outdoor Tennis Courts", "", new List<int>() { }));
            buildings.Add(new Building("Rugby Pitch", "Rugby Pitch", "", new List<int>() { }));
            buildings.Add(new Building("10E", "School of Management", "", new List<int>() { }));
            buildings.Add(new Building("Shooting Range", "Shooting Range", "", new List<int>() { }));
            buildings.Add(new Building("South Football Pitch", "South Football Pitch          at point  X=343643269.549  Y=225989761.315  Z=    0.000", "", new List<int>() { }));
            buildings.Add(new Building("STV", "Sports Training Village", "", new List<int>() { }));
            buildings.Add(new Building("St John's Pitches", "St John's Pitches", "", new List<int>() { }));
            buildings.Add(new Building("The Edge", "The Edge", "", new List<int>() { }));
            buildings.Add(new Building("Lime Tree", "The Lime Tree", "", new List<int>() { }));
            buildings.Add(new Building("SU", "The SU", "", new List<int>() { }));
            buildings.Add(new Building("UH", "University Hall", "", new List<int>() { }));
            buildings.Add(new Building("WH", "Wessex House", "", new List<int>() { }));
            rooms.Add(new Room("2.59", "1W 2.59", "", 2, 96568, false));
            rooms.Add(new Room("C2.51", "1W C3.51", "", 2, 96568, true));
            rooms.Add(new Room("C2.52", "1W C2.52", "", 2, 96568, true));
            rooms.Add(new Room("2.60", "1W 2.60", "", 2, 96568, false));
            rooms.Add(new Room("2.57", "1W 2.57", "", 2, 96568, false));
            rooms.Add(new Room("2.61", "1W 2.61", "", 2, 96568, false));
            rooms.Add(new Room("2.56", "1W 2.56", "", 2, 96568, false));
            rooms.Add(new Room("2.53", "1W 2.53", "", 2, 96568, false));
            rooms.Add(new Room("2.58", "1W 2.58", "", 2, 96568, false));
            rooms.Add(new Room("2.54", "1W 2.54", "", 2, 96568, false));
            rooms.Add(new Room("C2.3", "1W C2.3", "", 2, 96568, true));
            rooms.Add(new Room("C2.2", "1W C2.2", "", 2, 96568, true));
            rooms.Add(new Room("2.04", "1W 2.04", "", 2, 96568, false));
            rooms.Add(new Room("2.05", "1W 2.05", "", 2, 96568, false));
            rooms.Add(new Room("2.08", "1W 2.08", "", 2, 96568, false));
            rooms.Add(new Room("C2.03", "1W C2.03", "", 2, 96568, true));
            rooms.Add(new Room("2.02", "1W 2.02", "", 2, 96568, false));
            rooms.Add(new Room("2.01", "1W 2.01", "", 2, 96568, false));
            rooms.Add(new Room("C2.1", "1W C2.1", "", 2, 96568, true));
            rooms.Add(new Room("2.101", "1W 2.101", "", 2, 96568, false));
            rooms.Add(new Room("2.102", "1W 2.102", "", 2, 96568, false));
            rooms.Add(new Room("2.103", "1W 2.103", "", 2, 96568, false));
            rooms.Add(new Room("2.105", "1W 2.105", "", 2, 96568, false));
            rooms.Add(new Room("C2.101", "1W C2.101", "", 2, 96568, true));
            rooms.Add(new Room("C2.03", "1W C2.03", "", 2, 96568, true));
            rooms.Add(new Room("C2.102", "1W C2.102", "", 2, 96568, true));
            rooms.Add(new Room("2.106", "1W 2.106", "", 2, 96568, false));
            rooms.Add(new Room("2.107", "1W 2.107", "", 2, 96568, false));
            rooms.Add(new Room("2.108", "1W 2.108", "", 2, 96568, false));
            rooms.Add(new Room("2.104", "1W 2.104", "", 2, 96568, false));
            rooms.Add(new Room("C2.4", "1W C2.4", "", 2, 96568, true));
            rooms.Add(new Room("2.09", "1W 2.09", "", 2, 96568, false));
            rooms.Add(new Room("2.11", "1W 2.11", "", 2, 96568, false));
            rooms.Add(new Room("2.13", "1W 2.13", "", 2, 96568, false));
            rooms.Add(new Room("2.15", "1W 2.15", "", 2, 96568, false));
            rooms.Add(new Room("2.17", "1W 2.17", "", 2, 96568, false));
            rooms.Add(new Room("2.19", "1W 2.19", "", 2, 96568, false));
            rooms.Add(new Room("2.21", "1W 2.21", "", 2, 96568, false));
            rooms.Add(new Room("2.23", "1W 2.23", "", 2, 96568, false));
            rooms.Add(new Room("2.24", "1W 2.24", "", 2, 96568, false));
            rooms.Add(new Room("2.22", "1W 2.22", "", 2, 96568, false));
            rooms.Add(new Room("2.20", "1W 2.20", "", 2, 96568, false));
            rooms.Add(new Room("2.18", "1W 2.18", "", 2, 96568, false));
            rooms.Add(new Room("2.16", "1W 2.16", "", 2, 96568, false));
            rooms.Add(new Room("2.14", "1W 2.14", "", 2, 96568, false));
            rooms.Add(new Room("2.12", "1W 2.12", "", 2, 96568, false));

            foreach(Building building in buildings)
            {
                building.Id = rooms[0].BuildingId;
            }
        }
    }
}
