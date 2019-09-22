 using Microsoft.AspNetCore.Http;
using Moq;
using System.Net;
 using TogglOn.Client.AspNetCore;
 using TogglOn.Client.AspNetCore.Models;
using TogglOn.Client.AspNetCore.Models.Rules;
using Xunit;

namespace TogglOn.Tests
{
    public class Given_PercentageRule
    {
        public class When_Enabled_For_70_Percent_Of_Clients
        {
            [Fact]
            public void Then_It_Should_Be_Enabled()
            {
                var toggle = new FeatureToggle("test-toggle", "DevOps", "Test", true);

                var mockedHttpContext = new Mock<HttpContext>();
                mockedHttpContext.SetupGet(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));

                toggle.Enrich(mockedHttpContext.Object);

                var rule = new PercentageRule(70);

                var actual = rule.IsEnabled(toggle);

                Assert.True(actual);
            }

            [Fact]
            public void Then_It_Should_Not_Be_Enabled()
            {
                var toggle = new FeatureToggle("test-toggle", "DevOps", "Test", true);

                var mockedHttpContext = new Mock<HttpContext>();
                mockedHttpContext.SetupGet(x => x.Connection.RemoteIpAddress).Returns(IPAddress.Parse("127.0.0.1"));

                toggle.Enrich(mockedHttpContext.Object);

                var rule = new PercentageRule(70);

                var actual = rule.IsEnabled(toggle);

                Assert.False(actual);
            }
        }
    }
}
