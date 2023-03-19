using System;

public class ValueWithReactionOnChange<T> where T : IComparable
{
    public Action<T> ValueChanged;

    public T Value
    {
        get => _value;
        set
        {
            if (value.CompareTo(_value) != 0)
            {
                _value = value;
                ValueChanged?.Invoke(_value);
            }
        }
    }

    private T _value;

    public ValueWithReactionOnChange()
    {
    }

    public ValueWithReactionOnChange(T startValue)
    {
        _value = startValue;
    }
}