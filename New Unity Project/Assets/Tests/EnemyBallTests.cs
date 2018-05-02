using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class EnemyBallTests {
	// A UnityTest behaves like a coroutine in PlayMode
	// and allows you to yield null to skip a frame in EditMode
	[UnityTest]
	public IEnumerator EnemyBallTestsWithEnumeratorPasses() {
		// Use the Assert class to test conditions.
		// yield to skip a frame
		yield return null;

        Assert.True(Application.CanStreamedLevelBeLoaded("MultipleBallAndPlaneScene"), "Level could not be loaded.");
    }
}
