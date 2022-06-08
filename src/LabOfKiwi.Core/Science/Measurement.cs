using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabOfKiwi.Science;

public readonly struct Measurement : IMeasurement, IComparable<Measurement>, IEquatable<Measurement>
{
    private readonly double _value;
    private readonly Dimension _dimension;

    internal Measurement(double value, Dimension dimension)
    {
        _value = value;
        _dimension = dimension;
    }

    public double Value => _value;

    public Dimension Dimension => _dimension;

    public int CompareTo(Measurement other)
    {
        if (Dimension == other.Dimension)
        {
            return Value.CompareTo(other.Value);
        }

        throw new ArgumentException("Measurements must have same dimension for comparisons.");
    }

    public override bool Equals(object? obj)
    {
        if (obj is IMeasurement other)
        {
            return Value == other.Value && Dimension == other.Dimension;
        }

        return false;
    }

    public bool Equals(Measurement other)
    {
        return Value == other.Value && Dimension == other.Dimension;
    }

    public override int GetHashCode() => HashCode.Combine(typeof(IMeasurement), Value, Dimension);

    public Measurement Pow(int exponent)
    {
        return new Measurement(Math.Pow(Value, exponent), exponent * Dimension);
    }

    public static Measurement operator *(Measurement left, IMeasurement right)
    {
        return new Measurement(left.Value * right.Value, left.Dimension * right.Dimension);
    }

    public static Measurement operator /(Measurement left, IMeasurement right)
    {
        return new Measurement(left.Value / right.Value, left.Dimension / right.Dimension);
    }

    public static Measurement operator *(Measurement left, double right)
    {
        return new Measurement(left.Value * right, left.Dimension);
    }

    public static Measurement operator *(double left, Measurement right)
    {
        return new Measurement(left * right.Value, right.Dimension);
    }

    public static Measurement operator /(Measurement left, double right)
    {
        return new Measurement(left.Value / right, left.Dimension);
    }

    public static Measurement operator /(double left, Measurement right)
    {
        return new Measurement(left / right.Value, ~right.Dimension);
    }

    public static Measurement operator ++(Measurement m)
    {
        return new Measurement(m.Value + 1.0, m.Dimension);
    }

    public static Measurement operator --(Measurement m)
    {
        return new Measurement(m.Value - 1.0, m.Dimension);
    }

    public static Measurement operator +(Measurement m)
    {
        return m;
    }

    public static Measurement operator -(Measurement m)
    {
        return new Measurement(-m.Value, m.Dimension);
    }

    public static bool operator ==(Measurement left, IMeasurement right)
    {
        return left.Value == right.Value && left.Dimension == right.Dimension;
    }

    public static bool operator !=(Measurement left, IMeasurement right)
    {
        return left.Value == right.Value && left.Dimension == right.Dimension;
    }

    public static bool operator ==(IMeasurement left, Measurement right)
    {
        return left.Value == right.Value && left.Dimension == right.Dimension;
    }

    public static bool operator !=(IMeasurement left, Measurement right)
    {
        return left.Value == right.Value && left.Dimension == right.Dimension;
    }
}
