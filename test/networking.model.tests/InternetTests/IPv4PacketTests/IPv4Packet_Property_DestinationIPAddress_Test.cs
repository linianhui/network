using System;
using FluentAssertions;
using Networking.Model.Internet;
using Xunit;

namespace Networking.Model.Tests.InternetTests.IPv4PacketTests
{
    public class IPv4Packet_Property_DestinationIPAddress_Test
    {
        [Fact]
        public void Get()
        {
            var ipv4Packet = new IPv4Packet
            {
                Bytes = new Byte[32]
            };

            ipv4Packet.SetBytes(16, 4, new Byte[] { 0xC0, 0xA8, 0x01, 0x02 });

            ipv4Packet.DestinationIPAddress.ToString().Should().Be("192.168.1.2");
        }

        [Fact]
        public void Set()
        {
            var ipv4Packet = new IPv4Packet
            {
                Bytes = new Byte[32]
            };

            ipv4Packet.DestinationIPAddress = new IPAddress
            {
                Bytes = new Byte[] { 0xC0, 0xA8, 0x01, 0x02 }
            };

            ipv4Packet.DestinationIPAddress.ToString().Should().Be("192.168.1.2");
        }
    }
}
