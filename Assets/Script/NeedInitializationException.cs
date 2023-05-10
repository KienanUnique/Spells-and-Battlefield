using System;

public class NeedInitializationException : Exception
{
    public NeedInitializationException() : base("Need initialization before usage")
    {
    }
}