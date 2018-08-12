using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnTriggerGoldMine : NetworkBehaviour
{
    private bool setOwner = false;
    private bool removeOldOwner = false;

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
            else if (gameObject.GetComponent<GoldMineScript>().getOwnerNetworkId().Value != 0) //defaults to 0 for no owner
            {
                removeOldOwner = true;
            }
            setOwner = true;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (setOwner)
        {
            GameObject playerGameObject;
            if (HandlerPlayer.ActiveGameObject(collider, out playerGameObject))
            {
                if (hasAuthority)
                {
                    if (removeOldOwner)
                    {
                        CmdRemoveOldOwnerGoldMine(gameObject.GetComponent<GoldMineScript>().getOwnerNetworkId());
                        removeOldOwner = false;
                    }
                    gameObject.GetComponent<GoldMineScript>().setOwner(playerGameObject.GetComponent<PlayerConnectionObjectScript>().netId);
                    setOwner = false;
                    CmdGivePlayerGoldMine(playerGameObject);
                }
            }
        }
    }

    [Command]
    private void CmdGivePlayerGoldMine(GameObject playerGameObject)
    {
        GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManagerScript>().increasePlayerGoldMine(playerGameObject.GetComponent<PlayerConnectionObjectScript>().netId, 1);
    }

    [Command]
    private void CmdRemoveOldOwnerGoldMine(NetworkInstanceId netId)
    {
        GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManagerScript>().decreasePlayerGoldMine(netId, 1);
    }
}
