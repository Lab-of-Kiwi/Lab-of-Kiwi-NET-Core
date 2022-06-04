using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabOfKiwi.IO;

public static partial class StreamExtensions
{
    /// <summary>
    /// Asynchrously writes to the provided <see cref="Stream"/> a <see cref="double"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="double"/> value to write.</param>
    /// <param name="endianness">The <see cref="IO.Endianness"/> of <paramref name="stream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    public static ValueTask WriteAsync(this Stream stream, double value, Endianness endianness = Endianness.Native, CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        byte[] buffer = BitConverter.GetBytes(value);
        Reorder(buffer, endianness);

        return stream.WriteAsync(buffer, cancellationToken);
    }

    /// <summary>
    /// Asynchrously writes to the provided <see cref="Stream"/> a <see cref="float"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="float"/> value to write.</param>
    /// <param name="endianness">The <see cref="IO.Endianness"/> of <paramref name="stream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    public static ValueTask WriteAsync(this Stream stream, float value, Endianness endianness = Endianness.Native, CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        byte[] buffer = BitConverter.GetBytes(value);
        Reorder(buffer, endianness);

        return stream.WriteAsync(buffer, cancellationToken);
    }

    /// <summary>
    /// Asynchrously writes to the provided <see cref="Stream"/> an <see cref="int"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="int"/> value to write.</param>
    /// <param name="endianness">The <see cref="IO.Endianness"/> of <paramref name="stream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    public static ValueTask WriteAsync(this Stream stream, int value, Endianness endianness = Endianness.Native, CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        byte[] buffer = BitConverter.GetBytes(value);
        Reorder(buffer, endianness);

        return stream.WriteAsync(buffer, cancellationToken);
    }

    /// <summary>
    /// Asynchrously writes to the provided <see cref="Stream"/> a <see cref="long"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="long"/> value to write.</param>
    /// <param name="endianness">The <see cref="IO.Endianness"/> of <paramref name="stream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    public static ValueTask WriteAsync(this Stream stream, long value, Endianness endianness = Endianness.Native, CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        byte[] buffer = BitConverter.GetBytes(value);
        Reorder(buffer, endianness);

        return stream.WriteAsync(buffer, cancellationToken);
    }

    /// <summary>
    /// Asynchrously writes to the provided <see cref="Stream"/> a <see cref="short"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="short"/> value to write.</param>
    /// <param name="endianness">The <see cref="IO.Endianness"/> of <paramref name="stream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    public static ValueTask WriteAsync(this Stream stream, short value, Endianness endianness = Endianness.Native, CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        byte[] buffer = BitConverter.GetBytes(value);
        Reorder(buffer, endianness);

        return stream.WriteAsync(buffer, cancellationToken);
    }

    /// <summary>
    /// Asynchronously writes to the provided <see cref="Stream"/> a <see cref="string"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="string"/> value to write.</param>
    /// <param name="encoding">The <see cref="Encoding"/> to use. Default is <see cref="Encoding.UTF8"/></param>
    /// <param name="isNullTerminated">Tells that a 0 (NULL) character byte should be written after <paramref name="value"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/>is <c>null</c>.</exception>
    /// <exception cref="EncoderFallbackException"/>
    /// <exception cref="IOException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="ObjectDisposedException"/>
    public static async ValueTask WriteAsync(this Stream stream, string? value, Encoding? encoding = null, bool isNullTerminated = false, CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        if (!string.IsNullOrEmpty(value))
        {
            byte[] buffer = (encoding ?? Encoding.UTF8).GetBytes(value);
            await stream.WriteAsync(buffer, cancellationToken);
        }

        if (isNullTerminated && !cancellationToken.IsCancellationRequested)
        {
            stream.WriteByte(0);
        }
    }

    /// <summary>
    /// Asynchrously writes to the provided <see cref="Stream"/> an <see cref="uint"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="uint"/> value to write.</param>
    /// <param name="endianness">The <see cref="IO.Endianness"/> of <paramref name="stream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    public static ValueTask WriteAsync(this Stream stream, uint value, Endianness endianness = Endianness.Native, CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        byte[] buffer = BitConverter.GetBytes(value);
        Reorder(buffer, endianness);

        return stream.WriteAsync(buffer, cancellationToken);
    }

    /// <summary>
    /// Asynchrously writes to the provided <see cref="Stream"/> an <see cref="ulong"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="ulong"/> value to write.</param>
    /// <param name="endianness">The <see cref="IO.Endianness"/> of <paramref name="stream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    public static ValueTask WriteAsync(this Stream stream, ulong value, Endianness endianness = Endianness.Native, CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        byte[] buffer = BitConverter.GetBytes(value);
        Reorder(buffer, endianness);

        return stream.WriteAsync(buffer, cancellationToken);
    }

    /// <summary>
    /// Asynchrously writes to the provided <see cref="Stream"/> an <see cref="ushort"/> value.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="value">The <see cref="ushort"/> value to write.</param>
    /// <param name="endianness">The <see cref="IO.Endianness"/> of <paramref name="stream"/>.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    public static ValueTask WriteAsync(this Stream stream, ushort value, Endianness endianness = Endianness.Native, CancellationToken cancellationToken = default)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        byte[] buffer = BitConverter.GetBytes(value);
        Reorder(buffer, endianness);

        return stream.WriteAsync(buffer, cancellationToken);
    }
}
