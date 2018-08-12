using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ResourceManagerScript : NetworkBehaviour {

    public bool isInitialized = false;

    class PlayerResources // I made this a class and not a struct since a struct is a value type and hence when accessing it to for example set the gold, we would get a copy and not a reference.
    {
        public int gold;
        public int wood;
        public int goldMine;
    }

    Dictionary<NetworkInstanceId, PlayerResources> playerResourcesMap = new Dictionary<NetworkInstanceId, PlayerResources>();

    List<NetworkInstanceId> networkInstanceIdList = new List<NetworkInstanceId>();
	
    public void initializePlayerResources(List<NetworkInstanceId> networkInstanceIds)
    {
        for (int i = 0; i < networkInstanceIds.Count; i++)
        {
            PlayerResources playerResources = new PlayerResources
            {
                gold = 0,
                wood = 0,
                goldMine = 0
            };
            playerResourcesMap.Add(networkInstanceIds[i], playerResources);
            networkInstanceIdList.Add(networkInstanceIds[i]);
        }
        isInitialized = true;
    }

    public void newDay()
    {
        foreach(NetworkInstanceId networkInstanceId in networkInstanceIdList)
        {
            increasePlayerGold(networkInstanceId, playerResourcesMap[networkInstanceId].goldMine * 100);
        }
    }

    public void increasePlayerGold(NetworkInstanceId netId, int amount)
    {
        playerResourcesMap[netId].gold += amount;
        RpcSetGold(netId, playerResourcesMap[netId].gold);
    }

    public void decreasePlayerGold(NetworkInstanceId netId, int amount)
    {
        playerResourcesMap[netId].gold -= amount;
        RpcSetGold(netId, playerResourcesMap[netId].gold);
    }

    public void increasePlayerGoldMine(NetworkInstanceId netId, int amount)
    {
        playerResourcesMap[netId].goldMine += amount;
        RpcSetGoldMine(netId, playerResourcesMap[netId].goldMine);
    }

    public void decreasePlayerGoldMine(NetworkInstanceId netId, int amount)
    {
        playerResourcesMap[netId].goldMine -= amount;
        RpcSetGoldMine(netId, playerResourcesMap[netId].goldMine);
    }

    public void increasePlayerWood(NetworkInstanceId netId, int amount)
    {
        playerResourcesMap[netId].wood += amount;
        RpcSetWood(netId, playerResourcesMap[netId].wood);
    }

    public void decreasePlayerWood(NetworkInstanceId netId, int amount)
    {
        playerResourcesMap[netId].wood -= amount;
        RpcSetWood(netId, playerResourcesMap[netId].wood);
    }

    [ClientRpc]
    public void RpcSetGold(NetworkInstanceId netId, int amount)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject gObject in gameObjects)
        {
            if (netId == gObject.GetComponent<PlayerConnectionObjectScript>().netId)
            {
                gObject.GetComponent<PlayerConnectionObjectScript>().SetGold(amount);
            }
        }
    }

    [ClientRpc]
    public void RpcSetGoldMine(NetworkInstanceId netId, int amount)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject gObject in gameObjects)
        {
            if (netId == gObject.GetComponent<PlayerConnectionObjectScript>().netId)
            {
                gObject.GetComponent<PlayerConnectionObjectScript>().SetGoldMine(amount);
            }
        }
    }

    [ClientRpc]
    public void RpcSetWood(NetworkInstanceId netId, int amount)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject gObject in gameObjects)
        {
            if (netId == gObject.GetComponent<PlayerConnectionObjectScript>().netId)
            {
                gObject.GetComponent<PlayerConnectionObjectScript>().SetWood(amount);
            }
        }
    }

}
