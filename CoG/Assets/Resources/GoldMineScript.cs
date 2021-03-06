﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GoldMineScript : NetworkBehaviour
{
    [SyncVar]
    public NetworkInstanceId ownerNetworkId;
	
    public void setOwner(NetworkInstanceId ownerId)
    {
        CmdSetOwner(ownerId);
    }

    public NetworkInstanceId getOwnerNetworkId()
    {
        return ownerNetworkId;
    }

    [Command]
    private void CmdSetOwner(NetworkInstanceId ownerId)
    {
        RpcSetOwner(ownerId);
    }

    [ClientRpc]
    private void RpcSetOwner(NetworkInstanceId ownerId)
    {
        ownerNetworkId = ownerId;
    }
}
