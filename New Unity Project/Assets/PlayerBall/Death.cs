using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {
	
	void Update () {
        var plane = GameObject.FindWithTag("Plane");
        if (transform.position.y < plane.transform.position.y - 5) DestroyGameObject();
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
