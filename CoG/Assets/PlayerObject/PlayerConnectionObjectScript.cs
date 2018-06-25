using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObjectScript : NetworkBehaviour {
    public GameObject PlayerUnitPrefab;
    public string PlayerName = "Anonymous";

    [SyncVar]
    public int Money;
    [SyncVar]
    public int Wood;

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
        CmdSetMoney(0);
        CmdSetWood(0);
    }


    //SyncVars are variables where if their value changes on the SERVER, then all clients are automatically informed of the new value.
    //[SyncVar(hook="OnPlayerNameChanged")]


    
	
	// Update is called once per frame
	void Update () {
		// Remember: Update runs on everyones computer, whether or not they own this particular player object.
        if (isLocalPlayer == false) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            CmdSpawnMyUnit();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            string n = "Badg" + Random.Range(1, 100);
            Debug.Log("Sending the server a request to change our name to: " + n);
            CmdChangePlayerName(n);
        }
    }
    
    void OnPlayerNameChanged(string newName) {
        Debug.Log("OnPlayerNameChanged: OldName: " + PlayerName + " NewName: " + newName);
        PlayerName = newName;
        gameObject.name = "PlayerConnectionObject [" + newName + "]";
    }


    ///COMMANDS
    ///Special functions that ONLY get executed on the server.
    ///
    [Command]
    void CmdSpawnMyUnit() {

        // We are guaranteed to be on the server right now.
        PlayerUnitPrefab = Instantiate(PlayerUnitPrefab);

        // Now that the object exists on the server, propagate it to all the clients and also wire up the NetworkIdentity.
        NetworkServer.SpawnWithClientAuthority(PlayerUnitPrefab, connectionToClient);
    }

    [Command]
    void CmdChangePlayerName(string n) {
        Debug.Log("CmdChangePlayerName: " + n);
        RpcChangePlayerName(n);
    }

    [Command]
    public void CmdSetMoney(int amount) {
        Money += amount;
    }

    [Command]
    public void CmdSetWood(int amount)
    {
        Wood += amount;
    }

    ///RPC
    ///Special functions that ONLY get executed on the clients.
    ///

    [ClientRpc]
    void RpcChangePlayerName(string n) {
        Debug.Log("RpcChangePlayerName: We were asked to change the player name on a particular PlayerConnectionObject" + n);
        PlayerName = n;
    }
}
