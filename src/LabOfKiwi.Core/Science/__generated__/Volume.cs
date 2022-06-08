// Code was auto-generated: 6/8/2022 10:15:17 AM
using System;

namespace LabOfKiwi.Science;

public static class Volume
{
    private static readonly Dimension s_dimension = new(length: 3);

    public static Dimension Dimension => s_dimension;

    public readonly struct Measurement : IMeasurement, IComparable<Measurement>, IEquatable<Measurement>
    {
        private readonly double _value;

        public Measurement(double value)
        {
            _value = value;
        }

        public double Value => _value;

        public Dimension Dimension => s_dimension;

        public int CompareTo(Measurement other) => Value.CompareTo(other.Value);

        public override bool Equals(object? obj)
        {
            if (obj is Measurement other)
            {
                return Value == other.Value;
            }

            if (obj is IMeasurement iother)
            {
                return Value == iother.Value && Dimension == iother.Dimension;
            }

            return false;
        }

        public bool Equals(Measurement other) => Value == other.Value;

        public override int GetHashCode() => HashCode.Combine(typeof(IMeasurement), Value, Dimension);

        public Science.Measurement Pow(int exponent)
        {
            return new Science.Measurement(Math.Pow(Value, exponent), exponent * Dimension);
        }

        public static explicit operator Measurement(Science.Measurement m)
        {
            if (s_dimension == m.Dimension)
            {
                return new Measurement(m.Value);
            }

            throw new InvalidCastException("Mismatched dimensions.");
        }

        #region Comparison Operators
        public static bool operator ==(Measurement left, Measurement right) => left.Value == right.Value;

        public static bool operator !=(Measurement left, Measurement right) => left.Value != right.Value;

        public static bool operator <(Measurement left, Measurement right) => left.Value < right.Value;

        public static bool operator <=(Measurement left, Measurement right) => left.Value <= right.Value;

        public static bool operator >(Measurement left, Measurement right) => left.Value > right.Value;

        public static bool operator >=(Measurement left, Measurement right) => left.Value >= right.Value;
        #endregion

        #region Common Arithmetic Operators
        public static Measurement operator +(Measurement left, Measurement right) => new(left.Value + right.Value);

        public static Measurement operator -(Measurement left, Measurement right) => new(left.Value - right.Value);

        public static Measurement operator *(Measurement left, double right) => new(left.Value * right);

        public static Measurement operator *(double left, Measurement right) => new(left * right.Value);

        public static double operator /(Measurement left, Measurement right) => left.Value / right.Value;

        public static Measurement operator /(Measurement left, double right) => new(left.Value / right);

        public static Measurement operator ++(Measurement m) => new(m.Value + 1.0);

        public static Measurement operator --(Measurement m) => new(m.Value - 1.0);

        public static Measurement operator +(Measurement m) => m;

        public static Measurement operator -(Measurement m) => new(-m.Value);

        public static Science.Measurement operator *(Measurement left, IMeasurement right) => new(left.Value * right.Value, left.Dimension * right.Dimension);

        public static Science.Measurement operator /(Measurement left, IMeasurement right) => new(left.Value / right.Value, left.Dimension * right.Dimension);

        public static Measurement operator *(Measurement left, Dimensionless.Measurement right) => new(left.Value * right.Value);

        public static Measurement operator *(Dimensionless.Measurement left, Measurement right) => new(left.Value * right.Value);

        public static Measurement operator /(Measurement left, Dimensionless.Measurement right) => new(left.Value / right.Value);

        public static Measurement operator /(Dimensionless.Measurement left, Measurement right) => new(left.Value / right.Value);
        #endregion

        #region Custom Arithmetic
        public Length.Measurement Cbrt() => new(Math.Cbrt(Value));

        public static Length.Measurement operator /(Measurement left, Area.Measurement right) => new(left.Value / right.Value);

        public static Mass.Measurement operator *(Measurement left, Density.Measurement right) => new(left.Value * right.Value);

        public static Area.Measurement operator /(Measurement left, Length.Measurement right) => new(left.Value / right.Value);

        public static Energy.Measurement operator *(Measurement left, Pressure.Measurement right) => new(left.Value * right.Value);
        #endregion
    }
}
