using System;

namespace LabOfKiwi.Web.Html;

[Flags]
public enum DocumentPosition : ushort
{
    None = 0x00,
    Disconnected = 0x01,
    Preceding = 0x02,
    Following = 0x04,
    Contains = 0x08,
    ContainedBy = 0x10,
    ImplementationSpecific = 0x20,
}
