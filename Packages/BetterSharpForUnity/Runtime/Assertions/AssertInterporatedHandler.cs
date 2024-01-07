using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using static BetterSharp.BetterSharpInternal;

namespace BetterSharp.Assertions;

internal readonly struct AssertInterpolatedStringHandler
{
    private readonly StringBuilder? _builder;

    public AssertInterpolatedStringHandler(int literalLength, bool condition, out bool shouldAppend)
    {
        if (!condition)
        {
            _builder = new StringBuilder(literalLength);
            shouldAppend = true;
        }
        else
        {
            _builder = default;
            shouldAppend = false;
        }
    }

    public void AppendLiteral(string s)
    {
        _builder!.Append(s);
    }

    public void AppendFormatted<T>(T t)
    {
        _builder!.Append(t);
    }

    internal string GetFormattedText()
    {
        return _builder!.ToString();
    }
}

[InterpolatedStringHandler]
public readonly ref struct AssertIsTrueInterpolatedStringHandler
{
    private readonly AssertInterpolatedStringHandler _builder;

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public AssertIsTrueInterpolatedStringHandler(
        int literalLength, int formattedCount, bool condition, out bool shouldAppend)
    {
        _builder = new AssertInterpolatedStringHandler(literalLength, condition, out shouldAppend);
    }

    public void AppendLiteral(string s)
    {
        _builder.AppendLiteral(s);
    }

    public void AppendFormatted<T>(T t)
    {
        _builder.AppendFormatted(t);
    }

    internal string GetFormattedText()
    {
        return _builder.GetFormattedText();
    }
}

[InterpolatedStringHandler]
public readonly ref struct AssertIsFalseInterpolatedStringHandler
{
    private readonly AssertInterpolatedStringHandler _builder;

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public AssertIsFalseInterpolatedStringHandler(
        int literalLength, int formattedCount, bool condition, out bool shouldAppend)
    {
        _builder = new AssertInterpolatedStringHandler(literalLength, !condition, out shouldAppend);
    }

    public void AppendLiteral(string s)
    {
        _builder.AppendLiteral(s);
    }

    public void AppendFormatted<T>(T t)
    {
        _builder.AppendFormatted(t);
    }

    internal string GetFormattedText()
    {
        return _builder.GetFormattedText();
    }
}

[InterpolatedStringHandler]
public readonly ref struct AssertIsNullInterpolatedStringHandler<TValue> where TValue : class
{
    private readonly AssertInterpolatedStringHandler _builder;

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public AssertIsNullInterpolatedStringHandler(
        int literalLength, int formattedCount, TValue? value, out bool shouldAppend)
    {
        var condition = IsNull(value);
        _builder = new AssertInterpolatedStringHandler(literalLength, condition, out shouldAppend);
    }

    public void AppendLiteral(string s)
    {
        _builder.AppendLiteral(s);
    }

    public void AppendFormatted<T>(T t)
    {
        _builder.AppendFormatted(t);
    }

    internal string GetFormattedText()
    {
        return _builder.GetFormattedText();
    }
}

[InterpolatedStringHandler]
public readonly ref struct AssertIsNotNullInterpolatedStringHandler<TValue> where TValue : class
{
    private readonly AssertInterpolatedStringHandler _builder;

    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public AssertIsNotNullInterpolatedStringHandler(
        int literalLength, int formattedCount, TValue? value, out bool shouldAppend)
    {
        var condition = IsNotNull(value);
        _builder = new AssertInterpolatedStringHandler(literalLength, condition, out shouldAppend);
    }

    public void AppendLiteral(string s)
    {
        _builder.AppendLiteral(s);
    }

    public void AppendFormatted<T>(T t)
    {
        _builder.AppendFormatted(t);
    }

    internal string GetFormattedText()
    {
        return _builder.GetFormattedText();
    }
}
