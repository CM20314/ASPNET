namespace CM20314.Models
{
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
