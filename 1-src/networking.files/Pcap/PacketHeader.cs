using System;

namespace Networking.Files.Pcap
{
    /// <summary>
    /// Packet (Record) 首部
    /// <see href="https://wiki.wireshark.org/Development/LibpcapFileFormat"/>
    /// </summary>
    public partial class PacketHeader : Octets
    {
        /// <summary>
        /// 文件首部
        /// </summary>
        public PcapFileHeader FileHeader { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PacketHeader(PcapFileHeader fileHeader, Memory<Byte> headerBytes)
        {
            FileHeader = fileHeader;
            IsLittleEndian = fileHeader.IsLittleEndian;
            Bytes = headerBytes;
            TimestampSecond = GetUInt32(Layout.TimestampSecondBegin);
            TimestampMicrosecond = GetUInt32(Layout.TimestampMicrosecondBegin);
            TimestampNanosecond = ComputeNanosecond();
            CapturedLength = GetUInt32(Layout.CapturedLengthBegin);
            OriginalLength = GetUInt32(Layout.OriginalLengthBegin);
        }

        /// <summary>
        /// 时间戳-秒部分
        /// </summary>
        public UInt32 TimestampSecond { get; }

        /// <summary>
        /// 时间戳-微妙部分
        /// </summary>
        public UInt32 TimestampMicrosecond { get; }

        /// <summary>
        /// 捕获的长度
        /// </summary>
        public UInt32 CapturedLength { get; }

        /// <summary>
        /// 原始长度
        /// </summary>
        public UInt32 OriginalLength { get; }

        /// <summary>
        /// UNIX时间戳-精度纳秒
        /// </summary>
        public UInt64 TimestampNanosecond { get; }

        private UInt64 ComputeNanosecond()
        {
            UInt64 nanosecond = TimestampMicrosecond;
            if (FileHeader.IsNanosecond == false)
            {
                nanosecond *= 1000;
            }

            return TimestampSecond * 1_000_000_000UL + nanosecond;
        }
    }
}
