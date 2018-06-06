﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playground : MonoBehaviour {
	public Transform player;
	public bool pathOnly;
	public LayerMask unWalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	public float safeRadius;
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

	public List<Node> path;
	void OnDrawGizmos(){
		if( pathOnly ){
			if( path != null) {
				foreach (Node n in path) {
					Gizmos.color = Color.black;
					Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
				}
			}
		}
		else {
			Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
			if( grid != null) {
				Node playerNode = NodeFromWorldPoint(player.position);
				foreach (Node n in grid) {
					Gizmos.color = (n.walkable)?Color.white:Color.red;
					if( path != null){
						if( path.Contains(n))
							Gizmos.color = Color.black;
					}
					if( playerNode == n )
						Gizmos.color = Color.cyan;
					Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
				}
			}
		}
	}

	public Node NodeFromWorldPoint(Vector3 worldPos){
		float percentX = (worldPos.x + gridWorldSize.x/2)/gridWorldSize.x;
		float percentY = (worldPos.z + gridWorldSize.y/2)/gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX-1)*percentX);
		int y = Mathf.RoundToInt((gridSizeY-1)*percentY);

		return grid[x, y];
	}
	
	void CreateGrid(){
		grid = new Node[gridSizeX,gridSizeY];
		Vector3 worldLeftBottom = transform.position - 
			Vector3.right*gridWorldSize.x/2 - 
			Vector3.forward*gridWorldSize.y/2;
		for ( int x = 0; x < gridSizeX; x++) {
			for ( int y = 0; y < gridSizeY; y++) {
				Vector3 worldPoint = worldLeftBottom + Vector3.right*(x*nodeDiameter + nodeRadius) + Vector3.forward*(y*nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint, safeRadius, unWalkableMask));
				grid[x,y] = new Node(walkable, worldPoint, x, y);
			}
		}
	}

	public List<Node> getNeigbours(Node node) {
		List<Node> neighbours = new List<Node>();
		for( int x = -1; x <= 1; x++) {
			for ( int y = -1; y <= 1; y++) {
				if(x == 0 && y == 0) continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if( checkX > 0 && checkX < gridSizeX && checkY > 0 && checkY < gridSizeY ) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}
		return neighbours;
	}
	
}