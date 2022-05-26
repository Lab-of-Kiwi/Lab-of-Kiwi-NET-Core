using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LabOfKiwi.IO
{
    public static partial class StreamExtensions
    {
        /// <summary>
        /// Reads the next byte in the provided <see cref="Stream"/> instance and returns a <see cref="bool"/> value
        /// based on if the read byte is non-zero.
        /// </summary>
        /// 
        /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
        /// <returns><c>true</c> if the read byte is not 0; otherwise, <c>false</c>.</returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="NotSupportedException"><paramref name="stream"/> does not support reading.</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after <paramref name="stream"/> was closed.</exception>
        /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
        public static bool ReadBoolean(this Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            int b = stream.ReadByte();

            if (b == -1)
            {
                throw new EndOfStreamException();
            }

            return unchecked((byte)b) != 0;
        }

        /// <summary>
        /// Reads the next eight bytes in the provided <see cref="Stream"/> instance and returns a <see cref="double"/>
        /// value based on those bytes.
        /// </summary>
        /// 
        /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
        /// <param name="endianness">The expected endianness of <paramref name="stream"/>.</param>
        /// <returns>A <see cref="double"/> value.</returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
        public static double ReadDouble(this Stream stream, Endianness endianness = Endianness.Native)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Span<byte> buffer = new byte[sizeof(double)];
            InternalReadFully(stream, buffer);
            Reorder(buffer, endianness);

            return BitConverter.ToDouble(buffer);
        }

        /// <summary>
        /// Reads fully a sequence of bytes from the provided stream and advances the position within the stream by the
        /// number of bytes read.
        /// </summary>
        /// 
        /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
        /// <param name="buffer">A region of memory whose values are replaced by the bytes read from <paramref name="stream"/>.</param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
        public static void ReadFully(this Stream stream, Span<byte> buffer)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            InternalReadFully(stream, buffer);
        }

        /// <summary>
        /// Reads fully a sequence of bytes from the provided stream and advances the position within the stream by the
        /// number of bytes read.
        /// </summary>
        /// 
        /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
        /// <param name="buffer">An array of bytes whose values between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) are replaced by the bytes read from <paramref name="stream"/>.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the data read from <paramref name="stream"/>.</param>
        /// <param name="count">The maximum number of bytes to be read from the <paramref name="stream"/>.</param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> or <paramref name="buffer"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The sum of <paramref name="offset"/> and <paramref name="count"/> is larger than the buffer length.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> or <paramref name="count"/> is negative.</exception>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="NotSupportedException"><paramref name="stream"/> does not support reading.</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after <paramref name="stream"/> was closed.</exception>
        /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
        public static void ReadFully(this Stream stream, byte[] buffer, int offset, int count)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            // Delegate argument checks...
            if (stream.Read(buffer, offset, count) != count)
            {
                throw new EndOfStreamException();
            }
        }

        /// <summary>
        /// Reads the next byte in the provided <see cref="Stream"/> instance and returns it as a <see cref="sbyte"/>.
        /// </summary>
        /// 
        /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
        /// <returns>A <see cref="sbyte"/> value.</returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="NotSupportedException"><paramref name="stream"/> does not support reading.</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after <paramref name="stream"/> was closed.</exception>
        /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
        public static sbyte ReadInt8(this Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            int b = stream.ReadByte();

            if (b == -1)
            {
                throw new EndOfStreamException();
            }

            return unchecked((sbyte)(byte)b);
        }

        /// <summary>
        /// Reads the next two bytes in the provided <see cref="Stream"/> instance and returns a <see cref="short"/>
        /// value based on those bytes.
        /// </summary>
        /// 
        /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
        /// <param name="endianness">The expected endianness of <paramref name="stream"/>.</param>
        /// <returns>A <see cref="short"/> value.</returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
        public static short ReadInt16(this Stream stream, Endianness endianness = Endianness.Native)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Span<byte> buffer = new byte[sizeof(short)];
            InternalReadFully(stream, buffer);
            Reorder(buffer, endianness);

            return BitConverter.ToInt16(buffer);
        }

        /// <summary>
        /// Reads the next four bytes in the provided <see cref="Stream"/> instance and returns an <see cref="int"/>
        /// value based on those bytes.
        /// </summary>
        /// 
        /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
        /// <param name="endianness">The expected endianness of <paramref name="stream"/>.</param>
        /// <returns>An <see cref="int"/> value.</returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
        public static int ReadInt32(this Stream stream, Endianness endianness = Endianness.Native)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Span<byte> buffer = new byte[sizeof(int)];
            InternalReadFully(stream, buffer);
            Reorder(buffer, endianness);

            return BitConverter.ToInt32(buffer);
        }

        /// <summary>
        /// Reads the next eight bytes in the provided <see cref="Stream"/> instance and returns a <see cref="long"/>
        /// value based on those bytes.
        /// </summary>
        /// 
        /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
        /// <param name="endianness">The expected endianness of <paramref name="stream"/>.</param>
        /// <returns>A <see cref="long"/> value.</returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
        public static long ReadInt64(this Stream stream, Endianness endianness = Endianness.Native)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Span<byte> buffer = new byte[sizeof(long)];
            InternalReadFully(stream, buffer);
            Reorder(buffer, endianness);

            return BitConverter.ToInt64(buffer);
        }

        /// <summary>
        /// Reads the next four bytes in the provided <see cref="Stream"/> instance and returns a <see cref="float"/>
        /// value based on those bytes.
        /// </summary>
        /// 
        /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
        /// <param name="endianness">The expected endianness of <paramref name="stream"/>.</param>
        /// <returns>A <see cref="float"/> value.</returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
        public static float ReadSingle(this Stream stream, Endianness endianness = Endianness.Native)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Span<byte> buffer = new byte[sizeof(float)];
            InternalReadFully(stream, buffer);
            Reorder(buffer, endianness);

            return BitConverter.ToSingle(buffer);
        }

        /// <summary>
        /// Reads the bytes in the provided <see cref="Stream"/> instance until a 0 byte is found (NULL character), then
        /// takes the bytes read (excluding the NULL terminator) and returns a <see cref="string"/> using the
        /// provided <see cref="Encoding"/>.
        /// </summary>
        /// 
        /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
        /// <param name="encoding">The <see cref="Encoding"/> to use for creating the <see cref="string"/>; if <c>null</c> is provided, <see cref="Encoding.UTF8"/> is used.</param>
        /// <returns>A <see cref="string"/> value.</returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="NotSupportedException"><paramref name="stream"/> does not support reading.</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after <paramref name="stream"/> was closed.</exception>
        /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
        /// <exception cref="IOException">Unable to decode bytes read as a <see cref="string"/> using <paramref name="encoding"/>.</exception>
        public static string ReadString(this Stream stream, Encoding? encoding = null)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            encoding ??= Encoding.UTF8;

            List<byte> bytes = new List<byte>();

            while (true)
            {
                int i = stream.ReadByte();

                if (i == -1)
                {
                    throw new EndOfStreamException();
                }
                else if (i == 0)
                {
                    // NULL character found, so end of string
                    break;
                }
                else
                {
                    bytes.Add(unchecked((byte)i));
                }
            }

            try
            {
                return encoding.GetString(bytes.ToArray());
            }
            catch (Exception e)
            {
                throw new IOException("Unable to decode stream bytes as a string using " + encoding + ".", e);
            }
        }

        /// <summary>
        /// Reads the next byte in the provided <see cref="Stream"/> instance and returns it.
        /// </summary>
        /// 
        /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
        /// <returns>A <see cref="byte"/> value.</returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="NotSupportedException"><paramref name="stream"/> does not support reading.</exception>
        /// <exception cref="ObjectDisposedException">Methods were called after <paramref name="stream"/> was closed.</exception>
        /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
        public static byte ReadUInt8(this Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            int b = stream.ReadByte();

            if (b == -1)
            {
                throw new EndOfStreamException();
            }

            return unchecked((byte)b);
        }

        /// <summary>
        /// Reads the next two bytes in the provided <see cref="Stream"/> instance and returns an <see cref="ushort"/>
        /// value based on those bytes.
        /// </summary>
        /// 
        /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
        /// <param name="endianness">The expected endianness of <paramref name="stream"/>.</param>
        /// <returns>An <see cref="ushort"/> value.</returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
        public static ushort ReadUInt16(this Stream stream, Endianness endianness = Endianness.Native)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Span<byte> buffer = new byte[sizeof(ushort)];
            InternalReadFully(stream, buffer);
            Reorder(buffer, endianness);

            return BitConverter.ToUInt16(buffer);
        }

        /// <summary>
        /// Reads the next four bytes in the provided <see cref="Stream"/> instance and returns an <see cref="uint"/>
        /// value based on those bytes.
        /// </summary>
        /// 
        /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
        /// <param name="endianness">The expected endianness of <paramref name="stream"/>.</param>
        /// <returns>An <see cref="uint"/> value.</returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
        public static uint ReadUInt32(this Stream stream, Endianness endianness = Endianness.Native)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Span<byte> buffer = new byte[sizeof(uint)];
            InternalReadFully(stream, buffer);
            Reorder(buffer, endianness);

            return BitConverter.ToUInt32(buffer);
        }

        /// <summary>
        /// Reads the next eight bytes in the provided <see cref="Stream"/> instance and returns an <see cref="ulong"/>
        /// value based on those bytes.
        /// </summary>
        /// 
        /// <param name="stream">The <see cref="Stream"/> instance to read.</param>
        /// <param name="endianness">The expected endianness of <paramref name="stream"/>.</param>
        /// <returns>An <see cref="ulong"/> value.</returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
        public static ulong ReadUInt64(this Stream stream, Endianness endianness = Endianness.Native)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Span<byte> buffer = new byte[sizeof(ulong)];
            InternalReadFully(stream, buffer);
            Reorder(buffer, endianness);

            return BitConverter.ToUInt64(buffer);
        }
    }
}
