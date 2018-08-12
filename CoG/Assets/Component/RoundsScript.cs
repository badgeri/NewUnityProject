using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RoundsScript : MonoBehaviour {

    private bool hasStarted = false;
    private int nrOfPlayers;
    private int currentPlayersTurn;
    private int currentDay;
    private int currentWeek;

    // Use this for initialization
    void Start()
    {
        currentDay = 1;
        currentWeek = 1;
    }

    // Update is called once per frame
    void Update()
    {

        //todo - should add "hasAuthority" to methods?


        if (!hasStarted)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
            int nrReady = 0;
            foreach (GameObject gObject in gameObjects)
            {
                if (gObject.GetComponent<PlayerConnectionObjectScript>().getIsReady())
                {
                    nrReady++;
                }
            }
            if (gameObjects.Length == nrReady && nrReady > 0)
            {
                hasStarted = true;
                nrOfPlayers = gameObjects.Length;
                //todo need to block connecting players? 
                List<NetworkInstanceId> networkInstanceIds = new List<NetworkInstanceId>();
                foreach (GameObject gObject in gameObjects)
                {
                    gObject.GetComponent<PlayerConnectionObjectScript>().setIsPlayersTurn(false);
                    networkInstanceIds.Add(gObject.GetComponent<PlayerConnectionObjectScript>().netId);
                }
                gameObjects[0].GetComponent<PlayerConnectionObjectScript>().setIsPlayersTurn(true);
                currentPlayersTurn = 1;
                GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManagerScript>().initializePlayerResources(networkInstanceIds);
            }
        }
    }

    public void playerTurnDone()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        gameObjects[currentPlayersTurn - 1].GetComponent<PlayerConnectionObjectScript>().setIsPlayersTurn(false);
        if (currentPlayersTurn == nrOfPlayers)
        {
            gameObjects[0].GetComponent<PlayerConnectionObjectScript>().setIsPlayersTurn(true);
            nextDay();
            currentPlayersTurn = 1;
        }
        else
        {
            gameObjects[currentPlayersTurn].GetComponent<PlayerConnectionObjectScript>().setIsPlayersTurn(true);
            currentPlayersTurn++;
        }
    }

    private void nextDay()
    {
        GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManagerScript>().newDay();

        if (currentDay % 7 == 0)
        {
            nextWeek();
        }
        currentDay++;
    }

    private void nextWeek()
    {
        currentWeek++;
    }
}
