using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnTriggerWood : NetworkBehaviour
{
    private bool giveWood = false; //this is needed to allow the time it takes to set authority

    void OnTriggerEnter(Collider collider)
    {
        GameObject gObject;
        if (HandlerPlayer.ActiveGameObject(collider, out gObject))
        {
            gObject.GetComponent<PlayerConnectionObjectScript>().CmdSetClientAuthority(this.gameObject.GetComponent<NetworkIdentity>()); //set authority of the gold to the client, in order to destroy the object
            giveWood = true;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (giveWood)
        {
            GameObject playerGameObject;
            if (HandlerPlayer.ActiveGameObject(collider, out playerGameObject))
            {
                if (hasAuthority)
                {
                    CmdGivePlayerWoodAndDestroyThis(playerGameObject, this.gameObject);
                    giveWood = false;
                }
            }
        }
    }

    [Command]
    private void CmdGivePlayerWoodAndDestroyThis(GameObject playerGameObject, GameObject thisObject)
    {
        GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManagerScript>().increasePlayerWood(playerGameObject.GetComponent<PlayerConnectionObjectScript>().netId, 200);
        Destroy(thisObject);
    }
}
