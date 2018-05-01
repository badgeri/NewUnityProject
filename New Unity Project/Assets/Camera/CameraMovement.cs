using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    /// <summary>
    /// Initiation
    /// </summary>
	void Start () {
        var playerBall = GameObject.FindWithTag("Player");
        transform.position = new Vector3(playerBall.transform.position.x, playerBall.transform.position.y + 20, playerBall.transform.position.z - 20);
    }
	
    /// <summary>
    /// Gametick (last)
    /// </summary>
	void LateUpdate () {
        //Main camera follows the Player ball.
        var playerBall = GameObject.FindWithTag("Player");
        if (playerBall)
        {
            transform.position = new Vector3(playerBall.transform.position.x, playerBall.transform.position.y + 20, playerBall.transform.position.z - 20);
        }
	}
}
