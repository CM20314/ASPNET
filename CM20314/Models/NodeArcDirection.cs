using CM20314.Models.Database;

namespace CM20314.Models
{
    public class NodeArcDirection
    {
        public NodeArc NodeArc { get; set; }
        public string Direction { get; set; }

        public NodeArcDirection(NodeArc nodeArc, string direction) 
        {
            NodeArc = nodeArc;
            Direction = direction;
        }
    }
}
