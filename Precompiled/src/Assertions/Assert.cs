using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace BetterSharp.Assertions;

public sealed class BetterAssertionException : Exception
{
    public BetterAssertionException(Assert.ConditionType conditionType, string conditionExpr, string? userMessage)
    {
        ConditionType = conditionType;
        ConditionExpr = conditionExpr;
        UserMessage = userMessage;
    }

    public Assert.ConditionType ConditionType { get; }
    public string ConditionExpr { get; }
    public string? UserMessage { get; }

    public override string Message => Assert.FormatMessage(ConditionType, ConditionExpr, UserMessage);
}

public static class Assert
{
    public delegate void UnityAssertIsTrueDelegate(bool condition, string message);

    public enum ConditionType
    {
        IsTrue,
        IsFalse
    }

    private const string ConditionString = "UNITY_ASSERTIONS";

    private static bool s_areUnityAssertionMethodsSet;
    private static UnityAssertIsTrueDelegate? s_unityAssertIsTrue;
    private static UnityAssertIsTrueDelegate? s_unityAssertIsFalse;

    public static void SetUnityAssertionMethods(UnityAssertIsTrueDelegate isTrue, UnityAssertIsTrueDelegate isFalse)
    {
        s_areUnityAssertionMethodsSet = true;
        s_unityAssertIsTrue = isTrue;
        s_unityAssertIsFalse = isFalse;
    }

    public static string FormatMessage(ConditionType conditionType, string conditionExpr, string? userMessage)
    {
        if (string.IsNullOrEmpty(userMessage))
        {
            return $"{conditionType}({conditionExpr})";
        }

        return $"{conditionType}({conditionExpr}): {userMessage}";
    }

    private static void Fail(ConditionType conditionType, string conditionExpr, string? userMessage)
    {
        if (s_areUnityAssertionMethodsSet)
        {
            switch (conditionType)
            {
                case ConditionType.IsTrue:
                    s_unityAssertIsTrue!(false, FormatMessage(conditionType, conditionExpr, userMessage));
                    break;
                case ConditionType.IsFalse:
                    s_unityAssertIsFalse!(true, FormatMessage(conditionType, conditionExpr, userMessage));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(conditionType), conditionType, null);
            }
        }

        throw new BetterAssertionException(conditionType, conditionExpr, userMessage);
    }

    [Conditional(ConditionString)]
    [AssertionMethod]
    [ContractAnnotation("condition:false=>halt")]
    public static void IsTrue(
        [DoesNotReturnIf(false)] [AssertionCondition(AssertionConditionType.IS_TRUE)]
        bool condition,
        [InterpolatedStringHandlerArgument("condition")]
        ref AssertIsTrueInterpolatedStringHandler message,
        [CallerArgumentExpression("condition")]
        string? conditionExpr = null)
    {
        if (condition)
        {
            return;
        }

        Fail(ConditionType.IsTrue, conditionExpr!, message.GetFormattedText());
    }

    [Conditional(ConditionString)]
    [AssertionMethod]
    [ContractAnnotation("condition:false=>halt")]
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

        Fail(ConditionType.IsTrue, conditionExpr!, message);
    }

    [Conditional(ConditionString)]
    [AssertionMethod]
    [ContractAnnotation("condition:true=>halt")]
    public static void IsFalse(
        [DoesNotReturnIf(true)] [AssertionCondition(AssertionConditionType.IS_FALSE)]
        bool condition,
        [InterpolatedStringHandlerArgument("condition")]
        ref AssertIsFalseInterpolatedStringHandler message,
        [CallerArgumentExpression("condition")]
        string? conditionExpr = null)
    {
        if (!condition)
        {
            return;
        }

        Fail(ConditionType.IsFalse, conditionExpr!, message.GetFormattedText());
    }

    [Conditional(ConditionString)]
    [AssertionMethod]
    [ContractAnnotation("condition:true=>halt")]
    public static void IsFalse(
        [DoesNotReturnIf(true)] [AssertionCondition(AssertionConditionType.IS_FALSE)]
        bool condition,
        string? message = null,
        [CallerArgumentExpression("condition")]
        string? conditionExpr = null)
    {
        if (!condition)
        {
            return;
        }

        Fail(ConditionType.IsFalse, conditionExpr!, message);
    }
}
