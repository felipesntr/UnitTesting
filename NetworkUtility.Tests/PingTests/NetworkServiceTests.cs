using FluentAssertions;
using NetworkUtility.Ping;
using Xunit.Sdk;

namespace NetworkUtility.Tests.PingTests
{
    public class NetworkServiceTests
    {
        [Fact]
        public void NetworkService_SendPing_ReturnString()
        {
            //Arrange - variables, classes, mocks
            var pingService = new NetworkService();
            //Act
            var result = pingService.SendPing();
            //Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Success: Ping Sent!");
            result.Should().Contain("Success", Exactly.Once());
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(2, 3, 5)]
        [InlineData(2, 5, 7)]
        public void NetworkService_PingTimeout_ReturnInt(int a, int b, int expected)
        {
            var pingService = new NetworkService();
            var result = pingService.PingTimeout(a, b);
            result.Should().Be(expected);
        }
    }
}
