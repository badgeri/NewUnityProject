using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider colldier)
    {
        if (colldier.gameObject.tag == "Gold")
        {
            var playerBall = GameObject.FindWithTag("Player");
            playerBall.GetComponent<PlayerConnectionObjectScript>().CmdSetMoney(200);
            Destroy(colldier.gameObject);
        }
    }
}
