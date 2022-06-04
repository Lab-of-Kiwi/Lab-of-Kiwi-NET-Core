using System;
using System.IO;
using System.Text;
using Xunit;

namespace LabOfKiwi.IO;

public class StreamExtensionTests
{
    [Fact]
    public void Test_ReadBoolean()
    {
        var stream = ToStream(0, 1, 2);
        Assert.False(stream.ReadBoolean());
        Assert.True(stream.ReadBoolean());
        Assert.True(stream.ReadBoolean());
        Assert.Throws<EndOfStreamException>(() => stream.ReadBoolean());
        stream = null;
        Assert.Throws<ArgumentNullException>(() => stream.ReadBoolean());
    }

    [Fact]
    public void Test_WriteBoolean()
    {
        var stream = new MemoryStream();
        stream.Write(true);
        stream.Write(false);
        byte[] buffer = stream.ToArray();
        Assert.Equal(2, buffer.Length);
        Assert.Equal(1, buffer[0]);
        Assert.Equal(0, buffer[1]);
    }

    [Fact]
    public void Test_ReadDouble()
        => Test_ReadNumber(new byte[] { 0x40, 0x09, 0x21, 0xFB, 0x54, 0x44, 0x2D, 0x18 }, Math.PI, (s, e) => s.ReadDouble(e));

    [Fact]
    public void Test_WriteDouble()
        => Test_WriteNumber(Math.PI, new byte[] { 0x40, 0x09, 0x21, 0xFB, 0x54, 0x44, 0x2D, 0x18 }, (v, s, e) => s.Write(v, e));

    [Fact]
    public void Test_ReadFully()
    {
        var stream = ToStream(0x10, 0x20, 0x30, 0x40, 0x50);
        byte[] buffer = new byte[4];
        stream.ReadFully(buffer);

        Assert.Equal(0x10, buffer[0]);
        Assert.Equal(0x20, buffer[1]);
        Assert.Equal(0x30, buffer[2]);
        Assert.Equal(0x40, buffer[3]);

        Assert.Throws<EndOfStreamException>(() => stream.ReadFully(buffer));
        stream = null;
        Assert.Throws<ArgumentNullException>(() => stream.ReadFully(buffer));
    }

    [Fact]
    public void Test_ReadFully_WithOffset()
    {
        var stream = ToStream(0x10, 0x20, 0x30, 0x40, 0x50);
        byte[] buffer = new byte[5];
        stream.ReadFully(buffer, 1, 4);

        Assert.Equal(0x00, buffer[0]);
        Assert.Equal(0x10, buffer[1]);
        Assert.Equal(0x20, buffer[2]);
        Assert.Equal(0x30, buffer[3]);
        Assert.Equal(0x40, buffer[4]);

        Assert.Throws<EndOfStreamException>(() => stream.ReadFully(buffer));

        Assert.ThrowsAny<ArgumentException>(() => stream.ReadFully(buffer, -1, 0));
        Assert.ThrowsAny<ArgumentException>(() => stream.ReadFully(buffer, 0, -1));
        Assert.ThrowsAny<ArgumentException>(() => stream.ReadFully(buffer, -1, -1));
        Assert.ThrowsAny<ArgumentException>(() => stream.ReadFully(buffer, 0, 10));
        Assert.ThrowsAny<ArgumentException>(() => stream.ReadFully(buffer, 10, 4));
        Assert.ThrowsAny<ArgumentException>(() => stream.ReadFully(buffer, 3, 4));

        stream.ReadFully(buffer, 5, 0);

        stream = null;
        Assert.Throws<ArgumentNullException>(() => stream.ReadFully(buffer));
    }

    [Fact]
    public void Test_ReadInt8()
    {
        var stream = ToStream(0x00, 0x7F, 0xFF);
        Assert.Equal(0, stream.ReadInt8());
        Assert.Equal(127, stream.ReadInt8());
        Assert.Equal(-1, stream.ReadInt8());
        Assert.Throws<EndOfStreamException>(() => stream.ReadInt8());
        stream = null;
        Assert.Throws<ArgumentNullException>(() => stream.ReadInt8());
    }

    [Fact]
    public void Test_WriteInt8()
    {
        var stream = new MemoryStream();
        stream.Write((sbyte)-1);
        stream.Write((sbyte)127);
        byte[] buffer = stream.ToArray();
        Assert.Equal(2, buffer.Length);
        Assert.Equal(0xFF, buffer[0]);
        Assert.Equal(0x7F, buffer[1]);
    }

    [Fact]
    public void Test_ReadInt16()
        => Test_ReadNumber(new byte[] { 0x93, 0xD0 }, (short)-27696, (s, e) => s.ReadInt16(e));

    [Fact]
    public void Test_WriteInt16()
        => Test_WriteNumber((short)-27696, new byte[] { 0x93, 0xD0 }, (v, s, e) => s.Write(v, e));

    [Fact]
    public void Test_ReadInt32()
        => Test_ReadNumber(new byte[] { 0x93, 0xD0, 0x2A, 0xB7 }, -1815074121, (s, e) => s.ReadInt32(e));

    [Fact]
    public void Test_WriteInt32()
        => Test_WriteNumber(-1815074121, new byte[] { 0x93, 0xD0, 0x2A, 0xB7 }, (v, s, e) => s.Write(v, e));

    [Fact]
    public void Test_ReadInt64()
        => Test_ReadNumber(new byte[] { 0x93, 0xD0, 0x2A, 0xB7, 0x30, 0x69, 0x6D, 0x64 }, -7795683988698731164L, (s, e) => s.ReadInt64(e));

    [Fact]
    public void Test_WriteInt64()
        => Test_WriteNumber(-7795683988698731164L, new byte[] { 0x93, 0xD0, 0x2A, 0xB7, 0x30, 0x69, 0x6D, 0x64 }, (v, s, e) => s.Write(v, e));

    [Fact]
    public void Test_ReadSingle()
        => Test_ReadNumber(new byte[] { 0x3E, 0xAA, 0xAA, 0xAB }, 1.0F / 3.0F, (s, e) => s.ReadSingle(e));

    [Fact]
    public void Test_WriteSingle()
        => Test_WriteNumber(1.0F / 3.0F, new byte[] { 0x3E, 0xAA, 0xAA, 0xAB }, (v, s, e) => s.Write(v, e));

    [Fact]
    public void Test_ReadString()
    {
        byte[] str = Encoding.UTF8.GetBytes("Hello, World!");
        byte[] buffer = new byte[str.Length + 1];
        Array.Copy(str, 0, buffer, 0, str.Length);
        buffer[str.Length] = 0;

        var stream = ToStream(buffer);
        Assert.Equal("Hello, World!", stream.ReadString());
        Assert.Throws<EndOfStreamException>(() => stream.ReadString());
        stream = null;
        Assert.Throws<ArgumentNullException>(() => stream.ReadString());
    }

    [Fact]
    public void Test_ReadString_NoNullChar()
    {
        var stream = ToStream(Encoding.UTF8.GetBytes("Hello, World!"));
        Assert.Throws<EndOfStreamException>(() => stream.ReadString());
    }

    [Fact]
    public void Test_ReadString_TwoStrings()
    {
        var stream = ToStream((byte)'H', (byte)'i', 0, (byte)'F', (byte)'o', (byte)'o', 0);
        Assert.Equal("Hi", stream.ReadString());
        Assert.Equal("Foo", stream.ReadString());
    }

    [Fact]
    public void Test_ReadString_Empty()
    {
        var stream = ToStream((byte)'H', (byte)'i', 0, 0);
        Assert.Equal("Hi", stream.ReadString());
        Assert.Equal(string.Empty, stream.ReadString());
    }

    [Fact]
    public void Test_ReadUInt8()
    {
        var stream = ToStream(0x00, 0x7F, 0xFF);
        Assert.Equal(0, stream.ReadUInt8());
        Assert.Equal(127, stream.ReadUInt8());
        Assert.Equal(255, stream.ReadUInt8());
        Assert.Throws<EndOfStreamException>(() => stream.ReadUInt8());
        stream = null;
        Assert.Throws<ArgumentNullException>(() => stream.ReadUInt8());
    }

    [Fact]
    public void Test_WriteUInt8()
    {
        var stream = new MemoryStream();
        stream.Write((byte)0x45);
        stream.Write((byte)0xFF);
        byte[] buffer = stream.ToArray();
        Assert.Equal(2, buffer.Length);
        Assert.Equal(0x45, buffer[0]);
        Assert.Equal(0xFF, buffer[1]);
    }

    [Fact]
    public void Test_ReadUInt16()
        => Test_ReadNumber(new byte[] { 0x93, 0xD0 }, (ushort)37840, (s, e) => s.ReadUInt16(e));

    [Fact]
    public void Test_WriteUInt16()
        => Test_WriteNumber((ushort)37840, new byte[] { 0x93, 0xD0 }, (v, s, e) => s.Write(v, e));

    [Fact]
    public void Test_ReadUInt32()
        => Test_ReadNumber(new byte[] { 0x93, 0xD0, 0x2A, 0xB7 }, 2479893175U, (s, e) => s.ReadUInt32(e));

    [Fact]
    public void Test_WriteUInt32()
        => Test_WriteNumber(2479893175U, new byte[] { 0x93, 0xD0, 0x2A, 0xB7 }, (v, s, e) => s.Write(v, e));

    [Fact]
    public void Test_ReadUInt64()
        => Test_ReadNumber(new byte[] { 0x93, 0xD0, 0x2A, 0xB7, 0x30, 0x69, 0x6D, 0x64 }, 10651060085010820452UL, (s, e) => s.ReadUInt64(e));

    [Fact]
    public void Test_WriteUInt64()
        => Test_WriteNumber(10651060085010820452UL, new byte[] { 0x93, 0xD0, 0x2A, 0xB7, 0x30, 0x69, 0x6D, 0x64 }, (v, s, e) => s.Write(v, e));

    private void Test_ReadNumber<T>(byte[] buffer, T value, Func<Stream, Endianness, T> action)
    {
        var stream = ToStream(buffer);
        Assert.Equal(value, action(stream, Endianness.BigEndian));
        Array.Reverse(buffer);
        stream = ToStream(buffer);
        Assert.Equal(value, action(stream, Endianness.LittleEndian));
        Assert.Throws<EndOfStreamException>(() => action(stream, default));
        Assert.Throws<ArgumentNullException>(() => action(default, default));
    }

    private void Test_WriteNumber<T>(T value, byte[] expected, Action<T, Stream, Endianness> action)
    {
        var stream = new MemoryStream();
        action(value, stream, Endianness.BigEndian);

        byte[] actual = stream.ToArray();
        AssertSame(expected, actual);

        Array.Reverse(expected);
        stream = new MemoryStream();
        action(value, stream, Endianness.LittleEndian);
        actual = stream.ToArray();
        AssertSame(expected, actual);
    }

    private static MemoryStream ToStream(params byte[] buffer)
        => new MemoryStream(buffer);

    private static void AssertSame(byte[] expected, byte[] actual)
    {
        Assert.True(AreSame(expected, actual), "Byte arrays are not structurally identical.");
    }

    private static bool AreSame(byte[] arr1, byte[] arr2)
    {
        if (arr1 == arr2)
        {
            return true;
        }
        
        if (arr1 == null || arr2 == null)
        {
            return false;
        }

        if (arr1.Length != arr2.Length)
        {
            return false;
        }

        for (int i = 0; i < arr1.Length; i++)
        {
            if (arr1[0] != arr2[0])
            {
                return false;
            }
        }

        return true;
    }
}
