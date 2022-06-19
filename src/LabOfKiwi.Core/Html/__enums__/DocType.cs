using System;
using System.Text;

namespace LabOfKiwi.Html;

public enum DocType : byte
{
    Html5 = 0
}

internal static class DocTypeExtensions
{
    public static string ToTagString(this DocType value)
    {
        StringBuilder sb = new();
        sb.Append("<!DOCTYPE ");

        sb.Append(value switch
        {
            DocType.Html5 => "html",
            _ => throw new NotImplementedException()
        });

        sb.Append('>');

        return sb.ToString();
    }
}
