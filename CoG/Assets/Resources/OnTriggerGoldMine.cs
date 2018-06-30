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
            CmdDestroyGameObject(this.gameObject);

            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject gObject in gameObjects)
            {
                NetworkInstanceId id1 = collider.GetComponentInParent<PlayerUnit>().parentNetworkId;
                //NetworkInstanceId id1 = collider.GetComponent<PlayerUnit>().parentNetworkId;
                NetworkInstanceId id2 = gObject.GetComponent<PlayerConnectionObjectScript>().netId;

                int kj = 3;
                kj++;
                /*
                if (gObject.GetComponent<PlayerConnectionObjectScript>().hasAuthority) //todo - still need to check the correct gameobject... for some reason this does run on the other client and has authority fucks up.
                {
                    if (gameObject.tag == "GoldMine")
                    {
                        int e = 3;
                        e++;
                        //GivePlayerWood(gObject);
                    }
                }
                */
            }
        }
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
