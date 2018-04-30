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
    public IEnumerator _CanSceneBeLoaded()
    { 
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
	public IEnumerator _InitiatePlayerBall()
    {
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
        yield return new WaitForSeconds(1);
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
        yield return new WaitForSeconds(1);
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

    /// <summary>
    /// Verify that move left works for Player ball.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator _PlayerBallMoveLeft()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        yield return new WaitForSeconds(1);
        float initX = playerBall.transform.position.x;
        playerBall.GetComponent<Movement>().MoveLeft();
        yield return new WaitForSeconds(0.5f);
        Assert.Less(playerBall.transform.position.x, initX);
    }

    /// <summary>
    /// Verify that move right works for Player ball.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator _PlayerBallMoveRight()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        yield return new WaitForSeconds(1);
        float initX = playerBall.transform.position.x;
        playerBall.GetComponent<Movement>().MoveRight();
        yield return new WaitForSeconds(0.5f);
        Assert.Less(initX, playerBall.transform.position.x);
    }

    /// <summary>
    /// Verify that move up works for Player ball.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator _PlayerBallMoveUp()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        yield return new WaitForSeconds(1);
        float initZ = playerBall.transform.position.z;
        playerBall.GetComponent<Movement>().MoveUp();
        yield return new WaitForSeconds(0.5f);
        Assert.Less(initZ, playerBall.transform.position.z);
    }

    /// <summary>
    /// Verify that move down works for Player ball.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator _PlayerBallMoveDown()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        yield return new WaitForSeconds(1);
        float initZ = playerBall.transform.position.z;
        playerBall.GetComponent<Movement>().MoveDown();
        yield return new WaitForSeconds(0.5f);
        Assert.Less(playerBall.transform.position.z, initZ);
    }

    /// <summary>
    /// Verify that max velocity for Player ball is not exceeded.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator _PlayerBallMaxVelocity()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        yield return new WaitForSeconds(1);
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        Assert.AreEqual(Mathf.Abs(playerBall.GetComponent<Rigidbody>().velocity.z), Mathf.Abs(playerBall.GetComponent<Movement>().GetMinVelocity()), 0.01);
    }

    /// <summary>
    /// Verify that Player ball does not slow down.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator _PlayerBallConstantVelocity()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        yield return new WaitForSeconds(1);
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        playerBall.GetComponent<Movement>().MoveDown();
        yield return new WaitForSeconds(2f);
        Assert.AreEqual(Mathf.Abs(playerBall.GetComponent<Rigidbody>().velocity.z), Mathf.Abs(playerBall.GetComponent<Movement>().GetMinVelocity()), 0.01);
    }
}
