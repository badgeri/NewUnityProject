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
            CmdDestroyGameObject(this.gameObject);
            GivePlayerWood(gObject);
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
