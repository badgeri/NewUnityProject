using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>{
	public bool walkable; // is the current node walkable
	public Vector3 worldPosition; //the position of the node compare to the world
	public int gridX, gridY; // the grid position of the current node;
	public int gCost, hCost;
	public Node parent; 

	public Node(bool _walkable, Vector3 _worldPosition, int _gridX, int _gridY){
		walkable = _walkable;
		worldPosition = _worldPosition;
		gridX = _gridX;
		gridY = _gridY;
	}

	//We will only get fCost and never set it so only a method to get fCost is needed
	public int fCost {
		get { return gCost + hCost; }
	}

	//implement the interface IHeapItem for Node
	public int HeapIndex{
		get { return HeapIndex; }
		set { HeapIndex = value; }
	}

	//return one if the current item has higher priority than nodeToCompareTo
	public int CompareTo(Node nodeToCompareTo){
		int compare = nodeToCompareTo.fCost.CompareTo(fCost);
		if ( compare == 0) 
			compare = nodeToCompareTo.hCost.CompareTo(hCost);
		return compare; //negative since a low [fh]Cost is better
	}
}

