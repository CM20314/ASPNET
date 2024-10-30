using CM20314.Models.Database;

namespace CM20314.Models
{
    /// <summary>
    /// A set of node and node arcs to process after splitting a node arc into more nodes and node arcs
    /// </summary>
    public class NodeArcSplitSet
    {
        public List<Node> Nodes { get; set; } = new List<Node>();
        public List<NodeArc> NodeArcs { get; set; } = new List<NodeArc>();
    }
}
