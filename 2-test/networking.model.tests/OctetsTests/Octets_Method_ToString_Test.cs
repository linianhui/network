using System;
using FluentAssertions;
using Xunit;

namespace Networking.Model.Tests.OctetsTests
{
    public class Octets_Method_ToString_Test
    {
        [Fact]
        public void ToString_Test()
        {
            var octets = new Octets
            (
                new Byte[] { 0x12, 0x34, 0x56 }
            );

            octets.ToString().Should().Be("12-34-56");

            octets[0] = 0xAB;
            octets.ToString().Should().Be("AB-34-56");
        }
    }
}
