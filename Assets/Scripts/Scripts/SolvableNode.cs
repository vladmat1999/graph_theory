using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolvableNode
{
    public static HashSet<SolvableNode> allNodes = new HashSet<SolvableNode>();
   HashSet<SolvableNode> nodes;
   HashSet<SolvableNode> connections;

   public SolvableNode(HashSet<SolvableNode> newNodes, HashSet<SolvableNode> newConnections)
   {
       nodes = newNodes;
       connections = newConnections;
   }


}
