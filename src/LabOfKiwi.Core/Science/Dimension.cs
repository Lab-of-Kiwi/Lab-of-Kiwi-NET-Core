using System;
using System.Diagnostics;

namespace LabOfKiwi.Science;

[Serializable]
public readonly struct Dimension
{
    private readonly ulong _value;

    public Dimension(sbyte time = 0, sbyte length = 0, sbyte mass = 0, sbyte electricCurrent = 0,
        sbyte thermodynamicTemperature = 0, sbyte amountOfSubstance = 0, sbyte luminousIntensity = 0)
    {
        ulong value = 0UL;

        unchecked
        {
            value |= (byte)time;
            value |= (ulong)(byte)length << 8;
            value |= (ulong)(byte)mass << 16;
            value |= (ulong)(byte)electricCurrent << 24;
            value |= (ulong)(byte)thermodynamicTemperature << 32;
            value |= (ulong)(byte)amountOfSubstance << 40;
            value |= (ulong)(byte)luminousIntensity << 48;
        }

        _value = value;
    }

    public int Time => (sbyte)(_value & 0xFFUL);

    public int Length => (sbyte)((_value & 0xFF00UL) >> 8);

    public int Mass => (sbyte)((_value & 0xFF0000UL) >> 16);

    public int ElectricCurrent => (sbyte)((_value & 0xFF000000UL) >> 24);

    public int ThermodynamicTemperature => (sbyte)((_value & 0xFF00000000UL) >> 32);

    public int AmountOfSubstance => (sbyte)((_value & 0xFF0000000000UL) >> 40);

    public int LuminousIntensity => (sbyte)((_value & 0xFF000000000000UL) >> 48);

    public override bool Equals(object? obj)
    {
        return obj is Dimension other && Equals(other);
    }

    public bool Equals(Dimension other)
    {
        return _value == other._value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(Dimension), _value);
    }

    public static Dimension operator *(int left, Dimension right)
    {
        checked
        {
            return new Dimension(
                (sbyte)(left * right.Time),
                (sbyte)(left * right.Length),
                (sbyte)(left * right.Mass),
                (sbyte)(left * right.ElectricCurrent),
                (sbyte)(left * right.ThermodynamicTemperature),
                (sbyte)(left * right.AmountOfSubstance),
                (sbyte)(left * right.LuminousIntensity)
            );
        }
    }

    public static Dimension operator *(Dimension left, Dimension right)
    {
        checked
        {
            return new Dimension(
                (sbyte)(left.Time + right.Time),
                (sbyte)(left.Length + right.Length),
                (sbyte)(left.Mass + right.Mass),
                (sbyte)(left.ElectricCurrent + right.ElectricCurrent),
                (sbyte)(left.ThermodynamicTemperature + right.ThermodynamicTemperature),
                (sbyte)(left.AmountOfSubstance + right.AmountOfSubstance),
                (sbyte)(left.LuminousIntensity + right.LuminousIntensity)
            );
        }
    }

    public static Dimension operator /(Dimension left, Dimension right)
    {
        checked
        {
            return new Dimension(
                (sbyte)(left.Time - right.Time),
                (sbyte)(left.Length - right.Length),
                (sbyte)(left.Mass - right.Mass),
                (sbyte)(left.ElectricCurrent - right.ElectricCurrent),
                (sbyte)(left.ThermodynamicTemperature - right.ThermodynamicTemperature),
                (sbyte)(left.AmountOfSubstance - right.AmountOfSubstance),
                (sbyte)(left.LuminousIntensity - right.LuminousIntensity)
            );
        }
    }

    public static Dimension operator ~(Dimension value)
    {
        checked
        {
            return new Dimension(
                (sbyte)(-value.Time),
                (sbyte)(-value.Length),
                (sbyte)(-value.Mass),
                (sbyte)(-value.ElectricCurrent),
                (sbyte)(-value.ThermodynamicTemperature),
                (sbyte)(-value.AmountOfSubstance),
                (sbyte)(-value.LuminousIntensity)
            );
        }
    }

    public static bool operator ==(Dimension left, Dimension right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Dimension left, Dimension right)
    {
        return !left.Equals(right);
    }
}
