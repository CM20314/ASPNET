namespace CM20314.Models
{
    /// <summary>
    /// Different application start modes (use existing DB, or generate a new DB from files)
    /// </summary>
    public enum StartupMode
    {
        UseExistingDb,
        GenerateDb
    }
}
