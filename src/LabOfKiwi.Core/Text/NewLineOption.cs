using System;

namespace LabOfKiwi.Text;

/// <summary>
/// Represent an option for NewLine parsing. Values can be combined.
/// </summary>
[Flags]
public enum NewlineOption : byte
{
    /// <summary>
    /// No newlines allowed.
    /// </summary>
    None = 0b00000000,

    /// <summary>
    /// Windows newlines only (CR LF).
    /// </summary>
    Windows = 0b00000001,

    /// <summary>
    /// Unix/Linux like system newlines only (LF).
    /// </summary>
    Linux = 0b00000010,

    /// <summary>
    /// Classic Mac OS and Apple newlines only (CR).
    /// </summary>
    Mac = 0b00000100,

    /// <summary>
    /// Any newlines allowed.
    /// </summary>
    Any = 0b00000111
}
