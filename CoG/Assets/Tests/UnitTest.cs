using System;
using NUnit.Framework;

namespace UnityTest {

    [TestFixture, Category("Sample Test")]
    public class SampleTests
    {
        [Test, Category("Failing test")]
        public void failling()
        {
            Assert.Fail();
        }

        [Test, Category("Passing test")]
        public void PassingTest()
        {
            Assert.Pass();
        }
    }
}
