using LabOfKiwi.Collections;
using System;
using System.Collections.Generic;

namespace LabOfKiwi.Web.Html;

[Flags]
public enum SandboxOption : ushort
{
    None = 0x0000,
    AllowDownloads = 0x0001,
    AllowForms = 0x0002,
    AllowModals = 0x0004,
    AllowOrientationLock = 0x0008,
    AllowPointerLock = 0x0010,
    AllowPopups = 0x0020,
    AllowPopupsToEscapeSandbox = 0x0040,
    AllowPresentation = 0x0080,
    AllowSameOrigin = 0x0100,
    AllowScripts = 0x0200,
    AllowTopNavigation = 0x0400,
    AllowTopNavigationByUserActivation = 0x0800,
    AllowTopNavigationToCustomProtocols = 0x1000
}

internal static class SandboxOptionUtility
{
    private static readonly Map<SandboxOption, string> _optionMap = new();

    static SandboxOptionUtility()
    {
        _optionMap.Add(SandboxOption.AllowDownloads, "allow-downloads");
        _optionMap.Add(SandboxOption.AllowForms, "allow-forms");
        _optionMap.Add(SandboxOption.AllowModals, "allow-modals");
        _optionMap.Add(SandboxOption.AllowOrientationLock, "allow-orientation-lock");
        _optionMap.Add(SandboxOption.AllowPointerLock, "allow-pointer-lock");
        _optionMap.Add(SandboxOption.AllowPopups, "allow-popups");
        _optionMap.Add(SandboxOption.AllowPopupsToEscapeSandbox, "allow-popups-to-escape-sandbox");
        _optionMap.Add(SandboxOption.AllowPresentation, "allow-presentation");
        _optionMap.Add(SandboxOption.AllowSameOrigin, "allow-same-origin");
        _optionMap.Add(SandboxOption.AllowScripts, "allow-scripts");
        _optionMap.Add(SandboxOption.AllowTopNavigation, "allow-top-navigation");
        _optionMap.Add(SandboxOption.AllowTopNavigationByUserActivation, "allow-top-navigation-by-user-activation");
        _optionMap.Add(SandboxOption.AllowTopNavigationToCustomProtocols, "allow-top-navigation-to-custom-protocols");
    }

    public static string? ToHTMLString(SandboxOption value, string delimiter)
    {
        if (value == SandboxOption.None)
        {
            return null;
        }

        List<string> stringValues = new();

        for (int i = 0; i < 13; i++)
        {
            SandboxOption optionToAdd = (SandboxOption)(ushort)(1U << i);

            if (value.HasFlag(optionToAdd))
            {
                stringValues.Add(_optionMap[optionToAdd]);
            }
        }

        return string.Join(delimiter, stringValues);
    }

    public static bool TryParse(string? stringValue, string delimiter, out SandboxOption result)
    {
        if (string.IsNullOrEmpty(stringValue))
        {
            result = SandboxOption.None;
            return true;
        }

        result = SandboxOption.None;

        string[] rawValues = stringValue.Split(delimiter, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        foreach (string rawValue in rawValues)
        {
            if (_optionMap.TryGetValue(rawValue, out SandboxOption value))
            {
                result |= value;
            }
            else
            {
                result = default;
                return false;
            }
        }

        return true;
    }
}
