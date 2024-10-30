namespace CM20314.Models
{
    /// <summary>
    /// Represents data to translate and scale coordinates to map from one space to another
    /// </summary>
    public class MapOffset
    {
        public double OffX { get; set; }
        public double OffY { get; set; }
        public double Scale { get; set; }

        public MapOffset()
        {
            OffX = 0;
            OffY = 0;
            Scale = 1;
        }
        public MapOffset(double offX, double offY, double scale = 1)
        {
            this.Scale = scale;
            this.OffX = offX;
            this.OffY = offY;
        }
    }
}
