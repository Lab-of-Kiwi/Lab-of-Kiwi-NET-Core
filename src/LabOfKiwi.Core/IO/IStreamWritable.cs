using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LabOfKiwi.IO;

/// <summary>
/// Represents an object that can be written to a <see cref="Stream"/>.
/// </summary>
public interface IStreamWritable
{
    /// <summary>
    /// Writes the current instance to the provided <see cref="Stream"/>.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    /// <exception cref="IOException">An error occurs when writing to <paramref name="stream"/>.</exception>
    void Write(Stream stream);

    /// <summary>
    /// Asynchronously writes the current instance to the provided <see cref="Stream"/>.
    /// </summary>
    /// 
    /// <param name="stream">The <see cref="Stream"/> to write to.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    /// 
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
    /// <exception cref="IOException">An error occurs when writing to <paramref name="stream"/>.</exception>
    Task WriteAsync(Stream stream, CancellationToken cancellationToken = default);
}
