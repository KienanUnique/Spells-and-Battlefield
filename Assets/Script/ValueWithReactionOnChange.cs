using System;

public class ValueWithReactionOnChange<T> where T : IComparable
{
    public Action<T> AfterValueChanged;
    public Action<T> BeforeValueChanged;

    private T _value;

    public ValueWithReactionOnChange(T startValue)
    {
        _value = startValue;
    }

    public T Value
    {
        get => _value;
        set
        {
            if (value.CompareTo(_value) != 0)
            {
                BeforeValueChanged?.Invoke(_value);
                _value = value;
                AfterValueChanged?.Invoke(_value);
            }
        }
    }
}