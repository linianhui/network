using System;

namespace Networking.Files.PcapNG
{
    public partial class SimplePacketBody
    {
        /// <summary>
        /// 首部-布局信息
        /// <see href="https://pcapng.github.io/pcapng/#section_spb"/>
        /// <para></para>
        /// <para>|                  Simple Packet Block (SPB)                    |</para>
        /// <para>|- - - - - - - -+- - - 32 bits(4 octets) - - - -+- - - - - - - -|</para>
        /// <para>|0 1 2 3 4 5 6 7+0 1 2 3 4 5 6 7+0 1 2 3 4 5 6 7+0 1 2 3 4 5 6 7|</para>
        /// <para>|- - - - - - - -+- - - - - - - -+- - - - - - - -+- - - - - - - -|</para> 
        /// <para>|            Original Packet Length                             |</para>
        /// <para>|- - - - - - - -+- - - - - - - -+- - - - - - - -+- - - - - - - -|</para>
        /// <para>|            Packet Data                                        |</para>
        /// <para>|                                                               |</para>
        /// <para>|                                                               |</para>
        /// <para>|- - - - - - - -+- - - - - - - -+- - - - - - - -+- - - - - - - -|</para> 
        /// <para></para>
        /// </summary>
        public class Layout
        {
            /// <summary>
            /// Original Length-起始位置=0
            /// </summary>
            public const Int32 OriginalLengthBegin = 0;

            /// <summary>
            /// Original Length-结束位置=4
            /// </summary>
            public const Int32 OriginalLengthEnd = OriginalLengthBegin + 4;


            /// <summary>
            /// Header Length=4
            /// </summary>
            public const Int32 HeaderLength = OriginalLengthEnd;
        }
    }
}
