using System;

namespace LabOfKiwi.Science;

public static class Dimensionless
{
    private static readonly Dimension s_dimension = new();

    public static Dimension Dimension => s_dimension;

    public readonly struct Measurement : IMeasurement, IComparable<Measurement>, IEquatable<double>, IEquatable<Measurement>
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

            if (obj is double dother)
            {
                return Value == dother;
            }

            return false;
        }

        public bool Equals(double other) => Value == other;

        public bool Equals(Measurement other) => Value == other.Value;

        public override int GetHashCode() => HashCode.Combine(typeof(IMeasurement), Value, Dimension);

        public double Pow(double exponent)
        {
            return Math.Pow(Value, exponent);
        }

        Science.Measurement IMeasurement.Pow(int exponent)
        {
            return new Science.Measurement(Math.Pow(Value, exponent), s_dimension);
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
        public static double operator +(Measurement left, Measurement right) => left.Value + right.Value;

        public static double operator +(Measurement left, double right) => left.Value + right;

        public static double operator +(double left, Measurement right) => left + right.Value;

        public static double operator -(Measurement left, Measurement right) => left.Value - right.Value;

        public static double operator -(Measurement left, double right) => left.Value - right;

        public static double operator -(double left, Measurement right) => left - right.Value;

        public static double operator *(Measurement left, Measurement right) => left.Value * right.Value;

        public static double operator *(Measurement left, double right) => left.Value * right;

        public static double operator *(double left, Measurement right) => left * right.Value;

        public static double operator /(Measurement left, Measurement right) => left.Value / right.Value;

        public static double operator /(Measurement left, double right) => left.Value / right;

        public static double operator /(double left, Measurement right) => left / right.Value;

        public static Measurement operator ++(Measurement m) => new(m.Value + 1.0);

        public static Measurement operator --(Measurement m) => new(m.Value - 1.0);

        public static Measurement operator +(Measurement m) => m;

        public static Measurement operator -(Measurement m) => new(-m.Value);
        #endregion
    }
}
