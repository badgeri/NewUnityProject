using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle collisions for Player Ball
/// </summary>
public class OnCollision : MonoBehaviour {

    /// <summary>
    /// Entering collision.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "Plane")
        {
            GetComponent<Jumping>().setTouchingGround(true);
        }
	}

    /// <summary>
    /// Exit collision
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Plane")
        {
            GetComponent<Jumping>().setTouchingGround(false);
        }
    }
}
