using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playground : MonoBehaviour {
    public Transform player;
    public bool pathOnly;
    public LayerMask unWalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float safeRadius;
    public bool gridUpdated = false;
    Node[,] grid;
    public int treshhold = 3;
    public int runCase = 0; // for debuging

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;

    void Start() {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    public List<Node> path;
    void OnDrawGizmos() {
        if (pathOnly) {
            if (path != null) {
                foreach (Node n in path) {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
                }
            }
        }
        else {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
            if (grid != null) {
                Node playerNode;
                if (player == null)
                    playerNode = NodeFromWorldPoint(transform.position);
                else
                    playerNode = NodeFromWorldPoint(player.position);
                foreach (Node n in grid) {
                    Gizmos.color = (n.walkable) ? Color.white : Color.red;
                    if (path != null) {
                        if (path.Contains(n))
                            Gizmos.color = Color.black;
                    }
                    if (playerNode == n)
                        Gizmos.color = Color.cyan;
                    Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
                }
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPos) {
        float percentX = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPos.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    void CreateGrid() {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldLeftBottom = transform.position -
            Vector3.right * gridWorldSize.x / 2 -
            Vector3.forward * gridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                Vector3 worldPoint = worldLeftBottom + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, safeRadius, unWalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
                //if (!walkable) print("Create grid " + x + " " + y);
            }
        }
    }

    public void UpdateGrid(GameObject movedObject) {

        //Do not need to update grid if the object is walkable
        if ((1 << movedObject.layer & unWalkableMask) == 0 &&
            movedObject.layer != ExtendLayerMask.GhostMask
            )
            return;
        gridUpdated = true;

        Node n = NodeFromWorldPoint(movedObject.transform.position);
        int Xmin = n.gridX + 1;
        int Xmax = n.gridX;
        int Ymin = n.gridY;
        int Ymax = n.gridY;
        int x, y;

        bool isItSelf = false;
        bool nNonWalkable;
        int left = 0;
        int right = 0;
        int top = 0;
        int bottom = 0;

        if (runCase != 0)
        {
            right = treshhold;
            top = treshhold;
            bottom = treshhold;
            left = treshhold;
        }
        if ((runCase & 1) > 0) right = 0;
        if ((runCase & 2) > 0) left = 0;
        if ((runCase & 4) > 0) top = 0;
        if ((runCase & 8) > 0) bottom = 0;

        while (left < treshhold || right < treshhold || top < treshhold || bottom < treshhold)
        {
            //decrese Y, travers down on left side
            if( left < treshhold)
            {
                if (0 == Xmin) {
                    left = treshhold;
                    x = Xmin;
                }
                else x = --Xmin;
                isItSelf = false;
                for (y = Ymin; y <= Ymax; y++)
                {
                    //print("index1n " + (x) + " " + (y));
                    nNonWalkable = CheckIfNodeIsCollidingWithObject(ref isItSelf, grid[x, y], movedObject);
                    grid[x, y].walkable = !nNonWalkable;
                }
                if (!isItSelf) left++;
                else left = 0;
            }

            //increase X, travers left on bottom side
            if (bottom < treshhold)
            {
                if (0 == Ymin) {
                    top = treshhold;
                    y = Ymin;
                }
                else y = --Ymin;

                isItSelf = false;
                for (x = Xmin; x <= Xmax; x++)
                {
                    //print("index2n " + (x) + " " + (y));
                    nNonWalkable = CheckIfNodeIsCollidingWithObject(ref isItSelf, grid[x, y], movedObject);
                    grid[x, y].walkable = !nNonWalkable;
                }
                if (!isItSelf) bottom++;
                else bottom = 0;
            }

            //increase Y, travers up on right side
            if (right < treshhold)
            {
                if (gridSizeX == Xmax) right = treshhold;
                else
                {
                    x = ++Xmax;

                    isItSelf = false;
                    for (y = Ymin; y <= Ymax; y++)
                    {
                        //print("index3n " + (x) + " " + (y));
                        nNonWalkable = CheckIfNodeIsCollidingWithObject(ref isItSelf, grid[x, y], movedObject);
                        grid[x, y].walkable = !nNonWalkable;
                    }
                    if (!isItSelf) right++;
                    else right = 0;
                }
            }

            //decrese X, travers left on top side
            if (top < treshhold)
            {
                if (gridSizeY == Ymax) top = treshhold;
                else
                {
                    y = ++Ymax;
                    isItSelf = false;
                    for (x = Xmin; x <= Xmax; x++)
                    {
                        //print("index4n " + (x) + " " + (y));
                        nNonWalkable = CheckIfNodeIsCollidingWithObject(ref isItSelf, grid[x, y], movedObject);
                        grid[x, y].walkable = !nNonWalkable;
                    }
                    if (!isItSelf) top++;
                    else top = 0;
                }
            }
            //print(left + " " + bottom + " " + right + " " + top);
        }
    }

    private bool CheckIfNodeIsCollidingWithObject(ref bool isItself, Node n, GameObject gameObject)
    {
        LayerMask mask = unWalkableMask | 1 << gameObject.layer;
        RaycastHit[] hitInfos = Physics.SphereCastAll(n.worldPosition, safeRadius,
            Vector3.up, safeRadius, mask);

        foreach (RaycastHit hitInfo in hitInfos)
        {
            if (hitInfo.collider.gameObject == gameObject)
            {
                isItself = true;
                // if there was only a collision with Ghost, it is not a collision
                if (gameObject.layer == ExtendLayerMask.GhostMask)
                {
                    return hitInfos.Length > 1;
                }
            }
        }
        return hitInfos.Length > 0;
    }

    public List<Node> GetNeigbours(Node node) {
		List<Node> neighbours = new List<Node>();
		for( int x = -1; x <= 1; x++) {
			for ( int y = -1; y <= 1; y++) {
				if(x == 0 && y == 0) continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

                if (checkX > 0 && checkX < gridSizeX && checkY > 0 && checkY < gridSizeY) {
                    neighbours.Add(grid[checkX, checkY]);
                }
			}
		}
		return neighbours;
	}
	
}
