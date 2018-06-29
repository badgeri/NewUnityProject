using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnTrigger : NetworkBehaviour
{

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Gold" || collider.gameObject.tag == "Wood")
        {
            CmdDestroyGameObject(collider.gameObject);
        }
        else if (collider.gameObject.tag == "Playerobject") {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject gObject in gameObjects)
            {
                if (collider.gameObject.transform.root.GetComponent<PlayerUnit>().netId == gObject.GetComponent<PlayerConnectionObjectScript>().PlayerUnitPrefab.GetComponent<PlayerUnit>().netId)
                {
                    if (gameObject.tag == "Gold")
                    {
                        CmdGivePlayerGold(gObject);
                    }
                    else if (gameObject.tag == "Wood")
                    {
                        CmdGivePlayerWood(gObject);
                    }
                }
            }
        }
    }

    [Command]
    void CmdDestroyGameObject(GameObject gObject)
    {
        Destroy(gObject);
    }

    [Command]
    void CmdGivePlayerGold(GameObject gObject)
    {
        gObject.GetComponent<PlayerConnectionObjectScript>().CmdSetMoney(200);
    }

    [Command]
    void CmdGivePlayerWood(GameObject gObject)
    {
        gObject.GetComponent<PlayerConnectionObjectScript>().CmdSetWood(200);
    }

}
