using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Jump functionallity for Player ball.
/// </summary>
public class Jumping : MonoBehaviour {

    private bool mTouchingGround = false;

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
        if (getTouchingGround())
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + 10, rb.velocity.z);
            mTouchingGround = false;
        }
    }

    /// <summary>
    /// Set touching ground - ball can only jump if it is touching the ground.
    /// </summary>
    /// <param name="isTouchingGround"></param>
    public void setTouchingGround(bool isTouchingGround)
    {
        mTouchingGround = isTouchingGround;
    }

    /// <summary>
    /// Get touching ground - ball can only jump if it is touching the ground.
    /// </summary>
    /// <returns></returns>
    public bool getTouchingGround()
    {
        return mTouchingGround;
    }
}
