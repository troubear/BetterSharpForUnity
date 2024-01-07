using System.Runtime.CompilerServices;
using System.Text;
using Object = UnityEngine.Object;

namespace BetterSharp.Assersions;

internal struct AssertInterpolatedStringHandler
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
        _builder?.Append(s);
    }

    public void AppendFormatted<T>(T t)
    {
        _builder?.Append(t);
    }

    internal string GetFormattedText()
    {
        return _builder?.ToString() ?? "";
    }
}

[InterpolatedStringHandler]
public ref struct AssertIsTrueInterpolatedStringHandler
{
    private AssertInterpolatedStringHandler _builder;

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
public ref struct AssertIsFalseInterpolatedStringHandler
{
    private AssertInterpolatedStringHandler _builder;

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

internal static class IsUnityObject<T> where T : class
{
    public static readonly bool Value = typeof(Object).IsAssignableFrom(typeof(T));
}

[InterpolatedStringHandler]
public ref struct AssertIsNullInterpolatedStringHandler<TValue> where TValue : class
{
    private AssertInterpolatedStringHandler _builder;

    public AssertIsNullInterpolatedStringHandler(
        int literalLength, int formattedCount, TValue? value, out bool shouldAppend)
    {
        bool condition;
        if (IsUnityObject<TValue>.Value)
        {
            condition = value as Object == null;
        }
        else
        {
            condition = value == null;
        }

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
public ref struct AssertIsNotNullInterpolatedStringHandler<TValue> where TValue : class
{
    private AssertInterpolatedStringHandler _builder;

    public AssertIsNotNullInterpolatedStringHandler(
        int literalLength, int formattedCount, TValue? value, out bool shouldAppend)
    {
        bool condition;
        if (IsUnityObject<TValue>.Value)
        {
            condition = (bool)(value as Object);
        }
        else
        {
            condition = value != null;
        }

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
