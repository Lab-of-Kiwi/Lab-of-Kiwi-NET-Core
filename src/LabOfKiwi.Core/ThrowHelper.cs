using System;
using System.Diagnostics;

namespace LabOfKiwi;

internal static class ThrowHelper
{
    public static int RangeCheck(string parameterName, int value, int minValue = int.MinValue, int maxValue = int.MaxValue)
    {
        Debug.Assert(minValue <= maxValue);

        if (value < minValue || value > maxValue)
        {
            if (minValue == maxValue)
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Value must be {minValue}.");
            }

            if (minValue == int.MinValue)
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Value must be less than {maxValue + 1}.");
            }
            
            if (maxValue == int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(parameterName, $"Value must be greater than {minValue - 1}.");
            }

            throw new ArgumentOutOfRangeException(parameterName, $"Value must be between {minValue} and {maxValue}, inclusive.");
        }

        return value;
    }

    public static T NullCheck<T>(string parameterName, T? argument) where T : class
    {
        if (argument == null)
        {
            throw new ArgumentNullException(parameterName);
        }

        return argument;
    }
}
