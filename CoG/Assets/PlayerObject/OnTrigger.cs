using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnTrigger : NetworkBehaviour
{

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Gold")
        {
            CmdDestroyGameObject(collider.gameObject);
        }
        else if (collider.gameObject.tag == "Playerobject") {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject gObject in gameObjects)
            {
                if (gObject.GetComponent<PlayerConnectionObjectScript>().connectionId == collider.gameObject.transform.root.GetComponent<PlayerUnit>().connectionId)
                {
                    gObject.GetComponent<PlayerConnectionObjectScript>().CmdSetMoney(200);
                }
            }
        }
    }

    [Command]
    void CmdDestroyGameObject(GameObject gObject)
    {
        Destroy(gObject);
    }
}
