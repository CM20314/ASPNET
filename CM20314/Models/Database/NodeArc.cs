using System;
namespace CM20314.Models.Database
{
	public class NodeArc : Entity
	{
        private int         node1ID;
        private int         node2ID;
        private bool        stepFree;
        private float       cost;
        private NodeArcType nodeArcType;
        private bool        requiresUsageRequest;

        public NodeArc(Node n1, Node n2, bool steps, float c, NodeArcType type, bool usageRequest)
        {
            node1ID              = n1.Id;
            node2ID              = n2.Id;
            stepFree             = steps;
            cost                 = c;
            nodeArcType          = type;
            requiresUsageRequest = usageRequest;
        }

        //public Node[] getNodes(int node1ID, int node2ID) { return ?; }

        public bool isStepFree() { return stepFree; }
        public float getCost() { return cost; }
        public NodeArcType getNodeArcType() {  return nodeArcType; }
    }
}

