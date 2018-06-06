using UnityEngine;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

	public Playground grid;

    private float mMaxVelocity = 0.5f;
    private float mCurrentVelocity = 0;
    private Vector3 dirToFirstNodeInPath = new Vector3();

	void Update(){
        if (Input.GetMouseButtonDown(0)) {
            Node n = getNodeOfMouseClick();
            if (n != null)
                FindPath(transform.position, n.worldPosition);
		}

        if(grid.path != null)
        {
            UpdateTravelDirection(grid.path);

            if(grid.path.Count > 0) {
                CalculateAndUpdateVelocity(dirToFirstNodeInPath);
            }
            else {
                GetComponent<Rigidbody>().velocity.Set(0, 0, 0);
            }
        }
    }

	void FindPath (Vector3 startPos, Vector3 targetPos) {
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);
		List<Node> openSet = new List<Node>();
		HashSet<Node> closedSet = new HashSet<Node>();

		openSet.Add(startNode);

		while(openSet.Count > 0){
			Node currentNode = openSet[0];
			for ( int i = 1; i < openSet.Count; i++) {
				if( openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) {
					currentNode = openSet[i];
				}
			}
			openSet.Remove(currentNode);
			closedSet.Add(currentNode);

			if( currentNode == targetNode){
				RetracePath(startNode, targetNode);
				return;
			}

			foreach (Node neighbour in grid.getNeigbours(currentNode)){
				if( !neighbour.walkable || closedSet.Contains(neighbour)){
					continue;
				}

				int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
				if( newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
					neighbour.gCost = newMovementCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = currentNode;

					if( !openSet.Contains(neighbour)) 
						openSet.Add(neighbour);
				}
			}
		}
	}

	void RetracePath(Node startNode, Node endNode){
		List<Node> path = new List<Node>();	
		Node currentNode = endNode;
		while ( currentNode != startNode){
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();
		grid.path = path;
	}

	
	int GetDistance(Node nodeA, Node nodeB){
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14 * dstY + 10 * (dstX-dstY);
		return 14 * dstX - 10 * (dstX-dstY);
	}

    /// <summary>
    /// Returns  true if path[0].position is passed
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private void UpdateTravelDirection(List<Node> path)
    {
        if (path.Count == 0) return;

        Vector3 pathDir = new Vector3();
        dirToFirstNodeInPath = path[0].worldPosition - transform.position;
        dirToFirstNodeInPath.Normalize();
        if (path.Count > 1)
        {
            pathDir = path[1].worldPosition - path[0].worldPosition;
            pathDir.Normalize();
        }

        // if Dot < 0, vectors are in opposite direction, recalculate new direction
        // meaning that we have past path[0] and need to aim for the next node in path
        if ( Vector3.Dot(dirToFirstNodeInPath, pathDir) < 0)
        {
            path.RemoveAt(0);
            UpdateTravelDirection(path);
        }
    }

    /// <summary>
    /// Get direction of mouse click
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private void getDirectionToNode(ref Vector3 direction, Node node)
    {
        direction = node.worldPosition - transform.position;
        direction.Normalize();
    }
    
    private Node getNodeOfMouseClick()
    {
        RaycastHit hit;
        //If the ray hits an object
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            return grid.NodeFromWorldPoint(hit.point);
        }
        return null;
    }
    
    /// <summary>
    /// Get direction of mouse click
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private bool getDirectionOfMouseClick(ref Vector3 direction)
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            //If the ray hits an object
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                Vector3 g = hit.point;
                Rigidbody rb = GetComponent<Rigidbody>();

                var heading = hit.point - transform.position;
                var distance = heading.magnitude;
                direction = heading / distance;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Calculate and update velocity based on a direction.
    /// </summary>
    /// <param name="direction"></param>
    private void CalculateAndUpdateVelocity(Vector3 direction)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        mCurrentVelocity = Mathf.Min(rb.velocity.magnitude + 3, mMaxVelocity);
        float angle = Mathf.Atan2(direction.z, direction.x);
        float Vz = Mathf.Sin(angle) * mCurrentVelocity;
        float Vx = Mathf.Cos(angle) * mCurrentVelocity;
        rb.velocity = new Vector3(Vx, rb.velocity.y, Vz);
    }
}
