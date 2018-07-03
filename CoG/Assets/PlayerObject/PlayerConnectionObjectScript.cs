using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObjectScript : NetworkBehaviour {
    public GameObject PlayerUnitPrefab;
    public string PlayerName = "Anonymous";


    [SyncVar]
    private bool shouldInitializePlayerUnit = false;
    [SyncVar]
    public int Money;
    [SyncVar]
    public int GoldMine;
    [SyncVar]
    public int Wood;
    [SyncVar]
    private bool isReady = false;
    [SyncVar]
    private bool isPlayersTurn = true;

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
        CmdSetGoldMine(0);
        CmdSetWood(0);
    }


    //SyncVars are variables where if their value changes on the SERVER, then all clients are automatically informed of the new value.
    //[SyncVar(hook="OnPlayerNameChanged")]

    /*
    void OnPlayerNameChanged(string newName)
    {
        Debug.Log("OnPlayerNameChanged: OldName: " + PlayerName + " NewName: " + newName);
        PlayerName = newName;
        gameObject.name = "PlayerConnectionObject [" + newName + "]";
    }
    */


    // Update is called once per frame
    void Update () {
		// Remember: Update runs on everyones computer, whether or not they own this particular player object.
        if (isLocalPlayer == false) {
            return;
        }

        if (shouldInitializePlayerUnit)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("PlayerUnit");
            foreach (GameObject gObject in gameObjects)
            {
                if (gObject.GetComponent<PlayerUnit>().hasAuthority)
                {
                    if (gObject.GetComponent<PlayerUnit>().setParentNetworkId(netId)) //// <---- does not work, PlayerUnitPrefab is not the instance that is created!!!
                    {
                        shouldInitializePlayerUnit = false;
                    }

                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            CmdSpawnMyUnit();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CmdSetIsReady(true);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            CmdSetTurnDone();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            string n = "Badg" + Random.Range(1, 100);
            Debug.Log("Sending the server a request to change our name to: " + n);
            CmdChangePlayerName(n);
        }
    }

    public bool getIsReady()
    {
        return isReady;
    }

    public bool getIsPlayersTurn()
    {
        return isPlayersTurn;
    }

    public void setIsPlayersTurn(bool isTurn)
    {
        isPlayersTurn = isTurn;
    }

    public void SetMoney(int amount)
    {
        CmdSetMoney(amount);
    }

    public void SetWood(int amount)
    {
        CmdSetWood(amount);
    }

    public void SetGoldMine(int amount)
    {
        CmdSetGoldMine(amount);
    }

    public void newDay()
    {
        CmdSetMoney(GoldMine * 100);
    }

    ///COMMANDS
    ///Special functions that ONLY get executed on the server.
    ///
    [Command]
    private void CmdSpawnMyUnit() {

        // We are guaranteed to be on the server right now.
        PlayerUnitPrefab = Instantiate(PlayerUnitPrefab);

        // Now that the object exists on the server, propagate it to all the clients and also wire up the NetworkIdentity.
        NetworkServer.SpawnWithClientAuthority(PlayerUnitPrefab, connectionToClient);

        shouldInitializePlayerUnit = true;
    }     

    [Command]
    private void CmdChangePlayerName(string n) {
        Debug.Log("CmdChangePlayerName: " + n);
        RpcChangePlayerName(n);
    }

    [Command]
    private void CmdSetMoney(int amount) {
        Money += amount;
    }

    [Command]
    private void CmdSetWood(int amount)
    {
        Wood += amount;
    }

    [Command]
    private void CmdSetGoldMine(int amount)
    {
        GoldMine += amount;
    }

    [Command]
    private void CmdSetIsReady(bool ready)
    {
        isReady = ready;
    }

    [Command]
    private void CmdSetTurnDone()
    {
        GameObject.FindGameObjectWithTag("RoundManager").GetComponent<RoundsScript>().playerTurnDone();
    }


    ///RPC
    ///Special functions that ONLY get executed on the clients.
    ///

    [ClientRpc]
    private void RpcChangePlayerName(string n) {
        Debug.Log("RpcChangePlayerName: We were asked to change the player name on a particular PlayerConnectionObject" + n);
        PlayerName = n;
    }
}
