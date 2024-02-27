using CM20314.Models.Database;

namespace CM20314.Models
{
    public class NodeArcSplitSet
    {
        public List<Node> Nodes { get; set; } = new List<Node>();
        public List<NodeArc> NodeArcs { get; set; } = new List<NodeArc>();
    }
}
