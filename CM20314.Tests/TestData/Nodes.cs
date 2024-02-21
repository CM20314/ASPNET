using CM20314.Models;
using CM20314.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM20314.Tests.TestData
{
    public static class Nodes
    {
        public static List<DijkstraNode> testDijkstraNodes = new List<DijkstraNode>();
        public static List<Node> testNodes = new List<Node>();
        public static List<NodeArc> testNodeArcs = new List<NodeArc>();
        private static List<Coordinate> testCoordinates = new List<Coordinate>();

        public static void Initialise()
        {
            testDijkstraNodes = new List<DijkstraNode>();
            testNodeArcs = new List<NodeArc>();
            testCoordinates = new List<Coordinate>();

            Coordinate c1 = new Coordinate(1.0, 1.0);
            c1.Id = 1;
            DijkstraNode node1 = new DijkstraNode(0, 0, c1.Id, "Node 1");
            node1.Id = 1;
            node1.Coordinate = c1;

            Coordinate c2 = new Coordinate(2.0, 1.0);
            c2.Id = 2;
            DijkstraNode node2 = new DijkstraNode(0, 0, c2.Id, "Node 2");
            node2.Id = 2;
            node2.Coordinate = c2;

            Coordinate c3 = new Coordinate(3.0, 1.0);
            c3.Id = 3;
            DijkstraNode node3 = new DijkstraNode(0, 0, c3.Id, "Node 3");
            node3.Id = 3;
            node3.Coordinate = c3;

            Coordinate c4 = new Coordinate(4.0, 1.0);
            c4.Id = 4;
            DijkstraNode node4 = new DijkstraNode(0, 0, c4.Id, "Node 4");
            node4.Id = 4;
            node4.Coordinate = c4;

            Coordinate c5 = new Coordinate(5.0, 1.0);
            c5.Id = 5;
            DijkstraNode node5 = new DijkstraNode(0, 0, c5.Id, "Node 5");
            node5.Id = 5;
            node5.Coordinate = c5;

            Coordinate c6 = new Coordinate(6.0, 1.0);
            c6.Id = 6;
            DijkstraNode node6 = new DijkstraNode(0, 0, c6.Id, "Node 6");
            node6.Id = 6;
            node6.Coordinate = c6;

            Coordinate c7 = new Coordinate(7.0, 1.0);
            c7.Id = 7;
            DijkstraNode node7 = new DijkstraNode(0, 0, c7.Id, "Node 7");
            node7.Id = 7;
            node7.Coordinate = c7;

            Coordinate c8 = new Coordinate(8.0, 1.0);
            c8.Id = 8;
            DijkstraNode node8 = new DijkstraNode(0, 0, c8.Id, "Node 8");
            node8.Id = 8;
            node8.Coordinate = c8;

            Coordinate c9 = new Coordinate(9.0, 1.0);
            c9.Id = 9;
            DijkstraNode node9 = new DijkstraNode(0, 0, c9.Id, "Node 9");
            node9.Id = 9;
            node9.Coordinate = c9;

            Coordinate c10 = new Coordinate(10.0, 1.0);
            c10.Id = 10;
            DijkstraNode node10 = new DijkstraNode(0, 0, c10.Id, "Node 10");
            node10.Id = 10;
            node10.Coordinate = c10;

            Coordinate c11 = new Coordinate(100.0, 1.0);
            c11.Id = 11;
            DijkstraNode node11 = new DijkstraNode(0, 0, c11.Id, "Node 11");
            node11.Id = 10;
            node11.Coordinate = c11;

            NodeArc n12 = new NodeArc(node1, node2, true, 4, NodeArcType.Path, false);
            NodeArc n21 = new NodeArc(node2, node1, true, 4, NodeArcType.Path, false);
            NodeArc n13 = new NodeArc(node1, node3, true, 7, NodeArcType.Path, false);
            NodeArc n31 = new NodeArc(node3, node1, true, 7, NodeArcType.Path, false);
            NodeArc n41 = new NodeArc(node4, node1, true, 11, NodeArcType.Path, false);
            NodeArc n14 = new NodeArc(node1, node4, true, 11, NodeArcType.Path, false);

            NodeArc n24 = new NodeArc(node2, node4, true, 7, NodeArcType.Path, false);
            NodeArc n42 = new NodeArc(node4, node2, true, 7, NodeArcType.Path, false);

            NodeArc n34 = new NodeArc(node3, node4, true, 9, NodeArcType.Path, false);
            NodeArc n43 = new NodeArc(node4, node3, true, 9, NodeArcType.Path, false);
            NodeArc n36 = new NodeArc(node3, node6, true, 25, NodeArcType.Path, false);
            NodeArc n63 = new NodeArc(node6, node3, true, 25, NodeArcType.Path, false);
            NodeArc n38 = new NodeArc(node3, node8, true, 20, NodeArcType.Path, false);
            NodeArc n83 = new NodeArc(node8, node3, true, 20, NodeArcType.Path, false);

            NodeArc n45 = new NodeArc(node4, node5, true, 25, NodeArcType.Path, false);
            NodeArc n54 = new NodeArc(node5, node4, true, 25, NodeArcType.Path, false);
            NodeArc n47 = new NodeArc(node4, node7, true, 30, NodeArcType.Path, false);
            NodeArc n74 = new NodeArc(node7, node4, true, 30, NodeArcType.Path, false);

            NodeArc n69 = new NodeArc(node6, node9, true, 23, NodeArcType.Path, false);
            NodeArc n96 = new NodeArc(node9, node6, true, 23, NodeArcType.Path, false);

            NodeArc n710 = new NodeArc(node7, node10, true, 30, NodeArcType.Path, false);
            NodeArc n107 = new NodeArc(node10, node7, true, 30, NodeArcType.Path, false);

            NodeArc n86 = new NodeArc(node8, node6, true, 20, NodeArcType.Path, false);
            NodeArc n68 = new NodeArc(node6, node8, true, 20, NodeArcType.Path, false);

            NodeArc n910 = new NodeArc(node9, node10, true, 50, NodeArcType.Path, false);
            NodeArc n109 = new NodeArc(node10, node9, true, 50, NodeArcType.Path, false);

            testDijkstraNodes.Add(node1);
            testDijkstraNodes.Add(node2);
            testDijkstraNodes.Add(node3);
            testDijkstraNodes.Add(node4);
            testDijkstraNodes.Add(node5);
            testDijkstraNodes.Add(node6);
            testDijkstraNodes.Add(node7);
            testDijkstraNodes.Add(node8);
            testDijkstraNodes.Add(node9);
            testDijkstraNodes.Add(node10);
            testDijkstraNodes.Add(node11);

            testCoordinates.Add(c1);
            testCoordinates.Add(c2);
            testCoordinates.Add(c3);
            testCoordinates.Add(c4);
            testCoordinates.Add(c5);
            testCoordinates.Add(c6);
            testCoordinates.Add(c7);
            testCoordinates.Add(c8);
            testCoordinates.Add(c9);
            testCoordinates.Add(c10);

            testNodeArcs.Add(n12);
            testNodeArcs.Add(n13);
            testNodeArcs.Add(n14);

            //Make NodeArcs undirected
            testNodeArcs.Add(n41);
            testNodeArcs.Add(n31);
            testNodeArcs.Add(n21);
            
            testNodeArcs.Add(n24);
            testNodeArcs.Add(n42);
            
            testNodeArcs.Add(n34);
            testNodeArcs.Add(n36);
            testNodeArcs.Add(n38);

            //Make NodeArcs undirected
            testNodeArcs.Add(n43);
            testNodeArcs.Add(n63);
            testNodeArcs.Add(n83);

            testNodeArcs.Add(n47);
            testNodeArcs.Add(n45);

            //Make NodeArcs undirected
            testNodeArcs.Add(n54);
            testNodeArcs.Add(n74);

            testNodeArcs.Add(n69);
            //Make NodeArc undirected
            testNodeArcs.Add(n96);

            testNodeArcs.Add(n710);
            //Make NodeArc undirected
            testNodeArcs.Add(n107);

            testNodeArcs.Add(n86);
            //Make NodeArc undirected
            testNodeArcs.Add(n68);

            testNodeArcs.Add(n910);
            //Make NodeArc undirected
            testNodeArcs.Add(n109);
        }
    }
}
