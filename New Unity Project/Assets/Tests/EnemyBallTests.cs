using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;


/// <summary>
/// Test class for Enemy Ball
/// </summary>
public class EnemyBallTests
{

    /// <summary>
    /// Verify that Enemy Ball moves to collide closest player ball.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator _EnemyBallCollideClosestPlayerBall()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("MultipleBallAndPlaneScene"), "Level could not be loaded.");
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MultipleBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        yield return new WaitForSeconds(2.5f);
        var enemyBall = GameObject.FindWithTag("Enemy");
        Assert.True(enemyBall.GetComponent<OnCollision>().getTouchingPlayerBall(), "Enemy ball is expected to touch player ball.");
        var playerBalls = GameObject.FindGameObjectsWithTag("Player");
        Assert.True(playerBalls.Length == 2);
        Assert.True(playerBalls[0].GetComponent<OnCollision>().getTouchingEnemyBall(), "First player ball is expected to touch enemy ball.");
        Assert.False(playerBalls[1].GetComponent<OnCollision>().getTouchingEnemyBall(), "Second player ball is not expected to touch enemy ball.");
    }
}
