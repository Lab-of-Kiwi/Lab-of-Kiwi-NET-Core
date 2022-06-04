using System;
using Xunit;

namespace LabOfKiwi;

public partial class ArrayExtensionsTests
{
    [Fact]
    public void Test_PadLeft()
    {
        int[] arr = new int[] { 1, 2, 3 };
        int[] result = arr.PadLeft(5, 100);

        Assert.Equal(5, result.Length);
        Assert.Equal(100, result[0]);
        Assert.Equal(100, result[1]);
        Assert.Equal(1, result[2]);
        Assert.Equal(2, result[3]);
        Assert.Equal(3, result[4]);
    }

    [Fact]
    public void Test_PadLeft_SameLength()
    {
        int[] arr = new int[] { 1, 2, 3 };
        int[] result = arr.PadLeft(3, 100);

        Assert.False(ReferenceEquals(arr, result));
        Assert.Equal(3, result.Length);
        Assert.Equal(1, result[0]);
        Assert.Equal(2, result[1]);
        Assert.Equal(3, result[2]);
    }

    [Fact]
    public void Test_PadLeft_SmallerTarget()
    {
        int[] arr = new int[] { 1, 2, 3 };
        int[] result = arr.PadLeft(2, 100);

        Assert.True(ReferenceEquals(arr, result));
        Assert.Equal(3, result.Length);
        Assert.Equal(1, result[0]);
        Assert.Equal(2, result[1]);
        Assert.Equal(3, result[2]);
    }

    [Fact]
    public void Test_PadRight()
    {
        int[] arr = new int[] { 1, 2, 3 };
        int[] result = arr.PadRight(5, 100);

        Assert.Equal(5, result.Length);
        Assert.Equal(1, result[0]);
        Assert.Equal(2, result[1]);
        Assert.Equal(3, result[2]);
        Assert.Equal(100, result[3]);
        Assert.Equal(100, result[4]);
    }

    [Fact]
    public void Test_PadRight_SameLength()
    {
        int[] arr = new int[] { 1, 2, 3 };
        int[] result = arr.PadRight(3, 100);

        Assert.False(ReferenceEquals(arr, result));
        Assert.Equal(3, result.Length);
        Assert.Equal(1, result[0]);
        Assert.Equal(2, result[1]);
        Assert.Equal(3, result[2]);
    }

    [Fact]
    public void Test_PadRight_SmallerTarget()
    {
        int[] arr = new int[] { 1, 2, 3 };
        int[] result = arr.PadRight(2, 100);

        Assert.True(ReferenceEquals(arr, result));
        Assert.Equal(3, result.Length);
        Assert.Equal(1, result[0]);
        Assert.Equal(2, result[1]);
        Assert.Equal(3, result[2]);
    }

    [Fact]
    public void Test_PadCenter_SameLength()
    {
        int[] arr = new int[] { 1, 2, 3 };
        int[] result = arr.PadCenter(3, 100);

        Assert.False(ReferenceEquals(arr, result));
        Assert.Equal(3, result.Length);
        Assert.Equal(1, result[0]);
        Assert.Equal(2, result[1]);
        Assert.Equal(3, result[2]);
    }

    [Fact]
    public void Test_PadCenter_SmallerTarget()
    {
        int[] arr = new int[] { 1, 2, 3 };
        int[] result = arr.PadCenter(2, 100);

        Assert.True(ReferenceEquals(arr, result));
        Assert.Equal(3, result.Length);
        Assert.Equal(1, result[0]);
        Assert.Equal(2, result[1]);
        Assert.Equal(3, result[2]);
    }

    [Fact]
    public void Test_PadCenter_OddOdd()
    {
        int[] arr = new int[] { 1, 2, 3 };
        int[] result = arr.PadCenter(7, 100);

        Assert.Equal(7, result.Length);
        Assert.Equal(100, result[0]);
        Assert.Equal(100, result[1]);
        Assert.Equal(1, result[2]);
        Assert.Equal(2, result[3]);
        Assert.Equal(3, result[4]);
        Assert.Equal(100, result[5]);
        Assert.Equal(100, result[6]);
    }

    [Fact]
    public void Test_PadCenter_EvenEven()
    {
        int[] arr = new int[] { 1, 2 };
        int[] result = arr.PadCenter(6, 100);

        Assert.Equal(6, result.Length);
        Assert.Equal(100, result[0]);
        Assert.Equal(100, result[1]);
        Assert.Equal(1, result[2]);
        Assert.Equal(2, result[3]);
        Assert.Equal(100, result[4]);
        Assert.Equal(100, result[5]);
    }

    [Fact]
    public void Test_PadCenter_OddEven()
    {
        int[] arr = new int[] { 1, 2, 3 };
        int[] result = arr.PadCenter(6, 100);

        Assert.Equal(6, result.Length);
        Assert.Equal(100, result[0]);
        Assert.Equal(1, result[1]);
        Assert.Equal(2, result[2]);
        Assert.Equal(3, result[3]);
        Assert.Equal(100, result[4]);
        Assert.Equal(100, result[5]);
    }

    [Fact]
    public void Test_PadCenter_EvenOdd()
    {
        int[] arr = new int[] { 1, 2 };
        int[] result = arr.PadCenter(5, 100);

        Assert.Equal(5, result.Length);
        Assert.Equal(100, result[0]);
        Assert.Equal(1, result[1]);
        Assert.Equal(2, result[2]);
        Assert.Equal(100, result[3]);
        Assert.Equal(100, result[4]);
    }

    [Fact]
    public void Test_PadCenter_OddEven_PreferLeftPadding()
    {
        int[] arr = new int[] { 1, 2, 3 };
        int[] result = arr.PadCenter(6, 100, preferLeftPadding: true);

        Assert.Equal(6, result.Length);
        Assert.Equal(100, result[0]);
        Assert.Equal(100, result[1]);
        Assert.Equal(1, result[2]);
        Assert.Equal(2, result[3]);
        Assert.Equal(3, result[4]);
        Assert.Equal(100, result[5]);
    }

    [Fact]
    public void Test_PadCenter_EvenOdd_PreferLeftPadding()
    {
        int[] arr = new int[] { 1, 2 };
        int[] result = arr.PadCenter(5, 100, preferLeftPadding: true);

        Assert.Equal(5, result.Length);
        Assert.Equal(100, result[0]);
        Assert.Equal(100, result[1]);
        Assert.Equal(1, result[2]);
        Assert.Equal(2, result[3]);
        Assert.Equal(100, result[4]);
    }

    [Fact]
    public void Test_Pad_Null()
    {
        int[] arr = null;

        Assert.Throws<ArgumentNullException>(() => arr.PadLeft(10, 100));
        Assert.Throws<ArgumentNullException>(() => arr.PadRight(10, 100));
        Assert.Throws<ArgumentNullException>(() => arr.PadCenter(10, 100, preferLeftPadding: false));
        Assert.Throws<ArgumentNullException>(() => arr.PadCenter(10, 100, preferLeftPadding: true));
    }

    [Fact]
    public void Test_Pad_NegativeTotalWidth()
    {
        int[] arr = new int[5];

        Assert.Throws<ArgumentOutOfRangeException>(() => arr.PadLeft(-10, 100));
        Assert.Throws<ArgumentOutOfRangeException>(() => arr.PadRight(-10, 100));
        Assert.Throws<ArgumentOutOfRangeException>(() => arr.PadCenter(-10, 100, preferLeftPadding: false));
        Assert.Throws<ArgumentOutOfRangeException>(() => arr.PadCenter(-10, 100, preferLeftPadding: true));
    }

    [Fact]
    public void Test_StructurallyEquals_ValueType()
    {
        int[] arr = new int[] { 1, 2, 3, 4, 5 };
        Assert.True(arr.StructurallyEquals(arr));

        int[] other = new int[] { 1, 2, 3, 4, 5 };
        Assert.True(arr.StructurallyEquals(other));

        other = new int[] { 1, 2, 3, 5, 6 };
        Assert.False(arr.StructurallyEquals(other));

        Assert.False(arr.StructurallyEquals(null));
        Assert.True(Array.Empty<int>().StructurallyEquals(null));
    }

    [Fact]
    public void Test_StructurallyEquals_RefType()
    {
        string s1 = "Hello";
        string s2 = "World";

        string[] arr = new string[] { s1, s2 };
        Assert.True(arr.StructurallyEquals(arr));

        string[] other = new string[] { s1, s2 };
        Assert.True(arr.StructurallyEquals(other));

        other = new string[] { s2, s1 };
        Assert.False(arr.StructurallyEquals(other));

        Assert.False(arr.StructurallyEquals(null));
        Assert.True(Array.Empty<string>().StructurallyEquals(null));

        string s3 = new string(s1.ToCharArray());
        string s4 = new string(s2.ToCharArray());

        Assert.False(ReferenceEquals(s1, s3));
        Assert.False(ReferenceEquals(s2, s4));
        Assert.True(s1 == s3);
        Assert.True(s2 == s4);

        other = new string[] { s3, s4 };
        Assert.True(arr.StructurallyEquals(other));
        Assert.False(arr.StructurallyEquals(other, forceReferenceMatch: true));
    }
}
