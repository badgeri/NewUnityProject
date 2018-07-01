using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnTriggerGoldMine : NetworkBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Playerobject")
        {
            if(gameObject.GetComponent<GoldMineScript>().getOwnerNetworkId() == collider.GetComponentInParent<PlayerUnit>().parentNetworkId) // todo - doesnt work!! getOwnerNetworkId, networkid for goldmine doesnt sync?
            {
                return;
            }
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject gObject in gameObjects)
            {
                if (collider.GetComponentInParent<PlayerUnit>().parentNetworkId == gObject.GetComponent<PlayerConnectionObjectScript>().netId && gObject.GetComponent<PlayerConnectionObjectScript>().hasAuthority)
                {
                    GivePlayerGoldMine(gObject);
                }
            }
        }
    }

    private void GivePlayerGoldMine(GameObject gObject)
    {
        gObject.GetComponent<PlayerConnectionObjectScript>().SetGoldMine(1);
        gameObject.GetComponent<GoldMineScript>().setOwner(gObject.GetComponent<PlayerConnectionObjectScript>().netId);
    }
}
