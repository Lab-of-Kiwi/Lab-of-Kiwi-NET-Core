using System;
using System.Diagnostics;
using System.Numerics;

using MathD = System.Math;

namespace LabOfKiwi.Numerics
{
    public readonly partial struct Fraction
    {
        public static class Math
        {
            public static readonly Fraction PI = MathD.PI;
            public static readonly Fraction E = MathD.E;

            public static Fraction Abs(Fraction f)
            {
                return new Fraction(BigInteger.Abs(f._numerator), f._denominator);
            }

            public static Fraction Acos(Fraction f)
            {
                return MathD.Acos((double)f);
            }

            public static Fraction Acosh(Fraction f)
            {
                return MathD.Acosh((double)f);
            }

            public static Fraction Asin(Fraction f)
            {
                return MathD.Asin((double)f);
            }

            public static Fraction Asinh(Fraction f)
            {
                return MathD.Asinh((double)f);
            }

            public static Fraction Atan(Fraction f)
            {
                return MathD.Atan((double)f);
            }

            public static Fraction Atan2(Fraction y, Fraction x)
            {
                return MathD.Atan2((double)y, (double)x);
            }

            public static Fraction Atanh(Fraction f)
            {
                return MathD.Atanh((double)f);
            }

            public static Fraction Cbrt(Fraction f)
            {
                return MathD.Cbrt((double)f);
            }

            public static Fraction Cos(Fraction f)
            {
                return MathD.Cos((double)f);
            }

            public static Fraction Cosh(Fraction f)
            {
                return MathD.Cosh((double)f);
            }

            public static BigInteger DivRem(Fraction value, out BigInteger remainder)
            {
                if (TryDivRem(value, out BigInteger q, out BigInteger r))
                {
                    remainder = r;
                    return q;
                }

                throw new ArithmeticException("Cannot cast NaN or infinite fraction values to integer types.");
            }

            public static Fraction Exp(Fraction f)
            {
                return MathD.Exp((double)f);
            }

            public static int ILogB(Fraction f)
            {
                return MathD.ILogB((double)f);
            }

            public static Fraction Invert(Fraction f)
            {
                return Create(f._denominator, f._numerator);
            }

            public static Fraction Log(Fraction f)
            {
                return MathD.Log((double)f);
            }

            public static Fraction Log(Fraction f, Fraction newBase)
            {
                return MathD.Log((double)f, (double)newBase);
            }

            public static Fraction Log10(Fraction f)
            {
                return MathD.Log10((double)f);
            }

            public static Fraction Max(Fraction val1, Fraction val2)
            {
                return val2 > val1 ? val2 : val1;
            }

            public static Fraction Min(Fraction val1, Fraction val2)
            {
                return val2 < val1 ? val2 : val1;
            }

            public static Fraction Pow(Fraction x, Fraction y)
            {
                return MathD.Pow((double)x, (double)y);
            }

            public static Fraction Pow(Fraction x, int y)
            {
                // To the 0th power
                if (y == 0)
                {
                    // NaN and Zero to 0th power is NaN
                    if (x._numerator == BigInteger.Zero)
                    {
                        return NaN;
                    }

                    // All else is one, including infinities
                    return One;
                }

                // If not finite
                if (x._denominator == BigInteger.Zero)
                {
                    // NaN is always NaN
                    if (x._numerator == BigInteger.Zero)
                    {
                        return NaN;
                    }

                    // Infinities flipped are 0
                    if (y < 0)
                    {
                        return Zero;
                    }

                    // Positive infinity or negative infinity with even exponent.
                    if (y % 2 == 0 || x._numerator == BigInteger.One)
                    {
                        return PositiveInfinity;
                    }

                    // Negative infinity with odd exponent.
                    return NegativeInfinity;
                }

                // Need to calculate inverse
                if (y < 0)
                {
                    // Cannot negate int.MinValue, so special case
                    if (y == int.MinValue)
                    {
                        y = int.MaxValue;
                        return Create(BigInteger.Pow(x._denominator, y) * x._denominator, BigInteger.Pow(x._numerator, y) * x._numerator);
                    }

                    y = -y;
                    return Create(BigInteger.Pow(x._denominator, y), BigInteger.Pow(x._numerator, y));
                }

                return Create(BigInteger.Pow(x._numerator, y), BigInteger.Pow(x._denominator, y));
            }

            public static Fraction Sin(Fraction f)
            {
                ReduceAngle(ref f);
                return MathD.Sin((double)f);
            }

            public static Fraction Sinh(Fraction f)
            {
                return MathD.Sinh((double)f);
            }

            public static Fraction Sqrt(Fraction f)
            {
                return MathD.Sqrt((double)f);
            }

            public static Fraction Tan(Fraction f)
            {
                return MathD.Tan((double)f);
            }

            public static Fraction Tanh(Fraction f)
            {
                return MathD.Tanh((double)f);
            }

            public static bool TryDivRem(Fraction value, out BigInteger quotient, out BigInteger remainder)
            {
                if (value._denominator != BigInteger.Zero)
                {
                    quotient = BigInteger.DivRem(value._numerator, value._denominator, out remainder);
                    return true;
                }

                quotient = default;
                remainder = default;
                return false;
            }

            private static void ReduceAngle(ref Fraction f)
            {
                Debug.Assert(f.IsFinite);

                Fraction pi2 = 2 * PI;
                Fraction minusPi = -PI;

                while (f >= PI)
                {
                    f -= pi2;
                }

                while (f < minusPi)
                {
                    f += pi2;
                }
            }
        }
    }
}
