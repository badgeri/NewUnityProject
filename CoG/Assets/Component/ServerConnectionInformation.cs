using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerConnectionInformation : NetworkBehaviour
{

    //TODO, should use UUID's

    private List<int> connectionIds = new List<int>();
    private int currentId;

    void Start() {
        currentId = 0;
    }

    public void removeConnectionId(int connectionId) {
        connectionIds.Remove(connectionIds.Find(cId => cId.Equals(connectionId)));
    }

    public int getConnectionId() {
        currentId++;
        connectionIds.Add(currentId);
        return currentId;
    }
}
