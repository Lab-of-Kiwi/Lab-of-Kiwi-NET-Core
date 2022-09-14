using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabOfKiwi.Numerics;

internal static class FractionUtility
{
    public static (decimal Numerator, decimal Denominator) ConvertToFraction(decimal value)
    {
        decimal i = decimal.Truncate(value);
        decimal d = value - i;
        decimal exp = 1;

        while (d % 1 != 0)
        {
            exp *= 10;
            d *= 10;
        }

        return (value * exp, exp);
    }

    public static (decimal Numerator, decimal Denominator) ConvertToFraction(decimal value, long min, long max)
    {
        decimal i = decimal.Truncate(value);
        decimal d = value - i;
        decimal exp = 1;

        while (d % 1 != 0)
        {
            if ((exp * 10) > max)
            {
                break;
            }

            decimal check = value * exp * 10;

            if (check < min || check > max)
            {
                break;
            }

            exp *= 10;
            d *= 10;
        }

        return (value * exp, exp);
    }

    public static (double Numerator, double Denominator) ConvertToFraction(double value)
    {
        double i = Math.Truncate(value);
        double d = value - i;
        double exp = 1;

        while (d % 1 != 0)
        {
            exp *= 10;
            d *= 10;
        }

        return (value * exp, exp);
    }

    public static (double Numerator, double Denominator) ConvertToFraction(double value, long min, long max)
    {
        double i = Math.Truncate(value);
        double d = value - i;
        double exp = 1;

        while (d % 1 != 0)
        {
            if ((exp * 10) > max)
            {
                break;
            }

            double check = value * exp * 10;

            if (check < min || check > max)
            {
                break;
            }

            exp *= 10;
            d *= 10;
        }

        return (value * exp, exp);
    }
}
