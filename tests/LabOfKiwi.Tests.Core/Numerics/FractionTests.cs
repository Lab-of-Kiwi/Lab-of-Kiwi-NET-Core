using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LabOfKiwi.Numerics
{
    public class FractionTests
    {
        [Fact]
        public void Test_ToString()
        {
            Assert.Equal("0", default(Fraction).ToString());
            Assert.Equal(double.NaN.ToString(), Fraction.NaN.ToString());
            Assert.Equal(double.PositiveInfinity.ToString(), Fraction.PositiveInfinity.ToString());
            Assert.Equal(double.NegativeInfinity.ToString(), Fraction.NegativeInfinity.ToString());

            Assert.Equal("0", Fraction.Zero.ToString());
            Assert.Equal("1", Fraction.One.ToString());
            Assert.Equal("-1", Fraction.MinusOne.ToString());

            var x = Fraction.One / 2;
            Assert.Equal("1 / 2", x.ToString());

            x = Fraction.MinusOne / 2;
            Assert.Equal("-1 / 2", x.ToString());
        }

        [Fact]
        public void Test_IsFinite()
        {
            Assert.True(default(Fraction).IsFinite);
            Assert.True(Fraction.Zero.IsFinite);
            Assert.True(Fraction.One.IsFinite);
            Assert.True(Fraction.MinusOne.IsFinite);
            Assert.False(Fraction.PositiveInfinity.IsFinite);
            Assert.False(Fraction.NegativeInfinity.IsFinite);
            Assert.False(Fraction.NaN.IsFinite);
        }

        [Fact]
        public void Test_IsInfinity()
        {
            Assert.False(default(Fraction).IsInfinity);
            Assert.False(Fraction.Zero.IsInfinity);
            Assert.False(Fraction.One.IsInfinity);
            Assert.False(Fraction.MinusOne.IsInfinity);
            Assert.True(Fraction.PositiveInfinity.IsInfinity);
            Assert.True(Fraction.NegativeInfinity.IsInfinity);
            Assert.False(Fraction.NaN.IsInfinity);
        }

        [Fact]
        public void Test_IsNaN()
        {
            Assert.False(default(Fraction).IsNaN);
            Assert.False(Fraction.Zero.IsNaN);
            Assert.False(Fraction.One.IsNaN);
            Assert.False(Fraction.MinusOne.IsNaN);
            Assert.False(Fraction.PositiveInfinity.IsNaN);
            Assert.False(Fraction.NegativeInfinity.IsNaN);
            Assert.True(Fraction.NaN.IsNaN);
        }

        [Fact]
        public void Test_IsNegative()
        {
            Assert.False(default(Fraction).IsNegative);
            Assert.False(Fraction.Zero.IsNegative);
            Assert.False(Fraction.One.IsNegative);
            Assert.True(Fraction.MinusOne.IsNegative);
            Assert.False(Fraction.PositiveInfinity.IsNegative);
            Assert.True(Fraction.NegativeInfinity.IsNegative);
            Assert.False(Fraction.NaN.IsNegative);
        }

        [Fact]
        public void Test_IsNegativeInfinity()
        {
            Assert.False(default(Fraction).IsNegativeInfinity);
            Assert.False(Fraction.Zero.IsNegativeInfinity);
            Assert.False(Fraction.One.IsNegativeInfinity);
            Assert.False(Fraction.MinusOne.IsNegativeInfinity);
            Assert.False(Fraction.PositiveInfinity.IsNegativeInfinity);
            Assert.True(Fraction.NegativeInfinity.IsNegativeInfinity);
            Assert.False(Fraction.NaN.IsNegativeInfinity);
        }

        [Fact]
        public void Test_IsPositiveInfinity()
        {
            Assert.False(default(Fraction).IsPositiveInfinity);
            Assert.False(Fraction.Zero.IsPositiveInfinity);
            Assert.False(Fraction.One.IsPositiveInfinity);
            Assert.False(Fraction.MinusOne.IsPositiveInfinity);
            Assert.True(Fraction.PositiveInfinity.IsPositiveInfinity);
            Assert.False(Fraction.NegativeInfinity.IsPositiveInfinity);
            Assert.False(Fraction.NaN.IsPositiveInfinity);
        }

        [Fact]
        public void Test_IsZero()
        {
            Assert.True(default(Fraction).IsZero);
            Assert.True(Fraction.Zero.IsZero);
            Assert.False(Fraction.One.IsZero);
            Assert.False(Fraction.MinusOne.IsZero);
            Assert.False(Fraction.PositiveInfinity.IsZero);
            Assert.False(Fraction.NegativeInfinity.IsZero);
            Assert.False(Fraction.NaN.IsZero);
        }

        [Fact]
        public void Test_DefaultValue()
        {
            Assert.True(default(Fraction).Equals(default(Fraction)));

            Assert.True(Fraction.Zero.Equals(default(Fraction)));
            Assert.True(default(Fraction).Equals(Fraction.Zero));

            Assert.True(Fraction.Zero == default(Fraction));
            Assert.True(default(Fraction) == Fraction.Zero);

            Assert.True(default(Fraction).GetHashCode() == default(Fraction).GetHashCode());
            Assert.True(Fraction.Zero.GetHashCode() == default(Fraction).GetHashCode());
        }

        [Fact]
        public void Test_Comparison_Finite()
        {
            Fraction x = Fraction.Zero;

            Assert.False(x == Fraction.One);
            Assert.True(x != Fraction.One);
            Assert.True(x < Fraction.One);
            Assert.True(x <= Fraction.One);
            Assert.False(x > Fraction.One);
            Assert.False(x >= Fraction.One);

            Assert.True(x == Fraction.Zero);
            Assert.False(x != Fraction.Zero);
            Assert.False(x < Fraction.Zero);
            Assert.True(x <= Fraction.Zero);
            Assert.False(x > Fraction.Zero);
            Assert.True(x >= Fraction.Zero);

            Assert.False(x == Fraction.MinusOne);
            Assert.True(x != Fraction.MinusOne);
            Assert.False(x < Fraction.MinusOne);
            Assert.False(x <= Fraction.MinusOne);
            Assert.True(x > Fraction.MinusOne);
            Assert.True(x >= Fraction.MinusOne);

            Assert.False(x == Fraction.PositiveInfinity);
            Assert.True(x != Fraction.PositiveInfinity);
            Assert.True(x < Fraction.PositiveInfinity);
            Assert.True(x <= Fraction.PositiveInfinity);
            Assert.False(x > Fraction.PositiveInfinity);
            Assert.False(x >= Fraction.PositiveInfinity);

            Assert.False(x == Fraction.NegativeInfinity);
            Assert.True(x != Fraction.NegativeInfinity);
            Assert.False(x < Fraction.NegativeInfinity);
            Assert.False(x <= Fraction.NegativeInfinity);
            Assert.True(x > Fraction.NegativeInfinity);
            Assert.True(x >= Fraction.NegativeInfinity);

            Assert.False(x == Fraction.NaN);
            Assert.True(x != Fraction.NaN);
            Assert.False(x < Fraction.NaN);
            Assert.False(x <= Fraction.NaN);
            Assert.True(x > Fraction.NaN);
            Assert.True(x >= Fraction.NaN);
        }

        [Fact]
        public void Test_Comparison_PositiveInfinity()
        {
            Fraction x = Fraction.PositiveInfinity;

            Assert.False(x == Fraction.One);
            Assert.True(x != Fraction.One);
            Assert.False(x < Fraction.One);
            Assert.False(x <= Fraction.One);
            Assert.True(x > Fraction.One);
            Assert.True(x >= Fraction.One);

            Assert.False(x == Fraction.Zero);
            Assert.True(x != Fraction.Zero);
            Assert.False(x < Fraction.Zero);
            Assert.False(x <= Fraction.Zero);
            Assert.True(x > Fraction.Zero);
            Assert.True(x >= Fraction.Zero);

            Assert.False(x == Fraction.MinusOne);
            Assert.True(x != Fraction.MinusOne);
            Assert.False(x < Fraction.MinusOne);
            Assert.False(x <= Fraction.MinusOne);
            Assert.True(x > Fraction.MinusOne);
            Assert.True(x >= Fraction.MinusOne);

            Assert.True(x == Fraction.PositiveInfinity);
            Assert.False(x != Fraction.PositiveInfinity);
            Assert.False(x < Fraction.PositiveInfinity);
            Assert.True(x <= Fraction.PositiveInfinity);
            Assert.False(x > Fraction.PositiveInfinity);
            Assert.True(x >= Fraction.PositiveInfinity);

            Assert.False(x == Fraction.NegativeInfinity);
            Assert.True(x != Fraction.NegativeInfinity);
            Assert.False(x < Fraction.NegativeInfinity);
            Assert.False(x <= Fraction.NegativeInfinity);
            Assert.True(x > Fraction.NegativeInfinity);
            Assert.True(x >= Fraction.NegativeInfinity);

            Assert.False(x == Fraction.NaN);
            Assert.True(x != Fraction.NaN);
            Assert.False(x < Fraction.NaN);
            Assert.False(x <= Fraction.NaN);
            Assert.True(x > Fraction.NaN);
            Assert.True(x >= Fraction.NaN);
        }

        [Fact]
        public void Test_Comparison_NegativeInfinity()
        {
            Fraction x = Fraction.NegativeInfinity;

            Assert.False(x == Fraction.One);
            Assert.True(x != Fraction.One);
            Assert.True(x < Fraction.One);
            Assert.True(x <= Fraction.One);
            Assert.False(x > Fraction.One);
            Assert.False(x >= Fraction.One);

            Assert.False(x == Fraction.Zero);
            Assert.True(x != Fraction.Zero);
            Assert.True(x < Fraction.Zero);
            Assert.True(x <= Fraction.Zero);
            Assert.False(x > Fraction.Zero);
            Assert.False(x >= Fraction.Zero);

            Assert.False(x == Fraction.MinusOne);
            Assert.True(x != Fraction.MinusOne);
            Assert.True(x < Fraction.MinusOne);
            Assert.True(x <= Fraction.MinusOne);
            Assert.False(x > Fraction.MinusOne);
            Assert.False(x >= Fraction.MinusOne);

            Assert.False(x == Fraction.PositiveInfinity);
            Assert.True(x != Fraction.PositiveInfinity);
            Assert.True(x < Fraction.PositiveInfinity);
            Assert.True(x <= Fraction.PositiveInfinity);
            Assert.False(x > Fraction.PositiveInfinity);
            Assert.False(x >= Fraction.PositiveInfinity);

            Assert.True(x == Fraction.NegativeInfinity);
            Assert.False(x != Fraction.NegativeInfinity);
            Assert.False(x < Fraction.NegativeInfinity);
            Assert.True(x <= Fraction.NegativeInfinity);
            Assert.False(x > Fraction.NegativeInfinity);
            Assert.True(x >= Fraction.NegativeInfinity);

            Assert.False(x == Fraction.NaN);
            Assert.True(x != Fraction.NaN);
            Assert.False(x < Fraction.NaN);
            Assert.False(x <= Fraction.NaN);
            Assert.True(x > Fraction.NaN);
            Assert.True(x >= Fraction.NaN);
        }

        [Fact]
        public void Test_Comparison_NaN()
        {
            Fraction x = Fraction.NaN;

            Assert.False(x == Fraction.One);
            Assert.True(x != Fraction.One);
            Assert.True(x < Fraction.One);
            Assert.True(x <= Fraction.One);
            Assert.False(x > Fraction.One);
            Assert.False(x >= Fraction.One);

            Assert.False(x == Fraction.Zero);
            Assert.True(x != Fraction.Zero);
            Assert.True(x < Fraction.Zero);
            Assert.True(x <= Fraction.Zero);
            Assert.False(x > Fraction.Zero);
            Assert.False(x >= Fraction.Zero);

            Assert.False(x == Fraction.MinusOne);
            Assert.True(x != Fraction.MinusOne);
            Assert.True(x < Fraction.MinusOne);
            Assert.True(x <= Fraction.MinusOne);
            Assert.False(x > Fraction.MinusOne);
            Assert.False(x >= Fraction.MinusOne);

            Assert.False(x == Fraction.PositiveInfinity);
            Assert.True(x != Fraction.PositiveInfinity);
            Assert.True(x < Fraction.PositiveInfinity);
            Assert.True(x <= Fraction.PositiveInfinity);
            Assert.False(x > Fraction.PositiveInfinity);
            Assert.False(x >= Fraction.PositiveInfinity);

            Assert.False(x == Fraction.NegativeInfinity);
            Assert.True(x != Fraction.NegativeInfinity);
            Assert.True(x < Fraction.NegativeInfinity);
            Assert.True(x <= Fraction.NegativeInfinity);
            Assert.False(x > Fraction.NegativeInfinity);
            Assert.False(x >= Fraction.NegativeInfinity);

            Assert.False(x == Fraction.NaN);
            Assert.True(x != Fraction.NaN);
            Assert.False(x < Fraction.NaN);
            Assert.True(x <= Fraction.NaN);
            Assert.False(x > Fraction.NaN);
            Assert.True(x >= Fraction.NaN);
        }

        [Fact]
        public void Test_Addition()
        {
            Fraction x = Fraction.One / 2;
            Fraction y = Fraction.One / 4;
            Fraction z = Fraction.Create(3, 4);

            Assert.Equal(z, x + y);
            Assert.Equal(x, x + Fraction.Zero);
            Assert.Equal(Fraction.PositiveInfinity, x + Fraction.PositiveInfinity);
            Assert.Equal(Fraction.NegativeInfinity, x + Fraction.NegativeInfinity);
            Assert.True((x + Fraction.NaN).IsNaN);
        }

        [Fact]
        public void Test_Subtraction()
        {
            Fraction x = Fraction.Create(3, 4);
            Fraction y = Fraction.One / 2;
            Fraction z = Fraction.One / 4;

            Assert.Equal(z, x - y);
            Assert.Equal(x, x - Fraction.Zero);
            Assert.Equal(Fraction.NegativeInfinity, x - Fraction.PositiveInfinity);
            Assert.Equal(Fraction.PositiveInfinity, x - Fraction.NegativeInfinity);
            Assert.Equal(Fraction.PositiveInfinity, Fraction.PositiveInfinity - x);
            Assert.Equal(Fraction.NegativeInfinity, Fraction.NegativeInfinity - x);
            Assert.True((x - Fraction.NaN).IsNaN);
        }

        [Fact]
        public void Test_Pow()
        {
            Fraction x = Fraction.One / 2;

            Assert.Equal(Fraction.One, Fraction.Math.Pow(x, 0));
            Assert.Equal(x, Fraction.Math.Pow(x, 1));
            Assert.Equal(Fraction.One / 4, Fraction.Math.Pow(x, 2));
            Assert.Equal(Fraction.One / 8, Fraction.Math.Pow(x, 3));

            Assert.Equal(2, Fraction.Math.Pow(x, -1));
            Assert.Equal(4, Fraction.Math.Pow(x, -2));
            Assert.Equal(8, Fraction.Math.Pow(x, -3));
        }

        [Fact]
        public void Test_Sin()
        {
            Fraction x = Fraction.Zero;

            Assert.Equal(Fraction.MinusOne, Fraction.Math.Sin(45));
        }
    }
}
