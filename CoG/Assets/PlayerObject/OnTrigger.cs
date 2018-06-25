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
                if (collider.gameObject.transform.root.GetComponent<PlayerUnit>().netId == gObject.GetComponent<PlayerConnectionObjectScript>().PlayerUnitPrefab.GetComponent<PlayerUnit>().netId)
                {
                    CmdGivePlayerGold(gObject);
                }
            }
        }
    }

    [Command]
    void CmdDestroyGameObject(GameObject gObject)
    {
        Debug.Log("Destroy on server");
        Destroy(gObject);
    }

    [Command]
    void CmdGivePlayerGold(GameObject gObject)
    {
        Debug.Log("Set gold on server");
        gObject.GetComponent<PlayerConnectionObjectScript>().CmdSetMoney(200);
    }

}
