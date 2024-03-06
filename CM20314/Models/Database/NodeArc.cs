using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace CM20314.Models.Database
{
    public class NodeArc : Entity
    {
        public int Node1Id { get; set; }
        public int Node2Id { get; set; }
        public bool StepFree { get; set; }
        public double Cost { get; set; }
        public int NodeArcType { get; set; }
        public bool RequiresUsageRequest { get; set; }
        [NotMapped] public Node Node1 { get; set; }
        [NotMapped] public Node Node2 { get; set; }
        public bool IsMapDisplayablePath { get; set; }


        public NodeArc()
        {

        }
        public NodeArc(Node n1, Node n2, bool stepFree, double cost, NodeArcType type, bool usageRequest, bool isDisplayablePath, bool assignNodes)
        {
            Node1Id = n1.Id;
            Node2Id = n2.Id;
            StepFree = stepFree;
            Cost = cost;
            NodeArcType = (int)type;
            RequiresUsageRequest = usageRequest;
            IsMapDisplayablePath = isDisplayablePath;
            if (assignNodes)
            {
                Node1 = n1;
                Node2 = n2;
            }
        }

        //public Node[] getNodes(int node1ID, int node2ID) { return ?; }
    }
}

