using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

/// <summary>
/// Test class for Main Camera
/// </summary>
public class MainCameraTests {

    /// <summary>
    /// Verify that the Main Camera moves left when Player ball moves left.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator _CameraAndPlayerBallMoveLeft()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        var mainCamera = GameObject.FindWithTag("MainCamera");
        yield return new WaitForSeconds(1);
        Assert.AreEqual(playerBall.transform.position.x, mainCamera.transform.position.x, 0.05);
        playerBall.GetComponent<Movement>().MoveLeft();
        yield return new WaitForSeconds(3f);
        Assert.AreEqual(playerBall.transform.position.x, mainCamera.transform.position.x, 0.05);
    }

    /// <summary>
    /// Verify that the Main Camera moves right when Player ball moves right.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator _CameraAndPlayerBallMoveRight()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        var mainCamera = GameObject.FindWithTag("MainCamera");
        yield return new WaitForSeconds(1);
        Assert.AreEqual(playerBall.transform.position.x, mainCamera.transform.position.x, 0.05);
        playerBall.GetComponent<Movement>().MoveRight();
        yield return new WaitForSeconds(3f);
        Assert.AreEqual(playerBall.transform.position.x, mainCamera.transform.position.x, 0.05);
    }

    /// <summary>
    /// Verify that the Main Camera moves up when Player ball moves up.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator _CameraAndPlayerBallMoveUp()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        var mainCamera = GameObject.FindWithTag("MainCamera");
        yield return new WaitForSeconds(1);
        Assert.AreEqual(playerBall.transform.position.z, mainCamera.transform.position.z + 20, 0.05);
        playerBall.GetComponent<Movement>().MoveUp();
        yield return new WaitForSeconds(3f);
        Assert.AreEqual(playerBall.transform.position.z, mainCamera.transform.position.z + 20, 0.05);
    }

    /// <summary>
    /// Verify that the Main Camera moves down when Player ball moves down.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator _CameraAndPlayerBallMoveDown()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        var mainCamera = GameObject.FindWithTag("MainCamera");
        yield return new WaitForSeconds(1);
        Assert.AreEqual(playerBall.transform.position.z, mainCamera.transform.position.z + 20, 0.05);
        playerBall.GetComponent<Movement>().MoveDown();
        yield return new WaitForSeconds(3f);
        Assert.AreEqual(playerBall.transform.position.z, mainCamera.transform.position.z + 20, 0.05);
    }

    /// <summary>
    /// Verify that the Main Camera does not rotate when Player ball moves.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator _CameraDontRotateWhenPlayerBallMove()
    {
        Assert.True(Application.CanStreamedLevelBeLoaded("PlayerBallAndPlaneScene"));
        AsyncOperation loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("PlayerBallAndPlaneScene");
        yield return loadSceneAsync;
        Debug.Log("Loading complete");
        var playerBall = GameObject.FindWithTag("Player");
        var mainCamera = GameObject.FindWithTag("MainCamera");
        yield return new WaitForSeconds(1);
        var cameraRotation = mainCamera.transform.rotation;
        playerBall.GetComponent<Movement>().MoveDown();
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(cameraRotation.x, mainCamera.transform.rotation.x, 0.01);
        Assert.AreEqual(cameraRotation.y, mainCamera.transform.rotation.y, 0.01);
        Assert.AreEqual(cameraRotation.z, mainCamera.transform.rotation.z, 0.01);
        Assert.AreEqual(cameraRotation.w, mainCamera.transform.rotation.w, 0.01);
        playerBall.GetComponent<Movement>().MoveUp();
        playerBall.GetComponent<Movement>().MoveUp();
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(cameraRotation.x, mainCamera.transform.rotation.x, 0.01);
        Assert.AreEqual(cameraRotation.y, mainCamera.transform.rotation.y, 0.01);
        Assert.AreEqual(cameraRotation.z, mainCamera.transform.rotation.z, 0.01);
        Assert.AreEqual(cameraRotation.w, mainCamera.transform.rotation.w, 0.01);
        playerBall.GetComponent<Movement>().MoveRight();
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(cameraRotation.x, mainCamera.transform.rotation.x, 0.01);
        Assert.AreEqual(cameraRotation.y, mainCamera.transform.rotation.y, 0.01);
        Assert.AreEqual(cameraRotation.z, mainCamera.transform.rotation.z, 0.01);
        Assert.AreEqual(cameraRotation.w, mainCamera.transform.rotation.w, 0.01);
        playerBall.GetComponent<Movement>().MoveLeft();
        playerBall.GetComponent<Movement>().MoveLeft();
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(cameraRotation.x, mainCamera.transform.rotation.x, 0.01);
        Assert.AreEqual(cameraRotation.y, mainCamera.transform.rotation.y, 0.01);
        Assert.AreEqual(cameraRotation.z, mainCamera.transform.rotation.z, 0.01);
        Assert.AreEqual(cameraRotation.w, mainCamera.transform.rotation.w, 0.01);
    }
}
