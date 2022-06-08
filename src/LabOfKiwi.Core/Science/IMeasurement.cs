using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabOfKiwi.Science;

public interface IMeasurement : IComparable, IComparable<IMeasurement>, IEquatable<IMeasurement>
{
    double Value { get; }

    Dimension Dimension { get; }

    Measurement Pow(int exponent);

    int IComparable.CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is IMeasurement other)
        {
            return CompareTo(other);
        }

        throw new ArgumentException("Object must be a IMeasurement type.");
    }

    int IComparable<IMeasurement>.CompareTo(IMeasurement? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (Dimension == other.Dimension)
        {
            return Value.CompareTo(other.Value);
        }

        throw new ArgumentException("Measurements must have same dimension for comparisons.");
    }

    bool IEquatable<IMeasurement>.Equals(IMeasurement? other)
    {
        if (other == null)
        {
            return false;
        }

        return Value == other.Value && Dimension == other.Dimension;
    }
}
