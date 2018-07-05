using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnTriggerGold : NetworkBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        GameObject gObject;
        if (HandlerPlayer.ActiveGameObject(collider, out gObject))
        {
            gObject.GetComponent<PlayerConnectionObjectScript>().CmdSetClientAuthority(this.gameObject.GetComponent<NetworkIdentity>()); 
            GivePlayerGold(gObject);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        GameObject gObject;
        if (HandlerPlayer.ActiveGameObject(collider, out gObject))
        {
            if (hasAuthority)
            {
                if (this.gameObject)
                {
                    CmdDestroyGameObject(this.gameObject);
                }
            }
        }
    }

    private void GivePlayerGold(GameObject gObject)
    {
        gObject.GetComponent<PlayerConnectionObjectScript>().SetMoney(200);
    }

    [Command]
    private void CmdDestroyGameObject(GameObject gObject)
    {
        Destroy(gObject);
    }
}
