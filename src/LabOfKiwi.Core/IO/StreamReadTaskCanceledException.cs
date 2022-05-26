using System.Threading;
using System.Threading.Tasks;

namespace LabOfKiwi.IO
{
    /// <summary>
    /// Thrown when a stream read is cancelled.
    /// </summary>
    public class StreamReadTaskCanceledException : TaskCanceledException
    {
        /// <summary>
        /// Constructs a new <see cref="StreamReadTaskCanceledException"/> instace with
        /// the provided number of bytes read and the cancellation token that caused the exception.
        /// </summary>
        /// <param name="bytesRead">The number of bytes read before being cancelled.</param>
        /// <param name="token">The token that cancelled the task.</param>

        public StreamReadTaskCanceledException(int bytesRead, CancellationToken token)
            : base("Stream read task cancelled.", null, token)
        {
            BytesRead = bytesRead;
        }

        /// <summary>
        /// The number of bytes read.
        /// </summary>
        public int BytesRead { get; }

        public override string Message
            => base.Message + " Bytes read: " + BytesRead;
    }
}
