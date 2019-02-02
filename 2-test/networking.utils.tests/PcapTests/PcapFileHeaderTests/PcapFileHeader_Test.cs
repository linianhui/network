using System;
using FluentAssertions;
using Networking.Utils.Pcap;
using Xunit;

namespace Networking.Utils.Tests.PcapTests.PcapFileHeaderTests
{
    public class PcapFileHeader_Test
    {
        [Fact]
        public void Get()
        {
            PcapFileHeader pcapFileHeader = new PcapFileHeader
            {
                Bytes = new Byte[] {
                    0xD4, 0xC3, 0xB2, 0xA1,
                    0x02, 0x00,
                    0x04, 0x00,
                    0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00,
                    0xFF, 0xFF, 0x00, 0x00,
                    0x01, 0x00, 0x00, 0x00
                }
            };

            pcapFileHeader.MagicNumber.Should().Be(0xA1B2C3D4);
            pcapFileHeader.VersionMajor.Should().Be(2);
            pcapFileHeader.VersionMinor.Should().Be(4);
            pcapFileHeader.PacketMaxLength.Should().Be(65535);
            pcapFileHeader.Type.Should().Be(DataLinkType.Ethernet);
        }
    }
}