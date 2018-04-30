﻿using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayerBallTests {

    [UnityTest]
    public IEnumerator _CanSceneBeLoaded() { 
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
    }

    [UnityTest]
	public IEnumerator _InitiatePlayerBall() {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        Assert.That(playerBall != null);
    }

    [UnityTest]
    public IEnumerator _InitiatedPlayerBallFalling()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        Assert.That(playerBall != null);
        float initY = playerBall.transform.position.y;
        yield return null;
        Assert.AreNotEqual(initY, playerBall.transform.position.y);
    }

    [UnityTest]
    public IEnumerator _InitiatedPlayerBallFallingAndLandingOnPlane()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        var plane = GameObject.FindWithTag("Plane");
        
        Assert.That(playerBall != null);
        float initY = playerBall.transform.position.y;
        yield return null;
        Assert.AreNotEqual(initY, playerBall.transform.position.y);
    }
}
