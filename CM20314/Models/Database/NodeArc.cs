using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace CM20314.Models.Database
{
    public class NodeArc : Entity
    {
        private int node1Id;
        private int node2Id;
        private bool stepFree;
        private double cost;
        private int nodeArcType;
        private bool requiresUsageRequest;

        public NodeArc()
        {

        }
        public NodeArc(Node n1, Node n2, bool stepFree, double cost, NodeArcType type, bool usageRequest)
        {
            node1Id = n1.Id;
            node2Id = n2.Id;
            this.stepFree = stepFree;
            this.cost = cost;
            nodeArcType = (int)type;
            requiresUsageRequest = usageRequest;
        }

        //public Node[] getNodes(int node1ID, int node2ID) { return ?; }

        public bool isStepFree() { return stepFree; }
        public double getCost() { return cost; }
        public NodeArcType getNodeArcType() { return (NodeArcType)nodeArcType; }
        public int getNode1ID () { return node1Id;  }
        public int getNode2ID () { return node2Id;  }
    }
}

