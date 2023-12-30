using CM20314.Models;

namespace CM20314.Services
{
    public static class Constants
    {
        public static StartupMode STARTUP_MODE = StartupMode.GenerateDb;
        public static class SourceFilePaths
        {
            public const string ROOT = "Data\\Raw";

            public const string FILE_EXTENSION_DEFAULT = ".txt";
            public const int FLOOR_OUTDOOR = -100;

            public const string PATHS_FILENAME = "Paths";
            public const string FLOOR_FILENAME_BOUNDARY = "Boundary";
            public const string FLOOR_FILENAME_CONTAINERS = "Containers";
            public const string FLOOR_FILENAME_EXTERNAL_LINKS = "ExternalLinks";
            public const string FLOOR_FILENAME_INTERNAL_LINKS = "InternalLinks";
            public const string FLOOR_FILENAME_PATHS = "Paths";

            public const string FLOOR_CORRIDOR_PREFIX = "C";
            public const string BUILDING_FLOOR_SEPARATOR = "-";

            public static List<string> BUILDING_NAMES = new List<string>()
            {
                "10W", "1E", "1S", "1W", "1WN", "2E", "2S", "2W", "3E", "3GPitch", "3S", "3W", "3WA", "3WN", "4E", "4ES", "4S", "4SA", "4W", "4WCafe", "5S", "5W", "6E", "6W", "6WS", "7W", "8E", "8W_Main", "8W_Secondary", "9W", "AstroPitch", "AthleticsTrack", "BeachVolleyball", "BobsleighTrack", "ChancellorsBuilding", "Chaplaincy", "ClayCourt", "EastBuilding", "EastwoodPitches", "Estates", "FoundlersHall", "HockeyPitch", "Library", "LimekilnPitches", "MedicalCentre", "NorwoodHouse", "OutdoorTennisCourts", "RugbyPitch", "SchoolOfManagement", "ShootingRange", "SportsPitch", "SportsTrainingVillage", "StJohnsPitches", "TheEdge", "TheLimeTree", "TheSU", "UniversityHall", "WessexHouse"
            };

            public static Dictionary<string, List<int>> BUILDING_FLOORS = new Dictionary<string, List<int>>()
            {
                { "1W", new List<int> { 2 } }
            };
        }
    }
}
