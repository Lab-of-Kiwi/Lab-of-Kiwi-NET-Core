using System;
using System.IO;
using System.Text;

namespace LabOfKiwi.IO;

public static partial class StreamExtensions
{
    /// <summary>
    /// Writes to the provided <see cref="Stream"/> a <see cref="bool"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="bool"/> value to write.</param>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/>is <c>null</c>.</exception>
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="ObjectDisposedException"/>
    public static void Write(this Stream stream, bool value)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        stream.WriteByte(value ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// Writes to the provided <see cref="Stream"/> a <see cref="byte"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="byte"/> value to write.</param>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/>is <c>null</c>.</exception>
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="ObjectDisposedException"/>
    public static void Write(this Stream stream, byte value)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        stream.WriteByte(value);
    }

    /// <summary>
    /// Writes to the provided <see cref="Stream"/> a <see cref="double"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="double"/> value to write.</param>
    /// <param name="endianness">The <see cref="IO.Endianness"/> of <paramref name="stream"/>.</param>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/>is <c>null</c>.</exception>
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="ObjectDisposedException"/>
    public static void Write(this Stream stream, double value, Endianness endianness = Endianness.Native)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        Span<byte> buffer = BitConverter.GetBytes(value);
        Reorder(buffer, endianness);

        stream.Write(buffer);
    }

    /// <summary>
    /// Writes to the provided <see cref="Stream"/> a <see cref="float"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="float"/> value to write.</param>
    /// <param name="endianness">The <see cref="IO.Endianness"/> of <paramref name="stream"/>.</param>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/>is <c>null</c>.</exception>
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="ObjectDisposedException"/>
    public static void Write(this Stream stream, float value, Endianness endianness = Endianness.Native)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        Span<byte> buffer = BitConverter.GetBytes(value);
        Reorder(buffer, endianness);

        stream.Write(buffer);
    }

    /// <summary>
    /// Writes to the provided <see cref="Stream"/> an <see cref="int"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="int"/> value to write.</param>
    /// <param name="endianness">The <see cref="IO.Endianness"/> of <paramref name="stream"/>.</param>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/>is <c>null</c>.</exception>
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="ObjectDisposedException"/>
    public static void Write(this Stream stream, int value, Endianness endianness = Endianness.Native)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        Span<byte> buffer = BitConverter.GetBytes(value);
        Reorder(buffer, endianness);

        stream.Write(buffer);
    }

    /// <summary>
    /// Writes to the provided <see cref="Stream"/> a <see cref="long"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="long"/> value to write.</param>
    /// <param name="endianness">The <see cref="IO.Endianness"/> of <paramref name="stream"/>.</param>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/>is <c>null</c>.</exception>
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="ObjectDisposedException"/>
    public static void Write(this Stream stream, long value, Endianness endianness = Endianness.Native)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        Span<byte> buffer = BitConverter.GetBytes(value);
        Reorder(buffer, endianness);

        stream.Write(buffer);
    }

    /// <summary>
    /// Writes to the provided <see cref="Stream"/> a <see cref="sbyte"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="sbyte"/> value to write.</param>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/>is <c>null</c>.</exception>
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="ObjectDisposedException"/>
    public static void Write(this Stream stream, sbyte value)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        stream.WriteByte(unchecked((byte)value));
    }

    /// <summary>
    /// Writes to the provided <see cref="Stream"/> a <see cref="short"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="short"/> value to write.</param>
    /// <param name="endianness">The <see cref="IO.Endianness"/> of <paramref name="stream"/>.</param>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/>is <c>null</c>.</exception>
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="ObjectDisposedException"/>
    public static void Write(this Stream stream, short value, Endianness endianness = Endianness.Native)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        Span<byte> buffer = BitConverter.GetBytes(value);
        Reorder(buffer, endianness);

        stream.Write(buffer);
    }

    /// <summary>
    /// Writes to the provided <see cref="Stream"/> a <see cref="string"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="string"/> value to write.</param>
    /// <param name="encoding">The <see cref="Encoding"/> to use. Default is <see cref="Encoding.UTF8"/></param>
    /// <param name="isNullTerminated">Tells that a 0 (NULL) character byte should be written after <paramref name="value"/>.</param>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/>is <c>null</c>.</exception>
    /// <exception cref="EncoderFallbackException"/>
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="ObjectDisposedException"/>
    public static void Write(this Stream stream, string? value, Encoding? encoding = null, bool isNullTerminated = false)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        if (!string.IsNullOrEmpty(value))
        {
            ReadOnlySpan<byte> buffer = (encoding ?? Encoding.UTF8).GetBytes(value);
            stream.Write(buffer);
        }

        if (isNullTerminated)
        {
            stream.WriteByte(0);
        }
    }

    /// <summary>
    /// Writes to the provided <see cref="Stream"/> an <see cref="uint"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="uint"/> value to write.</param>
    /// <param name="endianness">The <see cref="IO.Endianness"/> of <paramref name="stream"/>.</param>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/>is <c>null</c>.</exception>
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="ObjectDisposedException"/>
    public static void Write(this Stream stream, uint value, Endianness endianness = Endianness.Native)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        Span<byte> buffer = BitConverter.GetBytes(value);
        Reorder(buffer, endianness);

        stream.Write(buffer);
    }

    /// <summary>
    /// Writes to the provided <see cref="Stream"/> an <see cref="ulong"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="ulong"/> value to write.</param>
    /// <param name="endianness">The <see cref="IO.Endianness"/> of <paramref name="stream"/>.</param>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/>is <c>null</c>.</exception>
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="ObjectDisposedException"/>
    public static void Write(this Stream stream, ulong value, Endianness endianness = Endianness.Native)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        Span<byte> buffer = BitConverter.GetBytes(value);
        Reorder(buffer, endianness);

        stream.Write(buffer);
    }

    /// <summary>
    /// Writes to the provided <see cref="Stream"/> an <see cref="ushort"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="ushort"/> value to write.</param>
    /// <param name="endianness">The <see cref="IO.Endianness"/> of <paramref name="stream"/>.</param>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/>is <c>null</c>.</exception>
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="ObjectDisposedException"/>
    public static void Write(this Stream stream, ushort value, Endianness endianness = Endianness.Native)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        Span<byte> buffer = BitConverter.GetBytes(value);
        Reorder(buffer, endianness);

        stream.Write(buffer);
    }
}
