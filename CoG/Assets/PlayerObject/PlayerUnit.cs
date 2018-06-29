using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// A PlayerUnit is a unit controlled by a player (will be renamed PlayerHero later!)

public class PlayerUnit : NetworkBehaviour {

    private bool isInitialized = false;

	// Use this for initialization
	void Start () {
    }

	
	// Update is called once per frame
	void Update () {

        //Verify that user is allowed to control object
        if (hasAuthority == false) {
            return;
        }

        if (!isInitialized)
        {
            isInitialized = true;
            Camera.main.transform.position = this.transform.position - this.transform.forward * 30 + this.transform.up * 30;
            Camera.main.transform.LookAt(this.transform.position);

            Camera.main.transform.parent = this.transform;
        }

        

		if (Input.GetKeyDown(KeyCode.Space)) {
            this.transform.Translate(0, 1, 0);
        }
	}

    public bool hasCallerAuthority()
    {
        return hasAuthority;
    }
}
