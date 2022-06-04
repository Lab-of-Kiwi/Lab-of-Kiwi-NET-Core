namespace LabOfKiwi.IO;

/// <summary>
/// Determines the endianness of data.
/// </summary>
public enum Endianness : byte
{
    /// <summary>
    /// Use the native endianness used by this computer architecture (e.g., Intel should be little-endian).
    /// </summary>
    Native = 0,

    /// <summary>
    /// Use little-endian (least significant bytes first).
    /// </summary>
    LittleEndian = 1,

    /// <summary>
    /// Use big-endian (most significant bytes first).
    /// </summary>
    BigEndian = 2
}
