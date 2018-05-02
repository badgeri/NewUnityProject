using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {
	
    /// <summary>
    /// Gametick
    /// </summary>
	void Update () {
        //Check if game object should be destroyd and destroys if yes
        DestroyGameObject();
    }

    /// <summary>
    /// Destroy game object
    /// </summary>
    void DestroyGameObject()
    {
        var plane = GameObject.FindWithTag("Plane");
        if (transform.position.y < plane.transform.position.y - 5) Destroy(gameObject);
    }
}
