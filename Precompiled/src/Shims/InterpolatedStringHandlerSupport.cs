﻿namespace System.Runtime.CompilerServices;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
public sealed class InterpolatedStringHandlerAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Parameter)]
public sealed class InterpolatedStringHandlerArgumentAttribute : Attribute
{
    public InterpolatedStringHandlerArgumentAttribute(string argument)
    {
        Arguments = new[] { argument };
    }

    public InterpolatedStringHandlerArgumentAttribute(params string[] arguments)
    {
        Arguments = arguments;
    }

    public string[] Arguments { get; }
}
