using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class OnMobTrigger : NetworkBehaviour
{
    public GameObject canvas;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Playerobject")
        {
            CmdDestroyGameObject(this.gameObject);
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject gObject in gameObjects)
            {
                if (collider.GetComponentInParent<PlayerUnit>().parentNetworkId == gObject.GetComponent<PlayerConnectionObjectScript>().netId && gObject.GetComponent<PlayerConnectionObjectScript>().hasAuthority)
                {
                    canvas.SetActive(true);
                }
            }
        }
    }

    [Command]
    private void CmdDestroyGameObject(GameObject gObject)
    {
        Destroy(gObject);
    }


}
