using System;
using System.Diagnostics;

namespace LabOfKiwi.Cmd;

public enum BorderType : uint
{
    None,
    LightSolid,
    HeavySolid,
    LightDoubleDash,
    HeavyDoubleDash,
    LightTripleDash,
    HeavyTripleDash,
    LightQuadrupleDash,
    HeavyQuadrupleDash,
    DoubleSolid
}

internal static class BorderTypeExtensions
{
    public static BorderTypeDefinition? GetDefinition(this BorderType value)
    {
        if (value == BorderType.None) return null;
        return new(value);
    }

    public static bool IsHeavy(this BorderType value) => value switch
    {
        BorderType.HeavySolid         => true,
        BorderType.HeavyDoubleDash    => true,
        BorderType.HeavyTripleDash    => true,
        BorderType.HeavyQuadrupleDash => true,
        _                             => false
    };

    public static bool IsLight(this BorderType value) => value switch
    {
        BorderType.LightSolid         => true,
        BorderType.LightDoubleDash    => true,
        BorderType.LightTripleDash    => true,
        BorderType.LightQuadrupleDash => true,
        _                             => false
    };

    public static bool IsDashed(this BorderType value) => value switch
    {
        BorderType.LightDoubleDash    => true,
        BorderType.HeavyDoubleDash    => true,
        BorderType.LightTripleDash    => true,
        BorderType.HeavyTripleDash    => true,
        BorderType.LightQuadrupleDash => true,
        BorderType.HeavyQuadrupleDash => true,
        _                             => false
    };

    private static int GetIndex(this BorderType value) => unchecked((int)(uint)value) - 1;

    public static uint CombineIndexes(this BorderType value1, BorderType value2)
    {
        return ((uint)value1 & 0xFFFFU) | (((uint)value2 & 0xFFFFU) << 16);
    }

    internal readonly struct BorderTypeDefinition
    {
        private static readonly char[][] Characters = new char[][]
        {
            new char[] { '\u2500', '\u2502', '\u250C', '\u2510', '\u2514', '\u2518', '\u251C', '\u2524', '\u252C', '\u2534', '\u253C' },
            new char[] { '\u2501', '\u2503', '\u250F', '\u2513', '\u2517', '\u251B', '\u2523', '\u252B', '\u2533', '\u253B', '\u254B' },
            new char[] { '\u254C', '\u254E', '\u250C', '\u2510', '\u2514', '\u2518', '\u251C', '\u2524', '\u252C', '\u2534', '\u253C' },
            new char[] { '\u254D', '\u254F', '\u250F', '\u2513', '\u2517', '\u251B', '\u2523', '\u252B', '\u2533', '\u253B', '\u254B' },
            new char[] { '\u2504', '\u2506', '\u250C', '\u2510', '\u2514', '\u2518', '\u251C', '\u2524', '\u252C', '\u2534', '\u253C' },
            new char[] { '\u2505', '\u2507', '\u250F', '\u2513', '\u2517', '\u251B', '\u2523', '\u252B', '\u2533', '\u253B', '\u254B' },
            new char[] { '\u2508', '\u250A', '\u250C', '\u2510', '\u2514', '\u2518', '\u251C', '\u2524', '\u252C', '\u2534', '\u253C' },
            new char[] { '\u2509', '\u250B', '\u250F', '\u2513', '\u2517', '\u251B', '\u2523', '\u252B', '\u2533', '\u253B', '\u254B' },
            new char[] { '\u2550', '\u2551', '\u2554', '\u2557', '\u255A', '\u255D', '\u2560', '\u2563', '\u2566', '\u2569', '\u256C' }
        };

        private readonly BorderType _type;

        public BorderTypeDefinition(BorderType type)
        {
            Debug.Assert(type != BorderType.None);
            _type = type;
        }

        public char Horizontal => Characters[_type.GetIndex()][0];

        public char Vertical => Characters[_type.GetIndex()][1];

        public char TopLeftCorner => Characters[_type.GetIndex()][2];

        public char TopRightCorner => Characters[_type.GetIndex()][3];

        public char BottomLeftCorner => Characters[_type.GetIndex()][4];

        public char BottomRightCorner => Characters[_type.GetIndex()][5];

        public char LeftInterset => Characters[_type.GetIndex()][6];

        public char RightInterset => Characters[_type.GetIndex()][7];

        public char TopIntersect => Characters[_type.GetIndex()][8];

        public char BottomIntersect => Characters[_type.GetIndex()][9];

        public char CentralIntersect => Characters[_type.GetIndex()][10];

        public char[] AllChars
        {
            get
            {
                char[] chars = Characters[_type.GetIndex()];
                char[] copy = new char[chars.Length];
                Array.Copy(chars, copy, chars.Length);
                return copy;
            }
        }
    }
}
