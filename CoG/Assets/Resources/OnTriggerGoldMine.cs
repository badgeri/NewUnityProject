using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnTriggerGoldMine : NetworkBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        GameObject gObject;
        if(HandlerPlayer.ActiveGameObject(collider, out gObject))
        {
            if(gameObject.GetComponent<GoldMineScript>().getOwnerNetworkId() == collider.GetComponentInParent<PlayerUnit>().parentNetworkId) // todo - doesnt work!! getOwnerNetworkId, networkid for goldmine doesnt sync?
            {
                return;
            }
            GivePlayerGoldMine(gObject);
        }
    }

    private void GivePlayerGoldMine(GameObject gObject)
    {
        gObject.GetComponent<PlayerConnectionObjectScript>().SetGoldMine(1);
        gameObject.GetComponent<GoldMineScript>().setOwner(gObject.GetComponent<PlayerConnectionObjectScript>().netId);
    }
}
