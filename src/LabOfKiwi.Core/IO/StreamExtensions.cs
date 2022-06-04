using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LabOfKiwi.IO;

/// <summary>
/// Provides various extension methods for <see cref="Stream"/> instances.
/// </summary>
public static partial class StreamExtensions
{
    // The endianness of this system's architecture.
    private static readonly Endianness Endianness = BitConverter.IsLittleEndian ? Endianness.LittleEndian : Endianness.BigEndian;

    // Internal method for reading fully into the provided buffer.
    private static void InternalReadFully(Stream stream, Span<byte> buffer)
    {
        if (stream.Read(buffer) != buffer.Length)
        {
            throw new EndOfStreamException();
        }
    }

    // Internal async method for reading fully into the provided buffer.
    private static async Task InternalReadFullyAsync(Stream stream, Memory<byte> buffer, CancellationToken cancellationToken)
    {
        int bytesRead = await stream.ReadAsync(buffer, cancellationToken);

        if (bytesRead != buffer.Length)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new StreamReadTaskCanceledException(bytesRead, cancellationToken);
            }

            throw new EndOfStreamException();
        }
    }

    // Internal method for reordering the bytes in the provided buffer based on the target endianness provided.
    private static void Reorder(Span<byte> buffer, Endianness endianness)
    {
        if (endianness != Endianness.Native && Endianness != endianness)
        {
            buffer.Reverse();
        }
    }
}
