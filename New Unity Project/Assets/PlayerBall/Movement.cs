using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	

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

    public void MoveRight()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb.velocity.x < 3)
        {
            rb.velocity = new Vector3(Mathf.Min(rb.velocity.x + 1, 3.0f), rb.velocity.y, rb.velocity.z);
        }
    }

    public void MoveLeft()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.x > -3)
        {
            rb.velocity = new Vector3(Mathf.Max(rb.velocity.x - 1, -3.0f), rb.velocity.y, rb.velocity.z);
        }
    }

    public void MoveUp()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.z < 3)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.Min(rb.velocity.z + 1, 3.0f));
        }
    }

    public void MoveDown()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.velocity.z > -3)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.Max(rb.velocity.z - 1, -3.0f));
        }
    }

}
