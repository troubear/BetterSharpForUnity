using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace BetterSharp.Assersions;

public class Assert
{
    private static string FormatMessage(string method, string expr, string? message)
    {
        if (string.IsNullOrEmpty(message))
        {
            return $"{method}({expr})";
        }

        return $"{method}({expr}): {message}";
    }

    [AssertionMethod]
    [Conditional("UNITY_ASSERTIONS")]
    public static void IsTrue(
        [DoesNotReturnIf(false)] [AssertionCondition(AssertionConditionType.IS_TRUE)]
        bool condition,
        [InterpolatedStringHandlerArgument("condition")]
        ref AssertIsTrueInterpolatedStringHandler message,
        [CallerArgumentExpression("condition")]
        string? conditionExpr = null)
    {
        UnityEngine.Assertions.Assert.IsTrue(
            condition, FormatMessage(nameof(IsTrue), conditionExpr!, message.GetFormattedText()));
    }

    [AssertionMethod]
    [Conditional("UNITY_ASSERTIONS")]
    public static void IsTrue(
        [DoesNotReturnIf(false)] [AssertionCondition(AssertionConditionType.IS_TRUE)]
        bool condition,
        string? message = null,
        [CallerArgumentExpression("condition")]
        string? conditionExpr = null)
    {
        if (condition)
        {
            return;
        }

        UnityEngine.Assertions.Assert.IsTrue(
            condition, FormatMessage(nameof(IsTrue), conditionExpr!, message));
    }

    [AssertionMethod]
    [Conditional("UNITY_ASSERTIONS")]
    public static void IsFalse(
        [DoesNotReturnIf(false)] [AssertionCondition(AssertionConditionType.IS_FALSE)]
        bool condition,
        [InterpolatedStringHandlerArgument("condition")]
        ref AssertIsFalseInterpolatedStringHandler message,
        [CallerArgumentExpression("condition")]
        string? conditionExpr = null)
    {
        UnityEngine.Assertions.Assert.IsFalse(
            condition, FormatMessage(nameof(IsFalse), conditionExpr!, message.GetFormattedText()));
    }

    [AssertionMethod]
    [Conditional("UNITY_ASSERTIONS")]
    public static void IsFalse(
        [DoesNotReturnIf(false)] [AssertionCondition(AssertionConditionType.IS_FALSE)]
        bool condition,
        string? message = null,
        [CallerArgumentExpression("condition")]
        string? conditionExpr = null)
    {
        if (!condition)
        {
            return;
        }

        UnityEngine.Assertions.Assert.IsFalse(
            condition, FormatMessage(nameof(IsFalse), conditionExpr!, message));
    }

    [AssertionMethod]
    [Conditional("UNITY_ASSERTIONS")]
    public static void IsNotNull<T>(
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
        T? value,
        [InterpolatedStringHandlerArgument("value")]
        ref AssertIsNotNullInterpolatedStringHandler<T> message,
        [CallerArgumentExpression("value")] string? valueExpr = null) where T : class
    {
        UnityEngine.Assertions.Assert.IsNotNull(
            value, FormatMessage(nameof(IsNotNull), valueExpr!, message.GetFormattedText()));
    }

    [AssertionMethod]
    [Conditional("UNITY_ASSERTIONS")]
    public static void IsNotNull<T>(
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
        T? value,
        string? message = null,
        [CallerArgumentExpression("value")] string? valueExpr = null) where T : class
    {
        if (value != null)
        {
            return;
        }

        UnityEngine.Assertions.Assert.IsNotNull(
            value, FormatMessage(nameof(IsNotNull), valueExpr!, message));
    }

    [AssertionMethod]
    [Conditional("UNITY_ASSERTIONS")]
    public static void IsNull<T>(
        [AssertionCondition(AssertionConditionType.IS_NULL)]
        T? value,
        [InterpolatedStringHandlerArgument("value")]
        ref AssertIsNullInterpolatedStringHandler<T> message,
        [CallerArgumentExpression("value")] string? valueExpr = null) where T : class
    {
        UnityEngine.Assertions.Assert.IsNull(
            value, FormatMessage(nameof(IsNull), valueExpr!, message.GetFormattedText()));
    }

    [AssertionMethod]
    [Conditional("UNITY_ASSERTIONS")]
    public static void IsNull<T>(
        [AssertionCondition(AssertionConditionType.IS_NULL)]
        T? value,
        string? message = null,
        [CallerArgumentExpression("value")] string? valueExpr = null) where T : class
    {
        if (value == null)
        {
            return;
        }

        UnityEngine.Assertions.Assert.IsNull(
            value, FormatMessage(nameof(IsNull), valueExpr!, message));
    }
}
