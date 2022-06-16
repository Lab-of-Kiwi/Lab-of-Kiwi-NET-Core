using System;

namespace LabOfKiwi.Cmd;

internal readonly struct SizeOption
{
    private readonly short _value;

    public SizeOption()
    {
        _value = short.MinValue;
    }

    private SizeOption(short value)
    {
        _value = value;
    }

    public bool IsPercent => short.MinValue < _value && _value < 0;

    public int Value => Math.Abs((int)_value);

    public bool IsAuto => _value == short.MinValue;

    public double AsPercent => Math.Abs((int)_value) / 100.0;

    public static bool TryParse(string? value, out SizeOption result)
    {
        if (string.IsNullOrEmpty(value))
        {
            result = default;
            return false;
        }

        if (value == "auto")
        {
            result = default;
            return true;
        }

        bool isPercent = value[^1] == '%';
        string numStr;

        if (isPercent)
        {
            if (value.Length == 1)
            {
                result = default;
                return false;
            }

            numStr = value[0..^1];
        }
        else
        {
            numStr = value;
        }

        if (!short.TryParse(numStr, out short iValue))
        {
            result = default;
            return false;
        }

        if (iValue == 0)
        {
            result = new SizeOption(0);
            return true;
        }

        if (iValue < 1)
        {
            result = default;
            return false;
        }

        if (isPercent)
        {
            iValue *= -1;
        }

        result = new SizeOption(iValue);
        return true;
    }
}
