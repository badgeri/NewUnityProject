using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {


    private float mMaxVelocity = 0.5f;
    private float mCurrentVelocity = 0;

    // Use this for initialization
    void Start () {
    	
    }

    // Update is called once per frame
    private void Update () {
        if (Input.GetMouseButton(0))
        {
            MoveTowardsPressedPoint();
        }
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
