using Microsoft.AspNetCore.Http;
using Moq;
using System.Net;
using TogglOn.Client.AspNetCore.Models;
using TogglOn.Client.AspNetCore.Models.Rules;
using Xunit;

namespace TogglOn.Tests
{
    public class GivenPercentageRule
    {
        public class WhenEvaluatingRuleBasedOnClientIp
        {
            [Fact]
            public void ThenForSeventyPercentOfClientsItShouldBeEnabled()
            {
                var toggle = new FeatureToggle("test-toggle", "DevOps", "Test", true);

                var mockedHttpContext = new Mock<HttpContext>();
                mockedHttpContext.SetupGet(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));

                toggle.Enrich(mockedHttpContext.Object);

                var rule = new PercentageRule(70);

                var actual = rule.IsEnabled(toggle);

                Assert.True(actual);
            }
        }

        public class WhenEvaluatingRuleUsingRequestStatistics
        {

        }
    }
}
