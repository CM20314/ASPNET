using CM20314.Models.Database;

namespace CM20314.Models
{
    /// <summary>
    /// Represents a client container search response
    /// </summary>
    public class SearchResponseData
    {
        public List<Container> Results { get; set; } = new List<Container>();
    }
}
