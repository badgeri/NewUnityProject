using UnityEngine;
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
        yield return new WaitForSeconds(0.5f);
        Assert.AreNotEqual(initY, playerBall.transform.position.y);
    }

    [UnityTest]
    public IEnumerator _InitiatedPlayerBallFallingAndLandingOnPlane()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");

        yield return new WaitForSeconds(2);
        LogAssert.Expect(LogType.Log, "Collision with plane");
    }

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
        playerBall.GetComponent<OnPressSpace>().Jump();
        yield return new WaitForSeconds(0.5f);
        Assert.AreNotEqual(restingY, playerBall.transform.position.y);
    }
}
