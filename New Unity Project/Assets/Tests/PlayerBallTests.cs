using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;


/// <summary>
/// Test class for Player Ball
/// </summary>
public class PlayerBallTests {


    /// <summary>
    /// Verify that scene PlayerBallAndPlaneScene can be loaded.
    /// </summary>
    /// <returns></returns>    
    [UnityTest]
    public IEnumerator _CanSceneBeLoaded() { 
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
    }

    /// <summary>
    /// Verify that Player ball is initiated upon startup.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
	public IEnumerator _InitiatePlayerBall() {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        Assert.That(playerBall != null);
    }

    /// <summary>
    /// Verity that Player ball is rigid body and affected by gravity.
    /// </summary>
    /// <returns></returns>
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
        yield return new WaitForSeconds(0.5f);
        Assert.AreNotEqual(initY, playerBall.transform.position.y);
    }

    /// <summary>
    /// Verify that Player ball lands on the plane.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator _InitiatedPlayerBallFallingAndLandingOnPlane()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        yield return new WaitForSeconds(2);
        Assert.True(playerBall.GetComponent<Jumping>().getTouchingGround());
    }

    /// <summary>
    /// Verify that Jump method works for Player ball.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator _PlayerBallJumpOnPressSpace()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        yield return new WaitForSeconds(2);
        float restingY = playerBall.transform.position.y;
        playerBall.GetComponent<Jumping>().Jump();
        yield return new WaitForSeconds(0.5f);
        Assert.AreNotEqual(restingY, playerBall.transform.position.y);
    }

    /// <summary>
    /// Verify that the Player ball cant jump while airborn.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator _PlayerBallDontJumpInAir()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        float initY = playerBall.transform.position.y;
        playerBall.GetComponent<Jumping>().Jump();
        yield return new WaitForSeconds(0.5f);
        Assert.Less(playerBall.transform.position.y, initY);
    }


}
