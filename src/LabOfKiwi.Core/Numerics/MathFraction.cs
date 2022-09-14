using System;

namespace LabOfKiwi.Numerics;

public static class MathFraction
{
    public static sbyte GreatestCommonDivisor(sbyte left, sbyte right)
    {
        while (right != 0)
        {
            sbyte temp = (sbyte)(left % right);
            left = right;
            right = temp;
        }

        return Math.Abs(left);
    }

    public static short GreatestCommonDivisor(short left, short right)
    {
        while (right != 0)
        {
            short temp = (short)(left % right);
            left = right;
            right = temp;
        }

        return Math.Abs(left);
    }

    public static int GreatestCommonDivisor(int left, int right)
    {
        while (right != 0)
        {
            int temp = left % right;
            left = right;
            right = temp;
        }

        return Math.Abs(left);
    }

    public static long GreatestCommonDivisor(long left, long right)
    {
        while (right != 0)
        {
            long temp = left % right;
            left = right;
            right = temp;
        }

        return Math.Abs(left);
    }

    public static byte GreatestCommonDivisor(byte left, byte right)
    {
        while (right != 0)
        {
            byte temp = (byte)(left % right);
            left = right;
            right = temp;
        }

        return left;
    }

    public static ushort GreatestCommonDivisor(ushort left, ushort right)
    {
        while (right != 0)
        {
            ushort temp = (ushort)(left % right);
            left = right;
            right = temp;
        }

        return left;
    }

    public static uint GreatestCommonDivisor(uint left, uint right)
    {
        while (right != 0)
        {
            uint temp = left % right;
            left = right;
            right = temp;
        }

        return left;
    }

    public static ulong GreatestCommonDivisor(ulong left, ulong right)
    {
        while (right != 0)
        {
            ulong temp = left % right;
            left = right;
            right = temp;
        }

        return left;
    }
}
