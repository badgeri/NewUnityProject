using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnTriggerWood : NetworkBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        GameObject gObject;
        if (HandlerPlayer.ActiveGameObject(collider, out gObject))
        {
            gObject.GetComponent<PlayerConnectionObjectScript>().CmdSetClientAuthority(this.gameObject.GetComponent<NetworkIdentity>());
            GivePlayerWood(gObject);
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

    private void GivePlayerWood(GameObject gObject)
    {
        gObject.GetComponent<PlayerConnectionObjectScript>().SetWood(200);
    }

    [Command]
    private void CmdDestroyGameObject(GameObject gObject)
    {
        Destroy(gObject);
    }
}
