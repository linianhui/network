using FluentAssertions;
using Networking.Files.Pcap;
using Xunit;

namespace Networking.Files.Tests.PcapTests.PcapFileHeaderTests
{
    public class PcapFileHeader_Layout_Test
    {
        [Fact]
        public void Layout()
        {
            PcapFileHeader.Layout.MagicNumberBegin.Should().Be(0);
            PcapFileHeader.Layout.MagicNumberEnd.Should().Be(4);

            PcapFileHeader.Layout.VersionMajorBegin.Should().Be(4);
            PcapFileHeader.Layout.VersionMajorEnd.Should().Be(6);

            PcapFileHeader.Layout.VersionMinorBegin.Should().Be(6);
            PcapFileHeader.Layout.VersionMinorEnd.Should().Be(8);

            PcapFileHeader.Layout.PacketMaxLengthBegin.Should().Be(16);
            PcapFileHeader.Layout.PacketMaxLengthEnd.Should().Be(20);

            PcapFileHeader.Layout.DataLinkTypeBegin.Should().Be(20);
            PcapFileHeader.Layout.DataLinkTypeEnd.Should().Be(24);

            PcapFileHeader.Layout.HeaderLength.Should().Be(24);
        }
    }
}