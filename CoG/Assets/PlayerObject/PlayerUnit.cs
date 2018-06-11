using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// A PlayerUnit is a unit controlled by a player (will be renamed PlayerHero later!)

public class PlayerUnit : NetworkBehaviour {

    [SyncVar]
    public int connectionId;

	// Use this for initialization
	void Start () {
	}

	
	// Update is called once per frame
	void Update () {

        //Verify that user is allowed to control object
        if (hasAuthority == false) {
            return;
        }

		if (Input.GetKeyDown(KeyCode.Space)) {
            this.transform.Translate(0, 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            Destroy(gameObject);
        }
	}

    [ClientRpc]
    public void RpcSetConnectionId(int id)
    {
        if (hasAuthority == false)
        {
            return;
        }
        connectionId = id;
    }
}
