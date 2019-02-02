using System;

namespace Networking.Utils.Pcap
{
    /// <summary>
    /// 数据链路的类型
    /// <see href="http://www.tcpdump.org/linktypes.html"/>
    /// </summary>
    public enum DataLinkType : UInt32
    {
        /// <summary>
        /// null
        /// </summary>
        Null = 0,

        /// <summary>
        /// Ethernet (DIX) II
        /// </summary>
        Ethernet = 1,

        /// <summary>
        /// Point to Point
        /// </summary>
        PPP = 9
    }
}