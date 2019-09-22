using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Moq;
using TogglOn.Client.Abstractions;
using TogglOn.Client.AspNetCore;
using TogglOn.Contract.Models;
using Xunit;

namespace TogglOn.Tests
{
    public class Given_Evaluation_Of_FeatureToggle
    {
        public class When_Argument_is_null
        {
            [Fact]
            public void Then_FeatureToggle_is_not_Enabled()
            {
                var clientMock = new Mock<ITogglOnClient>();

                var togglOnContextAccessor = new TogglOnContextAccessor();
                togglOnContextAccessor.TogglOnContext = new TogglOnContext("DevOps", "Development", new List<FeatureGroupDto>(), new List<FeatureToggleDto>());

                var httpContextMock = new Mock<IHttpContextAccessor>();

                var evaluator = new FeatureToggleEvaluator(clientMock.Object,  togglOnContextAccessor,  httpContextMock.Object);

                var enabled = evaluator.IsEnabled(null);

                Assert.False(enabled);
            }
        }
    }
}