using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnTriggerWood : NetworkBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Playerobject")
        {
            CmdDestroyGameObject(this.gameObject);
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject gObject in gameObjects)
            {
                if (collider.GetComponentInParent<PlayerUnit>().parentNetworkId == gObject.GetComponent<PlayerConnectionObjectScript>().netId && gObject.GetComponent<PlayerConnectionObjectScript>().hasAuthority) 
                {
                    GivePlayerWood(gObject);
                }
            }
        }
    }

    void GivePlayerWood(GameObject gObject)
    {
        gObject.GetComponent<PlayerConnectionObjectScript>().SetWood(200);
    }

    [Command]
    void CmdDestroyGameObject(GameObject gObject)
    {
        Destroy(gObject);
    }
}
