using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour {
    void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.tag == "Plane")
        {
            Debug.Log("Collision with plane");
        }
	}
}
