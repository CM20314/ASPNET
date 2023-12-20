using CM20314.Services;

namespace CM20314.Data
{
    public static class DbInitialiser
    {
        public static void Initialise()
        {
            switch (Constants.StartupMode)
            {
                case Models.StartupMode.UseExistingDb:
                    break;
                case Models.StartupMode.GenerateDb:
                    GenerateDbFromFiles();
                    break;
            }
        }

        public static void GenerateDbFromFiles()
        {

        }
    }
}
