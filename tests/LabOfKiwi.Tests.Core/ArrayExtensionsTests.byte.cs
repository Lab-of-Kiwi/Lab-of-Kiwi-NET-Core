using System;
using Xunit;

namespace LabOfKiwi
{
    public partial class ArrayExtensionsTests
    {
        [Fact]
        public void Test_ToHexString_Empty()
        {
            byte[] array = Array.Empty<byte>();
            Assert.Equal(string.Empty, array.ToHexString(uppercase: false, delimiter: null));
            Assert.Equal(string.Empty, array.ToHexString(uppercase: true, delimiter: null));
            Assert.Equal(string.Empty, array.ToHexString(uppercase: false, delimiter: " "));
            Assert.Equal(string.Empty, array.ToHexString(uppercase: true, delimiter: " "));
        }

        [Fact]
        public void Test_ToHexString_Single()
        {
            byte[] array = new byte[] { 0x0F };
            Assert.Equal("0f", array.ToHexString(uppercase: false, delimiter: null));
            Assert.Equal("0F", array.ToHexString(uppercase: true, delimiter: null));
            Assert.Equal("0f", array.ToHexString(uppercase: false, delimiter: " "));
            Assert.Equal("0F", array.ToHexString(uppercase: true, delimiter: " "));
        }

        [Fact]
        public void Test_ToHexString_Multi()
        {
            byte[] array = new byte[] { 0x0F, 0x1C, 0x56 };
            Assert.Equal("0f1c56", array.ToHexString(uppercase: false, delimiter: null));
            Assert.Equal("0F1C56", array.ToHexString(uppercase: true, delimiter: null));
            Assert.Equal("0f 1c 56", array.ToHexString(uppercase: false, delimiter: " "));
            Assert.Equal("0F 1C 56", array.ToHexString(uppercase: true, delimiter: " "));
        }

        [Fact]
        public void Test_ToBinaryString_Empty()
        {
            byte[] array = Array.Empty<byte>();
            Assert.Equal(string.Empty, array.ToBinaryString(delimiter: null, groupByNibbles: false));
            Assert.Equal(string.Empty, array.ToBinaryString(delimiter: " ", groupByNibbles: false));
            Assert.Equal(string.Empty, array.ToBinaryString(delimiter: null, groupByNibbles: true));
            Assert.Equal(string.Empty, array.ToBinaryString(delimiter: " ", groupByNibbles: true));
        }

        [Fact]
        public void Test_ToBinaryString_Single()
        {
            byte[] array = new byte[] { 0x0F };
            Assert.Equal("00001111", array.ToBinaryString(delimiter: null, groupByNibbles: false));
            Assert.Equal("00001111", array.ToBinaryString(delimiter: " ", groupByNibbles: false));
            Assert.Equal("00001111", array.ToBinaryString(delimiter: null, groupByNibbles: true));
            Assert.Equal("0000 1111", array.ToBinaryString(delimiter: " ", groupByNibbles: true));
        }

        [Fact]
        public void Test_ToBinaryString_Multi()
        {
            byte[] array = new byte[] { 0x0F, 0x1C, 0x56 };
            Assert.Equal("000011110001110001010110", array.ToBinaryString(delimiter: null, groupByNibbles: false));
            Assert.Equal("00001111 00011100 01010110", array.ToBinaryString(delimiter: " ", groupByNibbles: false));
            Assert.Equal("000011110001110001010110", array.ToBinaryString(delimiter: null, groupByNibbles: true));
            Assert.Equal("0000 1111 0001 1100 0101 0110", array.ToBinaryString(delimiter: " ", groupByNibbles: true));
        }
    }
}
