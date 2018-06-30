using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PickTests {

    [Test]
    public void SuccessTest() {
        // Use the Assert class to test conditions.
        Assert.AreEqual(1, 1, 0, "One and one are equal");

    }
    [Test]
    public void FailTest() {
        // Use the Assert class to test conditions.
        Assert.AreEqual(1, 0, 0, "One and zero is not equal");

    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        Assert.AreEqual(1, 1, 0);
        yield return null;
    }
}
