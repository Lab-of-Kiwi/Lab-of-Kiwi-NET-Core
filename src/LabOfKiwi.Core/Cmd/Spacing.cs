namespace LabOfKiwi.Cmd;

public sealed class Spacing
{
    private int _top;
    private int _right;
    private int _bottom;
    private int _left;

    public int Top
    {
        get => _top;
        set => _top = ThrowHelper.RangeCheck(nameof(value), value, minValue: 0);
    }

    public int Right
    {
        get => _right;
        set => _right = ThrowHelper.RangeCheck(nameof(value), value, minValue: 0);
    }

    public int Bottom
    {
        get => _bottom;
        set => _bottom = ThrowHelper.RangeCheck(nameof(value), value, minValue: 0);
    }

    public int Left
    {
        get => _left;
        set => _left = ThrowHelper.RangeCheck(nameof(value), value, minValue: 0);
    }

    public void Set(int value)
    {
        ThrowHelper.RangeCheck(nameof(value), value, minValue: 0);
        _top = value;
        _right = value;
        _bottom = value;
        _left = value;
    }

    public void Set(int topBottomValue, int leftRightValue)
    {
        ThrowHelper.RangeCheck(nameof(topBottomValue), topBottomValue, minValue: 0);
        ThrowHelper.RangeCheck(nameof(leftRightValue), leftRightValue, minValue: 0);
        _top = topBottomValue;
        _right = leftRightValue;
        _bottom = topBottomValue;
        _left = leftRightValue;
    }

    public void Set(int topValue, int leftRightValue, int bottomValue)
    {
        ThrowHelper.RangeCheck(nameof(topValue),       topValue,       minValue: 0);
        ThrowHelper.RangeCheck(nameof(leftRightValue), leftRightValue, minValue: 0);
        ThrowHelper.RangeCheck(nameof(bottomValue),    bottomValue,    minValue: 0);
        _top = topValue;
        _right = leftRightValue;
        _bottom = bottomValue;
        _left = leftRightValue;
    }

    public void Set(int topValue, int rightValue, int bottomValue, int leftValue)
    {
        ThrowHelper.RangeCheck(nameof(topValue),    topValue,    minValue: 0);
        ThrowHelper.RangeCheck(nameof(rightValue),  rightValue,  minValue: 0);
        ThrowHelper.RangeCheck(nameof(bottomValue), bottomValue, minValue: 0);
        ThrowHelper.RangeCheck(nameof(leftValue),   leftValue,   minValue: 0);
        _top = topValue;
        _right = rightValue;
        _bottom = bottomValue;
        _left = leftValue;
    }

    public (int Top, int Right, int Bottom, int Left) Deconstruct()
    {
        return (Top, Right, Bottom, Left);
    }
}
