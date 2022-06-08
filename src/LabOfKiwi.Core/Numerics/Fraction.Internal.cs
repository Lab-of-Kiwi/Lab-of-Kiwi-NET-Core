using System.Numerics;

namespace LabOfKiwi.Numerics
{
    public readonly partial struct Fraction
    {
        // ToString constants.
        private const string NaNString = "NaN";
        private const string NegativeInfinityString = "-\u221E";
        private const string PositiveInfinityString = "\u221E";
        private const string ZeroString = "0";

        // Values for floating point and decimal casts.
        private static readonly string DoubleFixedPoint = "0." + new string('#', 339);
        private static readonly BigInteger[] PowersOf10;

        // Debug method for asserting that a numerator and denominator are reduced.
        private static bool IsReduced(BigInteger num, BigInteger den)
        {
            BigInteger n = num;
            BigInteger d = den;
            Reduce(ref n, ref d);
            return num == n && den == d;
        }

        // Parses the provided string into a Fraction.
        private static Fraction ParseDecimalString(string str)
        {
            if (str.Contains('.'))
            {
                string[] split = str.Split('.');
                Fraction whole = BigInteger.Parse(split[0]);

                split[1] = split[1].TrimEnd('0');
                BigInteger n = BigInteger.Parse(split[1].TrimStart('0'));
                BigInteger d = PowersOf10[split[1].Length];
                Fraction frac = Create(n, d);

                if (str[0] == '-')
                {
                    return whole - frac;
                }

                return whole + frac;
            }

            return new Fraction(BigInteger.Parse(str), BigInteger.One);
        }

        // Reduces the provided numerator and denominator.
        private static void Reduce(ref BigInteger num, ref BigInteger den)
        {
            // Non-finite values
            if (den == BigInteger.Zero)
            {
                // Positive infinity should be 1 / 0
                if (num > BigInteger.One)
                {
                    num = BigInteger.One;
                }

                // Negative infinity should be -1 / 0
                else if (num < BigInteger.MinusOne)
                {
                    num = BigInteger.MinusOne;
                }

                // NaN is 0 / 0
                return;
            }

            // Zero should be 0 / 1
            if (num == BigInteger.Zero)
            {
                den = BigInteger.One;
                return;
            }

            // Sign should be in numerator
            if (den < BigInteger.Zero)
            {
                num = BigInteger.Negate(num);
                den = BigInteger.Negate(den);
            }

            // Remove negative temporarily for GCD
            bool isNegative;

            if (num < BigInteger.Zero)
            {
                isNegative = true;
                num = BigInteger.Negate(num);
            }
            else
            {
                isNegative = false;
            }

            // Reduce numerator and denominator by GCD
            BigInteger gcd = BigInteger.GreatestCommonDivisor(num, den);
            num /= gcd;
            den /= gcd;

            // Re-apply negative if needed
            if (isNegative)
            {
                num = BigInteger.Negate(num);
            }
        }

        // Tries to deconstruct this value into a whole number and its remainder, with a flag determining it is negative.
        private bool TryDeconstruct(out BigInteger wholeNumber, out Fraction remainder, out bool isNegative)
        {
            if (_denominator == BigInteger.Zero)
            {
                wholeNumber = default;
                remainder = default;
                isNegative = default;
                return false;
            }

            BigInteger numerator;

            if (_numerator < BigInteger.Zero)
            {
                numerator = BigInteger.Negate(_numerator);
                isNegative = true;
            }
            else
            {
                numerator = _numerator;
                isNegative = false;
            }

            wholeNumber = numerator / _denominator;

            BigInteger remNum = numerator % _denominator;
            BigInteger remDen = _denominator;
            remainder = Create(remNum, remDen);
            return true;
        }
    }
}
