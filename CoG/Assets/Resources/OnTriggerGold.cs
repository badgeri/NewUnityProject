using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnTriggerGold : NetworkBehaviour
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
                    GivePlayerGold(gObject);
                }
            }
        }
    }

    void GivePlayerGold(GameObject gObject)
    {
        gObject.GetComponent<PlayerConnectionObjectScript>().SetMoney(200);
    }

    [Command]
    void CmdDestroyGameObject(GameObject gObject)
    {
        Destroy(gObject);
    }
}
