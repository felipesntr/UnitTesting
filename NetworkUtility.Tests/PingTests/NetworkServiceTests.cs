﻿using FluentAssertions;
using FluentAssertions.Extensions;
using NetworkUtility.Ping;
using System.Net.NetworkInformation;
using Xunit.Sdk;

namespace NetworkUtility.Tests.PingTests
{
    public class NetworkServiceTests
    {
        private readonly NetworkService _pingService;
        public NetworkServiceTests()
        {
            //SUT
            _pingService = new NetworkService();
        }

        [Fact]
        public void NetworkService_SendPing_ReturnString()
        {
            //Act
            var result = _pingService.SendPing();
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
            var result = _pingService.PingTimeout(a, b);
            result.Should().Be(expected);
        }

        [Fact]
        public void NetworkService_LastPingDate_ReturnDateTime()
        {
            //Act
            var result = _pingService.LastPingDate();

            //Assert
            result.Should().BeAfter(1.January(2023));
            result.Should().BeBefore(1.June(2030));
        }

        [Fact]
        public void NetworkService_GetPingOptions_ReturnObject()
        {
            var expected = new PingOptions()
            {
                Ttl = 1,
                DontFragment = true
            };
            var result = _pingService.GetPingOptions();
            result.Should().BeOfType<PingOptions>();
            result.Should().BeEquivalentTo(expected);
            result.Ttl.Should().Be(1);
        }
    }
}
