using System;
public class ValueWithReactionOnChange<ValueType> where ValueType : IComparable
{
    public Action<ValueType> ValueChanged;
    public ValueType Value
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
    private ValueType _value;
}
