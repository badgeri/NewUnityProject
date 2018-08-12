using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnTriggerGold : NetworkBehaviour
{
    private bool giveGold = false; //this is needed to allow the time it takes to set authority

    void OnTriggerEnter(Collider collider)
    {
        GameObject gObject;
        if (HandlerPlayer.ActiveGameObject(collider, out gObject))
        {
            gObject.GetComponent<PlayerConnectionObjectScript>().CmdSetClientAuthority(this.gameObject.GetComponent<NetworkIdentity>()); //set authority of the gold to the client, in order to destroy the object
            giveGold = true;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (giveGold)
        {
            GameObject playerGameObject;
            if (HandlerPlayer.ActiveGameObject(collider, out playerGameObject))
            {
                if (hasAuthority)
                {
                    CmdGivePlayerGoldAndDestroyThis(playerGameObject, this.gameObject);
                    giveGold = false;
                }
            }
        }
    }

    [Command]
    private void CmdGivePlayerGoldAndDestroyThis(GameObject playerGameObject, GameObject thisObject)
    {
        GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManagerScript>().increasePlayerGold(playerGameObject.GetComponent<PlayerConnectionObjectScript>().netId, 200);
        Destroy(thisObject);
    }
}
