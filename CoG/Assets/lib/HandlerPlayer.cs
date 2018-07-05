using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerPlayer {
    public static bool ActiveGameObject(Collider collider, out GameObject outGameObject)
    {
        outGameObject = null;
        if (collider.gameObject.tag == "Playerobject")
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject gObject in gameObjects)
            {
                if (collider.GetComponentInParent<PlayerUnit>().parentNetworkId == gObject.GetComponent<PlayerConnectionObjectScript>().netId && gObject.GetComponent<PlayerConnectionObjectScript>().hasAuthority)
                {
                    outGameObject = gObject;
                    return true;
                }
            }
        }
        return false;
    }
}
