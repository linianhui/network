using System;
using Networking.Model.Internet;

namespace Networking.Model.DataLink
{
    /// <summary>
    /// Ethernet II(DIX) Frame
    /// <see href="https://en.wikipedia.org/wiki/ethernet_frame"/>
    /// </summary>
    public partial class EthernetFrame : DataLinkFrame
    {
        /// <summary>
        /// 目标MAC地址
        /// </summary>
        public MACAddress DestinationMACAddress
        {
            get
            {
                return new MACAddress
                {
                    Bytes = base[Layout.DestinationMACAddressBegin, MACAddress.Layout.Length]
                };
            }
            set
            {
                base[Layout.DestinationMACAddressBegin, MACAddress.Layout.Length] = value.Bytes;
            }
        }

        /// <summary>
        /// 源MAC地址
        /// </summary>
        public MACAddress SourceMACAddress
        {
            get
            {
                return new MACAddress
                {
                    Bytes = base[Layout.SourceMACAddressBegin, MACAddress.Layout.Length]
                };
            }
            set
            {
                base[Layout.SourceMACAddressBegin, MACAddress.Layout.Length] = value.Bytes;
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        public EthernetFrameType Type
        {
            get
            {
                return (EthernetFrameType)ReadAsUInt16FromBigEndian(Layout.TypeBegin);
            }
            set
            {
                WriteUInt16ToBigEndian(Layout.TypeBegin, (UInt16)value);
            }
        }

        /// <summary>
        /// 负载信息
        /// </summary>
        public Octets Payload
        {
            get
            {
                switch (Type)
                {

                    case EthernetFrameType.IPv4:
                        return new IPv4Packet
                        {
                            Bytes = Slice(Layout.HeaderLength)
                        };
                    case EthernetFrameType.ARP:
                        return new ARPFrame
                        {
                            Bytes = Slice(Layout.HeaderLength)
                        };
                    default:
                        return new Octets
                        {
                            Bytes = Slice(Layout.HeaderLength)
                        };
                }
            }
        }
    }
}
