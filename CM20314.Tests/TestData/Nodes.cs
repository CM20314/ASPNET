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
        public static List<Node> testNodes = new List<Node>();
        public static List<NodeArc> testNodeArcs = new List<NodeArc>();
        private static List<Coordinate> testCoordinates = new List<Coordinate>();

        public static void Initialise()
        {
            testNodes = new List<Node>();
            testNodeArcs = new List<NodeArc>();
            testCoordinates = new List<Coordinate>();

            Coordinate c1 = new Coordinate(1.0, 1.0);
            c1.Id = 1;
            Node node1 = new Node(0, 0, c1.Id, "Node 1");
            node1.Id = 1;
            node1.Coordinate = c1;

            Coordinate c2 = new Coordinate(2.0, 1.0);
            c2.Id = 2;
            Node node2 = new Node(0, 0, c2.Id, "Node 2");
            node2.Id = 2;
            node2.Coordinate = c2;

            Coordinate c3 = new Coordinate(3.0, 1.0);
            c3.Id = 3;
            Node node3 = new Node(0, 0, c3.Id, "Node 3");
            node3.Id = 3;
            node3.Coordinate = c3;

            Coordinate c4 = new Coordinate(4.0, 1.0);
            c4.Id = 4;
            Node node4 = new Node(0, 0, c4.Id, "Node 4");
            node4.Id = 4;
            node4.Coordinate = c4;

            Coordinate c5 = new Coordinate(5.0, 1.0);
            c5.Id = 5;
            Node node5 = new Node(0, 0, c5.Id, "Node 5");
            node5.Id = 5;
            node5.Coordinate = c5;

            Coordinate c6 = new Coordinate(6.0, 1.0);
            c6.Id = 6;
            Node node6 = new Node(0, 0, c6.Id, "Node 6");
            node6.Id = 6;
            node6.Coordinate = c6;

            Coordinate c7 = new Coordinate(7.0, 1.0);
            c7.Id = 7;
            Node node7 = new Node(0, 0, c7.Id, "Node 7");
            node7.Id = 7;
            node7.Coordinate = c7;

            Coordinate c8 = new Coordinate(8.0, 1.0);
            c8.Id = 8;
            Node node8 = new Node(0, 0, c8.Id, "Node 8");
            node8.Id = 8;
            node8.Coordinate = c8;

            Coordinate c9 = new Coordinate(9.0, 1.0);
            c9.Id = 9;
            Node node9 = new Node(0, 0, c9.Id, "Node 9");
            node9.Id = 9;
            node9.Coordinate = c9;

            Coordinate c10 = new Coordinate(10.0, 1.0);
            c10.Id = 10;
            Node node10 = new Node(0, 0, c10.Id, "Node 10");
            node10.Id = 10;
            node10.Coordinate = c10;

            Coordinate c11 = new Coordinate(100.0, 1.0);
            c11.Id = 11;
            Node node11 = new Node(0, 0, c11.Id, "Node 11");
            node11.Id = 11;
            node11.Coordinate = c11;

            NodeArc n12 = new NodeArc(node1, node2, true, 4, NodeArcType.Path, false, true);

            NodeArc n21 = new NodeArc(node2, node1, true, 4, NodeArcType.Path, false, true);
            NodeArc n13 = new NodeArc(node1, node3, true, 7, NodeArcType.Path, false, true);
            NodeArc n31 = new NodeArc(node3, node1, true, 7, NodeArcType.Path, false, true);
            NodeArc n41 = new NodeArc(node4, node1, true, 11, NodeArcType.Path, false, true);
            NodeArc n14 = new NodeArc(node1, node4, true, 11, NodeArcType.Path, false, true);

            NodeArc n24 = new NodeArc(node2, node4, true, 7, NodeArcType.Path, false, true);
            NodeArc n42 = new NodeArc(node4, node2, true, 7, NodeArcType.Path, false, true);

            NodeArc n34 = new NodeArc(node3, node4, true, 9, NodeArcType.Path, false, true);
            NodeArc n43 = new NodeArc(node4, node3, true, 9, NodeArcType.Path, false, true);
            NodeArc n36 = new NodeArc(node3, node6, true, 25, NodeArcType.Path, false, true);
            NodeArc n63 = new NodeArc(node6, node3, true, 25, NodeArcType.Path, false, true);
            NodeArc n38 = new NodeArc(node3, node8, true, 20, NodeArcType.Path, false, true);
            NodeArc n83 = new NodeArc(node8, node3, true, 20, NodeArcType.Path, false, true);

            NodeArc n45 = new NodeArc(node4, node5, true, 25, NodeArcType.Path, false, true);
            NodeArc n54 = new NodeArc(node5, node4, true, 25, NodeArcType.Path, false, true);
            NodeArc n47 = new NodeArc(node4, node7, true, 30, NodeArcType.Path, false, true);
            NodeArc n74 = new NodeArc(node7, node4, true, 30, NodeArcType.Path, false, true);

            NodeArc n69 = new NodeArc(node6, node9, true, 23, NodeArcType.Path, false, true);
            NodeArc n96 = new NodeArc(node9, node6, true, 23, NodeArcType.Path, false, true);

            NodeArc n710 = new NodeArc(node7, node10, true, 30, NodeArcType.Path, false, true);
            NodeArc n107 = new NodeArc(node10, node7, true, 30, NodeArcType.Path, false, true);

            NodeArc n86 = new NodeArc(node8, node6, true, 20, NodeArcType.Path, false, true);
            NodeArc n68 = new NodeArc(node6, node8, true, 20, NodeArcType.Path, false, true);

            NodeArc n910 = new NodeArc(node9, node10, true, 50, NodeArcType.Path, false, true);
            NodeArc n109 = new NodeArc(node10, node9, true, 50, NodeArcType.Path, false, true);

            testNodes.Add(node1);
            testNodes.Add(node2);
            testNodes.Add(node3);
            testNodes.Add(node4);
            testNodes.Add(node5);
            testNodes.Add(node6);
            testNodes.Add(node7);
            testNodes.Add(node8);
            testNodes.Add(node9);
            testNodes.Add(node10);
            testNodes.Add(node11);

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
