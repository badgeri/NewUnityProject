using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour {

	// Use this for initialization
	void Start () {

        //Is this actually my own local PlayerConnectionObject?
        if ( isLocalPlayer == false ) {
            //This object belongs to another player.
            return;
        }

        // Since the PlayerConnectionObject is invisible and not part of the world, create something physical to move around!
        Debug.Log("PlayerConnectionObject::Start -- Spawning my own personal unit.");

        // Command the server to spawn my unit
        CmdSpawnMyUnit();
	}

    public GameObject PlayerUnitPrefab;
	
	// Update is called once per frame
	void Update () {
		// Remember: Update runs on everyones computer, whether or not they own this particular player object.
        if (isLocalPlayer == false) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            CmdSpawnMyUnit();
        }
	}


    ///COMMANDS
    ///Special functions that ONLY get executed on the server.
    ///

    [Command]
    void CmdSpawnMyUnit() {
        // We are guaranteed to be on the server right now.
        GameObject myPlayerUnit = Instantiate(PlayerUnitPrefab);

        // Now that the object exists on the server, propagate it to all the clients and also wire up the NetworkIdentity.
        NetworkServer.SpawnWithClientAuthority(myPlayerUnit, connectionToClient);
    }
}
