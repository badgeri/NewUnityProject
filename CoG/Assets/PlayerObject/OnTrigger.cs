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

            Debug.Log("trigger with Playerobject.");

            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject gObject in gameObjects)
            {
                int colliderconnectionid = collider.gameObject.transform.root.GetComponent<PlayerUnit>().connectionId;
                int gObjectconnectionid = gObject.GetComponent<PlayerConnectionObjectScript>().connectionId;

                Debug.Log("collider connection id = " + colliderconnectionid + " and gameobjectconnectionid = " + gObjectconnectionid);

                if (gObject.GetComponent<PlayerConnectionObjectScript>().connectionId == collider.gameObject.transform.root.GetComponent<PlayerUnit>().connectionId)
                {
                    Debug.Log("Setting gold");
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
