using System;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        private Node node1;
        [NotMapped]
        private Node node2;

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

            this.node1 = n1;
            this.node2 = n2;
        }

        //public Node[] getNodes(int node1ID, int node2ID) { return ?; }

        public bool isStepFree() { return stepFree; }
        public double getCost() { return cost; }
        public NodeArcType getNodeArcType() { return (NodeArcType)nodeArcType; }

        public Node getNode1 () { return node1; }
        public Node getNode2 () { return node2; }

        public int getNode1ID () { return node1ID;  }
        public int getNode2ID () { return node2ID;  }
    }
}

