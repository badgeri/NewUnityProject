using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Jump functionallity for Player ball.
/// </summary>
public class Jumping : MonoBehaviour {

    /// <summary>
    /// Gametick
    /// </summary>
	void Update ()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
	}

    /// <summary>
    /// Jump method
    /// </summary>
    public void Jump()
    {
        if (GetComponent<OnCollision>().getTouchingGround())
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + 10, rb.velocity.z);
            GetComponent<OnCollision>().setTouchingGround(false);
        }
    }    
}
