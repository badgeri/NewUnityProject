using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playground : MonoBehaviour {
	public LayerMask unWalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	Node[,] grid;
	
	float nodeDiameter;
	int gridSizeX;
	int gridSizeY;

	void Start (){
		nodeDiameter = nodeRadius*2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
		CreateGrid();
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
		if( grid != null) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable)?Color.white:Color.red;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
			}
		}
	}
	
	void CreateGrid(){
		grid = new Node[gridSizeX,gridSizeY];
		Vector3 worldLeftBottom = transform.position - 
			Vector3.right*gridWorldSize.x/2 - 
			Vector3.forward*gridWorldSize.y/2;
		for ( int x = 0; x < gridSizeX; x++)
		{
			for ( int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = worldLeftBottom + Vector3.right*(x*nodeDiameter + nodeRadius) + Vector3.forward*(y*nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unWalkableMask));
				if(!walkable){
					print("print walkable");
					print(walkable);
				}
				grid[x,y] = new Node(walkable, worldPoint);

			}
		}
	}
	
}
