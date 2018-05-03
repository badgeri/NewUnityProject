using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mouse navigation
/// </summary>
public class MouseNavigation : MonoBehaviour {

    private float mMaxVelocity = 10.0f;
    private float mCurrentVelocity = 0;

    /// <summary>
    /// Gametick
    /// </summary>
    private void Update()
    {
        MoveTowardsPressedPoint();
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
        var angle = Mathf.Atan2(direction.z, direction.x);
        var Vz = Mathf.Sin(angle) * mCurrentVelocity;
        var Vx = Mathf.Cos(angle) * mCurrentVelocity;
        rb.velocity = new Vector3(Vx, rb.velocity.y, Vz);
    }
}
