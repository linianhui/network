using System;

namespace Networking.Model.Internet
{
    /// <summary>
    /// IPv4-Packet
    /// </summary>
    public partial class IPv4Packet : InternetPacket
    {
        /// <summary>
        /// 版本
        /// </summary>
        public IPVersion Version
        {
            get { return (IPVersion)(base[Layout.VersionBegin] >> 4); }
            set
            {
                var old = base[Layout.HeaderLengthBegin];
                base[Layout.VersionBegin] = (Byte)(((Byte)value) << 4 | old & 0x0F);
            }
        }

        /// <summary>
        /// 头部长度，单位4 octets
        /// </summary>
        public Byte HeaderLength
        {
            get { return (Byte)(base[Layout.HeaderLengthBegin] & 0x0F); }
            set
            {
                var old = base[Layout.HeaderLengthBegin];
                base[Layout.HeaderLengthBegin] = (Byte)(old & 0xF0 | value & 0x0F);
            }
        }

        /// <summary>
        /// 总长度
        /// </summary>
        public UInt16 TotalLength
        {
            get
            {
                return ReadAsUInt16FromBigEndian(Layout.TotalLengthBegin);
            }
            set
            {
                WriteUInt16ToBigEndian(Layout.TotalLengthBegin, value);
            }
        }

        /// <summary>
        /// Id
        /// </summary>
        public UInt16 Id
        {
            get
            {
                return ReadAsUInt16FromBigEndian(Layout.IdBegin);
            }
            set
            {
                WriteUInt16ToBigEndian(Layout.IdBegin, value);
            }
        }

        /// <summary>
        /// Time to Live
        /// </summary>
        public Byte TTL
        {
            get { return base[Layout.TTLBegin]; }
            set { base[Layout.TTLBegin] = value; }
        }

        /// <summary>
        /// 协议类型
        /// </summary>
        public IPPacketType PacketType
        {
            get { return (IPPacketType)base[Layout.ProtocolBegin]; }
            set { base[Layout.ProtocolBegin] = (Byte)value; }
        }

        /// <summary>
        /// Id
        /// </summary>
        public UInt16 HeaderChecksum
        {
            get
            {
                return ReadAsUInt16FromBigEndian(Layout.HeaderChecksumBegin);
            }
            set
            {
                WriteUInt16ToBigEndian(Layout.HeaderChecksumBegin, value);
            }
        }

        /// <summary>
        /// 源IP地址
        /// </summary>
        public IPAddress SourceIPAddress
        {
            get
            {
                return new IPAddress
                {
                    Bytes = base[Layout.SourceIPAddressBegin, IPAddress.Layout.V4Length]
                };
            }
            set
            {
                base[Layout.SourceIPAddressBegin, IPAddress.Layout.V4Length] = value.Bytes;
            }
        }


        /// <summary>
        /// 目标IP地址
        /// </summary>
        public IPAddress DestinationIPAddress
        {
            get
            {
                return new IPAddress
                {
                    Bytes = base[Layout.DestinationIPAddressBegin, IPAddress.Layout.V4Length]
                };
            }
            set
            {
                base[Layout.DestinationIPAddressBegin, IPAddress.Layout.V4Length] = value.Bytes;
            }
        }


    }
}
