// #define STRIP_BETTER_ASSERT

#if !UNITY_ASSERTIONS
#define STRIP_BETTER_ASSERT
#endif

// ReSharper disable RedundantUsingDirective
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using static BetterSharp.BetterSharpInternal;
using UnityAssert = UnityEngine.Assertions.Assert;

namespace BetterSharp.Assertions;

/// <inheritdoc cref="UnityEngine.Assertions.Assert" />
public static class Assert
{
    private static string FormatMessage(string method, string expr, string? message)
    {
        if (string.IsNullOrEmpty(message))
        {
            return $"{method}({expr})";
        }

        return $"{method}({expr}): {message}";
    }

    /// <inheritdoc cref="UnityEngine.Assertions.Assert.IsTrue(bool, string)" />
    [AssertionMethod]
#if STRIP_BETTER_ASSERT
    [Conditional(NeverDefinedSymbol)]
#endif
    public static void IsTrue(
        [DoesNotReturnIf(false)] [AssertionCondition(AssertionConditionType.IS_TRUE)]
        bool condition,
        [InterpolatedStringHandlerArgument("condition")]
        ref AssertIsTrueInterpolatedStringHandler message,
        [CallerArgumentExpression("condition")]
        string? conditionExpr = null)
    {
#if !STRIP_BETTER_ASSERT
        if (condition)
        {
            return;
        }

        UnityAssert.IsTrue(
            condition, FormatMessage(nameof(IsTrue), conditionExpr!, message.GetFormattedText()));
#endif
    }

    /// <inheritdoc cref="UnityEngine.Assertions.Assert.IsTrue(bool, string)" />
    [AssertionMethod]
#if STRIP_BETTER_ASSERT
    [Conditional(NeverDefinedSymbol)]
#endif
    public static void IsTrue(
        [DoesNotReturnIf(false)] [AssertionCondition(AssertionConditionType.IS_TRUE)]
        bool condition,
        string? message = null,
        [CallerArgumentExpression("condition")]
        string? conditionExpr = null)
    {
#if !STRIP_BETTER_ASSERT
        if (condition)
        {
            return;
        }

        UnityAssert.IsTrue(
            condition, FormatMessage(nameof(IsTrue), conditionExpr!, message));
#endif
    }

    /// <inheritdoc cref="UnityEngine.Assertions.Assert.IsFalse(bool, string)" />
    [AssertionMethod]
#if STRIP_BETTER_ASSERT
    [Conditional(NeverDefinedSymbol)]
#endif
    public static void IsFalse(
        [DoesNotReturnIf(false)] [AssertionCondition(AssertionConditionType.IS_FALSE)]
        bool condition,
        [InterpolatedStringHandlerArgument("condition")]
        ref AssertIsFalseInterpolatedStringHandler message,
        [CallerArgumentExpression("condition")]
        string? conditionExpr = null)
    {
#if !STRIP_BETTER_ASSERT
        if (!condition)
        {
            return;
        }

        UnityAssert.IsFalse(
            condition, FormatMessage(nameof(IsFalse), conditionExpr!, message.GetFormattedText()));
#endif
    }

    /// <inheritdoc cref="UnityEngine.Assertions.Assert.IsFalse(bool, string)" />
    [AssertionMethod]
#if STRIP_BETTER_ASSERT
    [Conditional(NeverDefinedSymbol)]
#endif
    public static void IsFalse(
        [DoesNotReturnIf(false)] [AssertionCondition(AssertionConditionType.IS_FALSE)]
        bool condition,
        string? message = null,
        [CallerArgumentExpression("condition")]
        string? conditionExpr = null)
    {
#if !STRIP_BETTER_ASSERT
        if (!condition)
        {
            return;
        }

        UnityAssert.IsFalse(
            condition, FormatMessage(nameof(IsFalse), conditionExpr!, message));
#endif
    }

    /// <inheritdoc cref="UnityEngine.Assertions.Assert.IsNull(UnityEngine.Object, string)" />
    [AssertionMethod]
#if STRIP_BETTER_ASSERT
    [Conditional(NeverDefinedSymbol)]
#endif
    public static void IsNull<T>(
        [AssertionCondition(AssertionConditionType.IS_NULL)]
        T? value,
        [InterpolatedStringHandlerArgument("value")]
        ref AssertIsNullInterpolatedStringHandler<T> message,
        [CallerArgumentExpression("value")] string? valueExpr = null) where T : class
    {
#if !STRIP_BETTER_ASSERT
        if (BetterSharpInternal.IsNull(value))
        {
            return;
        }

        UnityAssert.IsNull(
            value, FormatMessage(nameof(IsNull), valueExpr!, message.GetFormattedText()));
#endif
    }

    /// <inheritdoc cref="UnityEngine.Assertions.Assert.IsNull(UnityEngine.Object, string)" />
    [AssertionMethod]
#if STRIP_BETTER_ASSERT
    [Conditional(NeverDefinedSymbol)]
#endif
    public static void IsNull<T>(
        [AssertionCondition(AssertionConditionType.IS_NULL)]
        T? value,
        string? message = null,
        [CallerArgumentExpression("value")] string? valueExpr = null) where T : class
    {
#if !STRIP_BETTER_ASSERT
        if (BetterSharpInternal.IsNull(value))
        {
            return;
        }

        UnityAssert.IsNull(
            value, FormatMessage(nameof(IsNull), valueExpr!, message));
#endif
    }

    /// <inheritdoc cref="UnityEngine.Assertions.Assert.IsNotNull(UnityEngine.Object, string)" />
    [AssertionMethod]
#if STRIP_BETTER_ASSERT
    [Conditional(NeverDefinedSymbol)]
#endif
    public static void IsNotNull<T>(
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
        T? value,
        [InterpolatedStringHandlerArgument("value")]
        ref AssertIsNotNullInterpolatedStringHandler<T> message,
        [CallerArgumentExpression("value")] string? valueExpr = null) where T : class
    {
#if !STRIP_BETTER_ASSERT
        if (BetterSharpInternal.IsNotNull(value))
        {
            return;
        }

        UnityAssert.IsNotNull(
            value, FormatMessage(nameof(IsNotNull), valueExpr!, message.GetFormattedText()));
#endif
    }

    /// <inheritdoc cref="UnityEngine.Assertions.Assert.IsNotNull(UnityEngine.Object, string)" />
    [AssertionMethod]
#if STRIP_BETTER_ASSERT
    [Conditional(NeverDefinedSymbol)]
#endif
    public static void IsNotNull<T>(
        [AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
        T? value,
        string? message = null,
        [CallerArgumentExpression("value")] string? valueExpr = null) where T : class
    {
#if !STRIP_BETTER_ASSERT
        if (BetterSharpInternal.IsNotNull(value))
        {
            return;
        }

        UnityAssert.IsNotNull(
            value, FormatMessage(nameof(IsNotNull), valueExpr!, message));
#endif
    }
}
