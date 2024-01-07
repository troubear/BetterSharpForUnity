using UnityEngine;

namespace BetterSharp;

public static class BetterSharpInternal
{
    public const string NeverDefinedSymbol = "MONAFUWA_SWEET_SYMBOL";

    public static bool IsNull<T>(T? value) where T : class
    {
        if (IsUnityObject<T>.Value)
        {
            return !(bool)(value as Object);
        }

        return value == null;
    }

    public static bool IsNotNull<T>(T? value) where T : class
    {
        if (IsUnityObject<T>.Value)
        {
            return (bool)(value as Object);
        }

        return value != null;
    }

    public static class IsUnityObject<T> where T : class
    {
        public static readonly bool Value = typeof(Object).IsAssignableFrom(typeof(T));
    }
}
