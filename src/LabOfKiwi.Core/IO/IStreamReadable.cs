using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LabOfKiwi.IO
{
    /// <summary>
    /// Represents an object that can be populated by reading a <see cref="Stream"/>.
    /// </summary>
    public interface IStreamReadable
    {
        /// <summary>
        /// Reads the provided <see cref="Stream"/> and populates this instance with the data read.
        /// </summary>
        /// 
        /// <param name="stream">The <see cref="Stream"/> to read.</param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="IOException">An error occurs when reading <paramref name="stream"/>.</exception>
        /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
        void Read(Stream stream);

        /// <summary>
        /// Asynchrously reads the provided <see cref="Stream"/> and populates this instance with the data read.
        /// </summary>
        /// 
        /// <param name="stream">The <see cref="Stream"/> to read.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="IOException">An error occurs when reading <paramref name="stream"/>.</exception>
        /// <exception cref="EndOfStreamException">The end of <paramref name="stream"/> has been reached.</exception>
        /// <exception cref="StreamReadTaskCanceledException">Task is cancelled before all bytes are read.</exception>
        Task ReadAsync(Stream stream, CancellationToken cancellationToken = default);
    }
}
