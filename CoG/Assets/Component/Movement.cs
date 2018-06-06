using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float treshHold;
    private Playground grid { get { return GetComponent<Pathfinding>().grid; } }

    private float mMaxVelocity = 0.5f;
    private float mCurrentVelocity = 0;
    private Vector3 dirToNode1 = new Vector3();
    private bool dirToNodeNeedUpdate = true;

    // Update is called once per frame
    private void Update () {

        if(grid.path != null)
        {
            UpdateTravelDirection(grid.path);

            if(grid.path.Count > 0) {
                CalculateAndUpdateVelocity(dirToNode1);
            }
            else {
                GetComponent<Rigidbody>().velocity.Set(0, 0, 0);
            }
        }
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
        dirToNode1 = path[0].worldPosition - transform.position;
        dirToNode1.Normalize();
        if (path.Count > 1 && dirToNodeNeedUpdate)
        {
            pathDir = path[1].worldPosition - path[0].worldPosition;
            pathDir.Normalize();
        }

        // if Dot < 0, vectors are in opposite direction, recalculate new direction
        // meaning that we have past path[0] and need to aim for the next node in path
        if ( Vector3.Dot(dirToNode1, pathDir) < 0)
        {
            path.RemoveAt(0);
            UpdateTravelDirection(path);
        }
        print("current dif = " + (transform.position - path[0].worldPosition));
        print("current vector " + pathDir);
    }

    /// <summary>
    /// Move towards pressed point
    /// </summary>
    private void MoveTowardsPressedPoint()
    {
        Vector3 direction = new Vector3();
        if (getDirectionOfMouseClick(ref direction))
        {
            CalculateAndUpdateVelocity(direction);
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
    
    private bool getNodeOfMouseClick(ref Node node)
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            //If the ray hits an object
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                node = grid.NodeFromWorldPoint(hit.point);
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
