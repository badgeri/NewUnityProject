using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private float mMaxVelocity = 10.0f;
    private float mMinVelocity = -10.0f;
	
    /// <summary>
    /// Gametick
    /// </summary>
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveUp();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveDown();
        }
    }

    /// <summary>
    /// Method to move RigidBody to the right
    /// </summary>
    public void MoveRight()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb.velocity.x < mMaxVelocity)
        {
            rb.velocity = new Vector3(Mathf.Min(rb.velocity.x + 1, mMaxVelocity), rb.velocity.y, rb.velocity.z);
        }
    }

    /// <summary>
    /// Method to move RigidBody to the left
    /// </summary>
    public void MoveLeft()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.x > mMinVelocity)
        {
            rb.velocity = new Vector3(Mathf.Max(rb.velocity.x - 1, mMinVelocity), rb.velocity.y, rb.velocity.z);
        }
    }

    /// <summary>
    /// Method to move RigidBody up
    /// </summary>
    public void MoveUp()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.z < mMaxVelocity)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.Min(rb.velocity.z + 1, mMaxVelocity));
        }
    }

    /// <summary>
    /// Method to move RigidBody down
    /// </summary>
    public void MoveDown()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.z > mMinVelocity)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.Max(rb.velocity.z - 1, mMinVelocity));
        }
    }

    public float GetMaxVelocity()
    {
        return mMaxVelocity;
    }

    public float GetMinVelocity()
    {
        return mMinVelocity;
    }

}
