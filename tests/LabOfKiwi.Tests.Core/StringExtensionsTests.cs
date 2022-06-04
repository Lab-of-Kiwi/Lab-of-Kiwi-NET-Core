using System;
using Xunit;

namespace LabOfKiwi;

public class StringExtensionsTests
{
    [Fact]
    public void Test_PadCenter_SameLength()
    {
        string value = "Foo";
        string result = value.PadCenter(3, '_');

        Assert.False(ReferenceEquals(value, result));
        Assert.Equal(result, value);
    }

    [Fact]
    public void Test_PadCenter_SmallerTarget()
    {
        string value = "Foobar";
        string result = value.PadCenter(3, '_');
        Assert.True(ReferenceEquals(value, result));
    }

    [Fact]
    public void Test_PadCenter_OddOdd()
    {
        string value = "Foo";
        string result = value.PadCenter(7, '_');
        Assert.Equal("__Foo__", result);
    }

    [Fact]
    public void Test_PadCenter_EvenEven()
    {
        string value = "Foos";
        string result = value.PadCenter(6, '_');
        Assert.Equal("_Foos_", result);
    }

    [Fact]
    public void Test_PadCenter_OddEven()
    {
        string value = "Foo";
        string result = value.PadCenter(6, '_');
        Assert.Equal("_Foo__", result);
    }

    [Fact]
    public void Test_PadCenter_EvenOdd()
    {
        string value = "Foos";
        string result = value.PadCenter(7, '_');
        Assert.Equal("_Foos__", result);
    }

    [Fact]
    public void Test_PadCenter_OddEven_PreferLeftPadding()
    {
        string value = "Foo";
        string result = value.PadCenter(6, '_', preferLeftPadding: true);
        Assert.Equal("__Foo_", result);
    }

    [Fact]
    public void Test_PadCenter_EvenOdd_PreferLeftPadding()
    {
        string value = "Foos";
        string result = value.PadCenter(7, '_', preferLeftPadding: true);
        Assert.Equal("__Foos_", result);
    }

    [Fact]
    public void Test_PadCenter_Null()
    {
        string value = null;
        Assert.Throws<ArgumentNullException>(() => value.PadCenter(10, '_', preferLeftPadding: false));
        Assert.Throws<ArgumentNullException>(() => value.PadCenter(10, '_', preferLeftPadding: true));
    }

    [Fact]
    public void Test_PadCenter_NegativeTotalWidth()
    {
        string value = "foo";
        Assert.Throws<ArgumentOutOfRangeException>(() => value.PadCenter(-10, '_', preferLeftPadding: false));
        Assert.Throws<ArgumentOutOfRangeException>(() => value.PadCenter(-10, '_', preferLeftPadding: true));
    }
}
