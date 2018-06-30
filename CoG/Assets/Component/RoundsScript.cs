﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsScript : MonoBehaviour {

    bool hasStarted = false;
    int nrOfPlayers;
    int currentPlayersTurn;
    int currentDay;
    int currentWeek;

    // Use this for initialization
    void Start()
    {
        currentDay = 1;
        currentWeek = 1;
    }

    // Update is called once per frame
    void Update()
    {
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
                foreach (GameObject gObject in gameObjects)
                {
                    gObject.GetComponent<PlayerConnectionObjectScript>().setIsPlayersTurn(false);
                }
                gameObjects[0].GetComponent<PlayerConnectionObjectScript>().setIsPlayersTurn(true);
                currentPlayersTurn = 1;
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
            currentPlayersTurn = 1;
            nextDay();
        }
        else
        {
            gameObjects[currentPlayersTurn].GetComponent<PlayerConnectionObjectScript>().setIsPlayersTurn(true);
            currentPlayersTurn++;
        }
    }

    private void nextDay()
    {
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
