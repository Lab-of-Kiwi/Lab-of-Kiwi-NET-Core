using System;
using Xunit;

namespace LabOfKiwi.Text;

public class StringJoinerTests
{
    [Fact]
    public void Test_Empty()
    {
        var sj = new StringJoiner(",");
        Assert.Equal(string.Empty, sj.ToString());
    }

    [Fact]
    public void Test_Empty_WithDefaultValue()
    {
        var sj = new StringJoiner(",").SetEmptyValue("foo");
        Assert.Equal("foo", sj.ToString());
    }

    [Fact]
    public void Test_EmptyWithPrefix()
    {
        var sj = new StringJoiner(",", prefix: "{");
        Assert.Equal("{", sj.ToString());
    }

    [Fact]
    public void Test_EmptyWithSuffix()
    {
        var sj = new StringJoiner(",", suffix: "}");
        Assert.Equal("}", sj.ToString());
    }

    [Fact]
    public void Test_EmptyWithPrefixAndSuffix()
    {
        var sj = new StringJoiner(",", prefix: "{", suffix: "}");
        Assert.Equal("{}", sj.ToString());
    }

    [Fact]
    public void Test_Single()
    {
        var sj = new StringJoiner(",").Add("foo");
        Assert.Equal("foo", sj.ToString());
        sj.SetEmptyValue("bar");
        Assert.Equal("foo", sj.ToString());
    }

    [Fact]
    public void Test_SingleWithPrefix()
    {
        var sj = new StringJoiner(",", prefix: "{").Add("foo");
        Assert.Equal("{foo", sj.ToString());
        sj.SetEmptyValue("bar");
        Assert.Equal("{foo", sj.ToString());
    }

    [Fact]
    public void Test_SingleWithSuffix()
    {
        var sj = new StringJoiner(",", suffix: "}").Add("foo");
        Assert.Equal("foo}", sj.ToString());
        sj.SetEmptyValue("bar");
        Assert.Equal("foo}", sj.ToString());
    }

    [Fact]
    public void Test_SingleWithPrefixAndSuffix()
    {
        var sj = new StringJoiner(",", prefix: "{", suffix: "}").Add("foo");
        Assert.Equal("{foo}", sj.ToString());
        sj.SetEmptyValue("bar");
        Assert.Equal("{foo}", sj.ToString());
    }

    [Fact]
    public void Test_Multi()
    {
        var sj = new StringJoiner(",").Add("foo").Add("bar");
        Assert.Equal("foo,bar", sj.ToString());
        sj.SetEmptyValue("baz");
        Assert.Equal("foo,bar", sj.ToString());
    }

    [Fact]
    public void Test_MultiWithPrefix()
    {
        var sj = new StringJoiner(",", prefix: "{").Add("foo").Add("bar");
        Assert.Equal("{foo,bar", sj.ToString());
        sj.SetEmptyValue("baz");
        Assert.Equal("{foo,bar", sj.ToString());
    }

    [Fact]
    public void Test_MultiWithSuffix()
    {
        var sj = new StringJoiner(",", suffix: "}").Add("foo").Add("bar");
        Assert.Equal("foo,bar}", sj.ToString());
        sj.SetEmptyValue("baz");
        Assert.Equal("foo,bar}", sj.ToString());
    }

    [Fact]
    public void Test_MultiWithPrefixAndSuffix()
    {
        var sj = new StringJoiner(",", prefix: "{", suffix: "}").Add("foo").Add("bar");
        Assert.Equal("{foo,bar}", sj.ToString());
        sj.SetEmptyValue("baz");
        Assert.Equal("{foo,bar}", sj.ToString());
    }

    [Fact]
    public void Test_MergeEmpty()
    {
        var sj = new StringJoiner(",", prefix: "{", suffix: "}").Add("foo");
        var other = new StringJoiner("|", prefix: "[", suffix: "]");

        sj.Merge(other).Add("bar");
        Assert.Equal("{foo,bar}", sj.ToString());
    }

    [Fact]
    public void Test_MergeSingle()
    {
        var sj = new StringJoiner(",", prefix: "{", suffix: "}").Add("foo");
        var other = new StringJoiner("|", prefix: "[", suffix: "]").Add("Hello");

        sj.Merge(other).Add("bar");
        Assert.Equal("{foo,Hello,bar}", sj.ToString());
    }

    [Fact]
    public void Test_MergeMulti()
    {
        var sj = new StringJoiner(",", prefix: "{", suffix: "}").Add("foo");
        var other = new StringJoiner("|", prefix: "[", suffix: "]").Add("Hello").Add("World");

        sj.Merge(other).Add("bar");
        Assert.Equal("{foo,Hello|World,bar}", sj.ToString());
    }

    [Theory]
    [InlineData(",", "{", null)]
    [InlineData(",", null, "}")]
    [InlineData(null, "{", "}")]
    [InlineData(",", null, null)]
    [InlineData(null, "{", null)]
    [InlineData(null, null, "}")]
    [InlineData(null, null, null)]
    public void Test_Nulls(string delimiter, string prefix, string suffix)
    {
        Assert.Throws<ArgumentNullException>(() => new StringJoiner(delimiter, prefix, suffix));
    }

    [Fact]
    public void Test_AddMerge()
    {
        var sj = new StringJoiner(",");
        Assert.Throws<ArgumentNullException>(() => sj.Merge(null));
    }
}
