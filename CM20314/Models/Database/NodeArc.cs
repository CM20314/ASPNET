using System;
namespace CM20314.Models.Database
{
    public class NodeArc : Entity
    {
        private int node1ID;
        private int node2ID;
        private bool stepFree;
        private double cost;
        private int nodeArcType;
        private bool requiresUsageRequest;

        public NodeArc()
        {

        }
        public NodeArc(Node n1, Node n2, bool stepFree, double cost, NodeArcType type, bool usageRequest)
        {
            node1ID = n1.Id;
            node2ID = n2.Id;
            this.stepFree = stepFree;
            this.cost = cost;
            nodeArcType = (int)type;
            requiresUsageRequest = usageRequest;
        }

        //public Node[] getNodes(int node1ID, int node2ID) { return ?; }

        public bool isStepFree() { return stepFree; }
        public double getCost() { return cost; }
        public NodeArcType getNodeArcType() { return (NodeArcType)nodeArcType; }
    }
}

