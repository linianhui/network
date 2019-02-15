using System;

namespace Networking.Model
{
    /// <summary>
    /// bit操作
    /// </summary>
    public static class Bits
    {
        /// <summary>
        /// 15=0x0F=0B_0000_1111
        /// </summary>
        public const Byte B_0000_1111 = 0B_0000_1111;

        /// <summary>
        /// 240=0xF0=0B_1111_0000
        /// </summary>
        public const Byte B_1111_0000 = 0B_1111_0000;

        /// <summary>
        /// 获取指定位置的bit[1=true,0=false]
        /// </summary>
        /// <param name="this">this</param>
        /// <param name="bitIndex">bit的索引[0-7]</param>
        /// <returns></returns>
        public static Boolean GetBit(this Byte @this, Byte bitIndex)
        {
            CheckIndex(bitIndex, 7);

            var bits = @this >> (7 - bitIndex);
            var bit = bits & 1;
            return bit == 1;
        }

        /// <summary>
        /// 设置指定位置的bit[1=true,0=false]
        /// </summary>
        /// <param name="this">this</param>
        /// <param name="bitIndex">bit的索引[0-7]</param>
        /// <param name="bitValue">bit的值</param>
        /// <returns></returns>
        public static Byte SetBit(ref this Byte @this, Byte bitIndex, Boolean bitValue)
        {
            CheckIndex(bitIndex, 7);

            var bits = 1 << (7 - bitIndex);
            if (bitValue)
            {
                @this |= (Byte)bits;
            }
            else
            {
                @this &= (Byte)~bits;
            }

            return @this;
        }

        /// <summary>
        /// 获取指定位置的bits组成的Byte
        /// </summary>
        /// <param name="this">this</param>
        /// <param name="bitIndex">bit的索引[0-7]</param>
        /// <param name="bitLength">bit的长度[0-8]</param>
        /// <returns></returns>
        public static Byte GetRange(this Byte @this, Byte bitIndex, Byte bitLength)
        {
            CheckIndexAndLength(bitIndex, bitLength);

            var leftShiftResult = (Byte)(@this << bitIndex);
            var rightShift = 8 - bitLength;
            return (Byte)(leftShiftResult >> rightShift);
        }

        private static void CheckIndexAndLength(Byte bitIndex, Byte bitLength)
        {
            CheckIndex(bitIndex, 7);
            if (bitIndex + bitLength > 8)
            {
                throw new IndexOutOfRangeException(
                    $"{nameof(bitIndex)}+{nameof(bitLength)}={bitIndex}+{bitLength}={bitIndex + bitLength} greater than 8.");
            }
        }

        private static void CheckIndex(Byte index, Byte length)
        {
            if (index > length)
            {
                throw new IndexOutOfRangeException($"{nameof(index)}={index} not in [0-7].");
            }
        }
    }
}
