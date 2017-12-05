using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfHealthCheck;

namespace WcfHealthCheck.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void HealthCheck_IsHealthy()
        {
            var healthCheck = new HealthCheck("HealthCheck_IsHealthy");

            var actual = healthCheck.GetHealth();

            Assert.IsTrue(actual.IsHealthy);
        }

        [TestMethod]
        public void HealthCheck_ExpectedName()
        {
            var healthCheck = new HealthCheck("HealthCheck_ExpectedName");

            var actual = healthCheck.GetHealth();

            Assert.IsTrue(actual.IsHealthy);
        }

        [TestMethod]
        public void ForcedFail()
        {
            Assert.IsTrue(false);
        }
    }
}
