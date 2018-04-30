using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPressSpace : MonoBehaviour {
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
	}

    public void Jump() {
        transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
    }
}
