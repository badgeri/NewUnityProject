using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Pathfinding : NetworkBehaviour
{

	public Playground grid;

    private float mMaxVelocity = 10f;
    private float mCurrentVelocity = 0;
    private Vector3 dirToFirstNodeInPath = new Vector3();
    private Vector3 targetPosition = new Vector3();
    private bool isOrderedToMove = false;
    private LayerMask notGroundMask = 0;
    private bool objectIsSelected = false;
    private bool isAllowedToMove = false;
    private static GameObject player_canvas = null;
    private static GameObject target_canvas = null;
    private static int selectedOwner = 0;

    //private GameObject relatedPlayerConnectionObject;

    // Use this for initialization
    void Start()
    {
        // Remember: Update runs on everyones computer, whether or not they own this particular player object.
        grid = GameObject.FindWithTag("Playground").GetComponent<Playground>();
        notGroundMask = 1 << ExtendLayerMask.UI | 1 << ExtendLayerMask.UnWalkable;
        if (target_canvas == null && player_canvas == null)
        {
            GameObject parent = new GameObject();
            parent.name = "LocaterCanvases";
            target_canvas = createLocator(parent, Color.red, false);
            player_canvas = createLocator(parent, Color.blue, false);
            setActivePlayerLocator(false);
            setActiveTargetLocator(Vector3.zero, false);
        }
    }

    void Update(){
        // Remember: Update runs on everyones computer, whether or not they own this particular player object.
        if (GetComponent<NetworkIdentity>() != null)
        {
            if (hasAuthority == false)
            {
                return;
            }

            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject gObject in gameObjects)
            {
                if (GetComponentInParent<PlayerUnit>().parentNetworkId == gObject.GetComponent<PlayerConnectionObjectScript>().netId)
                {
                    if (!gObject.GetComponent<PlayerConnectionObjectScript>().getIsPlayersTurn())
                    {
                        return;
                    }
                }
            }
        }

        //find new mouse input
        if (Input.GetMouseButtonDown(0)) {
            if (IsOwner())
            {
                if (selectedObject())
                {
                    if (isAllowedToMove)
                    {
                        Vector3? tmp = getPositionFromMouseClick();
                        if (tmp.HasValue) // check if it is a double click on same
                        {
                            Node n1 = grid.NodeFromWorldPoint(tmp.Value);
                            Node n2 = grid.NodeFromWorldPoint(targetPosition);
                            if (Node.Equal(n1, n2))
                            {
                                isOrderedToMove = true;
                            }
                            else
                            {
                                targetPosition = tmp.Value;
                                isOrderedToMove = false;
                                FindPath(transform.position, targetPosition);
                                setActiveTargetLocator(targetPosition, true);
                            }
                        }
                    }
                    else
                    {
                        isAllowedToMove = setPositionFromMouseClick();
                        if (isAllowedToMove)
                        {
                            FindPath(transform.position, targetPosition);
                            setActiveTargetLocator(targetPosition, true);
                        }
                    }
                }
                else
                {
                    setActiveTargetLocator(Vector3.zero, false);
                    isAllowedToMove = false;
                    isOrderedToMove = false;
                }
            }
            else
            {
                selectedObject(); // to select object first
                setActiveTargetLocator(Vector3.zero, false);
                isAllowedToMove = false;
                isOrderedToMove = false;
            }
        }

        //if grid has been updated
        if (grid.gridUpdated)
        {
            grid.gridUpdated = false;
            FindPath(transform.position, targetPosition);
        }

        //if we have a grid path
        if (grid.path != null && isOrderedToMove && IsOwner())
        {
            UpdateTravelDirection(grid.path);

            if (grid.path.Count > 0)
            {
                CalculateAndUpdateVelocity(dirToFirstNodeInPath);
            }
            else
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                isOrderedToMove = false;
                setActiveTargetLocator(Vector3.zero, false);
            }
        }
        else if (GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            isOrderedToMove = false;
        }
    }

    private bool IsOwner()
    {
        return selectedOwner == transform.GetInstanceID();
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

			foreach (Node neighbour in grid.GetNeigbours(currentNode)){
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
    private Vector3 mPathDir = new Vector3();
    private float fTextLabelHeight = 10f;

    private void UpdateTravelDirection(List<Node> path)
    {
        if (path.Count == 0) return;

        //TODO ether shoot a ray from transform.position to the grid to 
        //     get the proper y value, or make sure that the gameObjects 
        //     transform.position.y is on the bottom of the object and 
        //     then the dirToFirstNodeInPath.y = 0 can be ignored
        dirToFirstNodeInPath = path[0].worldPosition - transform.position;
        dirToFirstNodeInPath.y = 0;
        dirToFirstNodeInPath.Normalize();
        if (path.Count > 1)
        {
            mPathDir = path[1].worldPosition - path[0].worldPosition;
            mPathDir.y = 0;
            mPathDir.Normalize();
        }
        else if( mPathDir.magnitude == 0) //in case the path is really short
        {
            mPathDir = path[0].worldPosition - transform.position;
            mPathDir.y = 0;
            mPathDir.Normalize();
        }

        // if Dot < 0, vectors are in opposite direction, recalculate new direction
        // meaning that we have past path[0] and need to aim for the next node in path
        if ( Vector3.Dot(dirToFirstNodeInPath, mPathDir) < 0)
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

    private GameObject createLocator(GameObject parent, Color color, bool useWorldSpace)
    {
        string objectName = "CircularCanvas" + color;

        GameObject g = new GameObject();
        Canvas canvas = g.AddComponent<Canvas>();
        CanvasScaler cs = g.AddComponent<CanvasScaler>();
        GraphicRaycaster gr = g.AddComponent<GraphicRaycaster>();
        g.name = objectName;
        g.transform.SetParent(parent.transform, false);
        g.transform.localPosition = new Vector3(0f, 0f, 0f);
        g.transform.localScale = new Vector3(
                                             1.0f / this.transform.localScale.x * 1f,
                                             1.0f / this.transform.localScale.y * 1f,
                                             1.0f / this.transform.localScale.z * 1f);
        canvas.renderMode = RenderMode.WorldSpace;
        cs.scaleFactor = 10.0f;
        cs.dynamicPixelsPerUnit = 10f;
        g.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 3.0f);
        g.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 3.0f);
        GameObject g2 = new GameObject();
        LineRenderer lineRenderer = g2.AddComponent<LineRenderer>();
        g2.name = "Circle";
        g2.transform.SetParent(g.transform, false);

        float theta_scale = 0.1f;             //Set lower to add more points
        int size = (int)((2.0 * Mathf.PI) / theta_scale); //Total number of points in circle.

        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.positionCount = size;
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = useWorldSpace;

        float theta = 0;
        float x, y;
        float r = 1.2f;
        for (int i = 0; i < size; i++, theta += theta_scale )
        {
            x = r * Mathf.Cos(theta);
            y = r * Mathf.Sin(theta);

            Vector3 pos = new Vector3(x, 0, y);
            lineRenderer.SetPosition(i, pos);
        }
        return g;
    }

    private void setActiveTargetLocator(Vector3 pos, bool active)
    {
        if (target_canvas == null) return;
        if (selectedOwner == transform.GetInstanceID())
        {
            target_canvas.transform.position = pos + Vector3.up * 0.5f;
            target_canvas.SetActive(active);
        }
        else if( selectedOwner == 0)
            target_canvas.SetActive(false);
    }

    private void setActivePlayerLocator(bool active)
    {
        if (player_canvas == null) return; //sanity
        if (selectedOwner == transform.GetInstanceID())
        {
            player_canvas.transform.SetParent(transform);
            player_canvas.transform.localPosition = Vector3.up * 0.5f;
            player_canvas.SetActive(active);
        }
        else if( selectedOwner == 0)
            player_canvas.SetActive(false);
            
    }

    private bool selectedObject()
    {
        //If the ray hits an object or hitInfo is up to date
        if (RaycastHandler.Update())
        {
            if (RaycastHandler.hitInfo.transform.gameObject == gameObject)
            {
                objectIsSelected = !objectIsSelected;
                if (objectIsSelected)
                    selectedOwner = transform.GetInstanceID();
                else selectedOwner = 0;

                setActivePlayerLocator(objectIsSelected);
            }
            else if (RaycastHandler.hitInfo.transform.gameObject.GetComponent<Pathfinding>())
            {
                objectIsSelected = false;
            }
        }
        return objectIsSelected;
    }
    
    private Vector3? getPositionFromMouseClick()
    {
        if (RaycastHandler.Update())
        {
            if (((1 << RaycastHandler.hitInfo.transform.gameObject.layer) & notGroundMask) == 0)
            {
                return RaycastHandler.hitInfo.point;
            }
        }
        return null;
    }
    private bool setPositionFromMouseClick()
    {
        //If the ray hits an object
        if (RaycastHandler.Update())
        {
            if (((1 << RaycastHandler.hitInfo.transform.gameObject.layer) & notGroundMask) == 0)
            {
                targetPosition = RaycastHandler.hitInfo.point;
                return true;
            }
        }
        return false;
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
