using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnTriggerGoldMine : NetworkBehaviour
{
    private bool setOwner = false;
    void OnTriggerEnter(Collider collider)
    {
        GameObject gObject;
        if(HandlerPlayer.ActiveGameObject(collider, out gObject))
        {
            gObject.GetComponent<PlayerConnectionObjectScript>().CmdSetClientAuthority(this.gameObject.GetComponent<NetworkIdentity>());
            if (gameObject.GetComponent<GoldMineScript>().getOwnerNetworkId() == collider.GetComponentInParent<PlayerUnit>().parentNetworkId)
            {
                return;
            }
            GivePlayerGoldMine(gObject);
            setOwner = true;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (setOwner)
        {
            GameObject gObject;
            if (HandlerPlayer.ActiveGameObject(collider, out gObject))
            {
                if (hasAuthority)
                {
                    gameObject.GetComponent<GoldMineScript>().setOwner(gObject.GetComponent<PlayerConnectionObjectScript>().netId);
                }
            }
        }
    }

    private void GivePlayerGoldMine(GameObject gObject)
    {
        gObject.GetComponent<PlayerConnectionObjectScript>().SetGoldMine(1);
    }
}
