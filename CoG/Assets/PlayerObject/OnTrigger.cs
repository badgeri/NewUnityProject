using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OnTrigger : NetworkBehaviour
{

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Gold" || collider.gameObject.tag == "Wood")
        {
            CmdDestroyGameObject(collider.gameObject);
        }
        else if (collider.gameObject.tag == "Playerobject") {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject gObject in gameObjects)
            {
                if (gObject.GetComponent<PlayerConnectionObjectScript>().hasAuthority) //todo - still need to check the correct gameobject... for some reason this does run on the other client and has authority fucks up.
                {
                    if (gameObject.tag == "Gold")
                    {
                        GivePlayerGold(gObject);
                    }
                    else if (gameObject.tag == "Wood")
                    {
                        GivePlayerWood(gObject);
                    }
                }
            }
        }
    }

    void GivePlayerGold(GameObject gObject)
    {
        gObject.GetComponent<PlayerConnectionObjectScript>().SetMoney(200);
    }

    void GivePlayerWood(GameObject gObject)
    {
        gObject.GetComponent<PlayerConnectionObjectScript>().SetWood(200);
    }

    [Command]
    void CmdDestroyGameObject(GameObject gObject)
    {
        Destroy(gObject);
    }

}
