using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabOfKiwi.IO;

public static partial class StreamExtensions
{
    /// <summary>
    /// Asynchronously reads the next eight bytes in the provided <see cref="Stream"/> instance as a
    /// <see cref="double"/>, and monitors cancellation requests.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
    /// <param name="endianness">The expected endianness of <paramref name="stream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>
    ///     A task that represents the asynchronous read operation. The value of its <c>Result</c> property contains the
    ///     <see cref="double"/> value read.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
    /// <exception cref="StreamReadTaskCanceledException">Task is cancelled before all bytes are read.</exception>
    public static async Task<double> ReadDoubleAsync(this Stream stream, Endianness endianness = Endianness.Native,
        CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        byte[] buffer = new byte[sizeof(double)];
        await InternalReadFullyAsync(stream, buffer, cancellationToken);
        Reorder(buffer, endianness);

        return BitConverter.ToDouble(buffer);
    }

    /// <summary>
    /// Asynchronously reads fully a sequence of bytes from the provided stream, advances the position within the stream
    /// by the number of bytes read, and monitors cancellation requests.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
    /// <param name="buffer">
    ///     A region of memory whose values are replaced by the bytes read from <paramref name="stream"/>.
    /// </param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
    /// <exception cref="StreamReadTaskCanceledException">Task is cancelled before all bytes are read.</exception>
    public static async Task ReadFullyAsync(this Stream stream, Memory<byte> buffer,
        CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        await InternalReadFullyAsync(stream, buffer, cancellationToken);
    }

    /// <summary>
    /// Asynchronously reads fully a sequence of bytes from the provided stream, advances the position within the stream
    /// by the number of bytes read, and monitors cancellation requests.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
    /// <param name="buffer">The buffer to write the data into.</param>
    /// <param name="offset">
    ///     The byte offset in <paramref name="buffer"/> at which to begin writing data from the stream.
    /// </param>
    /// <param name="count">The number of bytes to read.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// 
    /// <exception cref="ArgumentNullException">
    ///     <paramref name="stream"/> or <paramref name="buffer"/> are <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="InvalidOperationException"/>
    /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
    /// <exception cref="StreamReadTaskCanceledException">Task is cancelled before all bytes are read.</exception>
    public static async Task ReadFullyAsync(this Stream stream, byte[] buffer, int offset, int count,
        CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        // Delegate argument checks...
        int bytesRead = await stream.ReadAsync(buffer, offset, count, cancellationToken);
        
        if (bytesRead != count)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new StreamReadTaskCanceledException(bytesRead, cancellationToken);
            }

            throw new EndOfStreamException();
        }
    }

    /// <summary>
    /// Asynchronously reads the next two bytes in the provided <see cref="Stream"/> instance as a <see cref="short"/>,
    /// and monitors cancellation requests.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
    /// <param name="endianness">The expected endianness of <paramref name="stream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>
    ///     A task that represents the asynchronous read operation. The value of its <c>Result</c> property contains the
    ///     <see cref="short"/> value read.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
    /// <exception cref="StreamReadTaskCanceledException">Task is cancelled before all bytes are read.</exception>
    public static async Task<short> ReadInt16Async(this Stream stream, Endianness endianness = Endianness.Native,
        CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        byte[] buffer = new byte[sizeof(short)];
        await InternalReadFullyAsync(stream, buffer, cancellationToken);
        Reorder(buffer, endianness);

        return BitConverter.ToInt16(buffer);
    }

    /// <summary>
    /// Asynchronously reads the next four bytes in the provided <see cref="Stream"/> instance as an <see cref="int"/>,
    /// and monitors cancellation requests.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
    /// <param name="endianness">The expected endianness of <paramref name="stream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>
    ///     A task that represents the asynchronous read operation. The value of its <c>Result</c> property contains the
    ///     <see cref="int"/> value read.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
    /// <exception cref="StreamReadTaskCanceledException">Task is cancelled before all bytes are read.</exception>
    public static async Task<int> ReadInt32Async(this Stream stream, Endianness endianness = Endianness.Native,
        CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        byte[] buffer = new byte[sizeof(int)];
        await InternalReadFullyAsync(stream, buffer, cancellationToken);
        Reorder(buffer, endianness);

        return BitConverter.ToInt32(buffer);
    }

    /// <summary>
    /// Asynchronously reads the next eight bytes in the provided <see cref="Stream"/> instance as a <see cref="long"/>,
    /// and monitors cancellation requests.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
    /// <param name="endianness">The expected endianness of <paramref name="stream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>
    ///     A task that represents the asynchronous read operation. The value of its <c>Result</c> property contains the
    ///     <see cref="long"/> value read.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
    /// <exception cref="StreamReadTaskCanceledException">Task is cancelled before all bytes are read.</exception>
    public static async Task<long> ReadInt64Async(this Stream stream, Endianness endianness = Endianness.Native,
        CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        byte[] buffer = new byte[sizeof(long)];
        await InternalReadFullyAsync(stream, buffer, cancellationToken);
        Reorder(buffer, endianness);

        return BitConverter.ToInt64(buffer);
    }

    /// <summary>
    /// Asynchronously reads the next four bytes in the provided <see cref="Stream"/> instance as a <see cref="float"/>,
    /// and monitors cancellation requests.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
    /// <param name="endianness">The expected endianness of <paramref name="stream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>
    ///     A task that represents the asynchronous read operation. The value of its <c>Result</c> property contains the
    ///     <see cref="float"/> value read.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
    /// <exception cref="StreamReadTaskCanceledException">Task is cancelled before all bytes are read.</exception>
    public static async Task<float> ReadSingleAsync(this Stream stream, Endianness endianness = Endianness.Native,
        CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        byte[] buffer = new byte[sizeof(float)];
        await InternalReadFullyAsync(stream, buffer, cancellationToken);
        Reorder(buffer, endianness);

        return BitConverter.ToSingle(buffer);
    }

    /// <summary>
    /// Asynchronously reads the bytes in the provided <see cref="Stream"/> instance until a 0 byte is found
    /// (NULL character), then takes the bytes read (excluding the NULL terminator) and returns a <see cref="string"/>
    /// using the provided <see cref="Encoding"/>.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
    /// <param name="encoding">
    ///     The <see cref="Encoding"/> to use for creating the <see cref="string"/>; if <c>null</c> is provided,
    ///     <see cref="Encoding.UTF8"/> is used.
    /// </param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>
    ///     A task that represents the asynchronous read operation. The value of its <c>Result</c> property contains the
    ///     <see cref="string"/> value read.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="StreamReadTaskCanceledException">Task is cancelled before all bytes are read.</exception>
    /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="DecoderFallbackException"/>
    public static Task<string> ReadStringAsync(this Stream stream, Encoding? encoding = null,
        CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        return Task.Run(() =>
        {
            List<byte> bytes = new List<byte>();

            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new StreamReadTaskCanceledException(bytes.Count, cancellationToken);
                }

                int i = stream.ReadByte();

                if (i == -1)
                {
                    throw new EndOfStreamException();
                }
                else if (i == 0)
                {
                    break;
                }
                else
                {
                    bytes.Add(unchecked((byte)i));
                }
            }

            return (encoding ?? Encoding.UTF8).GetString(bytes.ToArray());
        });
    }

    /// <summary>
    /// Asynchronously reads the next two bytes in the provided <see cref="Stream"/> instance as a <see cref="ushort"/>,
    /// and monitors cancellation requests.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
    /// <param name="endianness">The expected endianness of <paramref name="stream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>
    ///     A task that represents the asynchronous read operation. The value of its <c>Result</c> property contains the
    ///     <see cref="ushort"/> value read.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
    /// <exception cref="StreamReadTaskCanceledException">Task is cancelled before all bytes are read.</exception>
    public static async Task<ushort> ReadUInt16Async(this Stream stream, Endianness endianness = Endianness.Native,
        CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        byte[] buffer = new byte[sizeof(ushort)];
        await InternalReadFullyAsync(stream, buffer, cancellationToken);
        Reorder(buffer, endianness);

        return BitConverter.ToUInt16(buffer);
    }

    /// <summary>
    /// Asynchronously reads the next four bytes in the provided <see cref="Stream"/> instance as an <see cref="uint"/>,
    /// and monitors cancellation requests.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
    /// <param name="endianness">The expected endianness of <paramref name="stream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>
    ///     A task that represents the asynchronous read operation. The value of its <c>Result</c> property contains the
    ///     <see cref="uint"/> value read.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
    /// <exception cref="StreamReadTaskCanceledException">Task is cancelled before all bytes are read.</exception>
    public static async Task<uint> ReadUInt32Async(this Stream stream, Endianness endianness = Endianness.Native,
        CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        byte[] buffer = new byte[sizeof(uint)];
        await InternalReadFullyAsync(stream, buffer, cancellationToken);
        Reorder(buffer, endianness);

        return BitConverter.ToUInt32(buffer);
    }

    /// <summary>
    /// Asynchronously reads the next eight bytes in the provided <see cref="Stream"/> instance as an
    /// <see cref="ulong"/>, and monitors cancellation requests.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
    /// <param name="endianness">The expected endianness of <paramref name="stream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>
    ///     A task that represents the asynchronous read operation. The value of its <c>Result</c> property contains the
    ///     <see cref="ulong"/> value read.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
    /// <exception cref="StreamReadTaskCanceledException">Task is cancelled before all bytes are read.</exception>
    public static async Task<ulong> ReadUInt64Async(this Stream stream, Endianness endianness = Endianness.Native,
        CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        byte[] buffer = new byte[sizeof(ulong)];
        await InternalReadFullyAsync(stream, buffer, cancellationToken);
        Reorder(buffer, endianness);

        return BitConverter.ToUInt64(buffer);
    }
}
