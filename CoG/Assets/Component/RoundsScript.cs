using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsScript : MonoBehaviour {

    bool hasStarted = false;

    // Use this for initialization
    void Start()
    {

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
                if (gObject.GetComponent<PlayerConnectionObjectScript>().IsReady())
                {
                    nrReady++;
                }
            }
            if (gameObjects.Length == nrReady && nrReady > 0)
            {
                hasStarted = true;
                //todo need to block connecting players? 
            }
        }
    }
}
