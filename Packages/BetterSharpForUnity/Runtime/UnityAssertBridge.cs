#if UNITY_ASSERTIONS

using BetterSharp.Assertions;
using UnityEngine;
using UnityAssert = UnityEngine.Assertions.Assert;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BetterSharp;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
internal static class UnityAssertBridge
{
    private static bool s_isInitialized;

#if UNITY_EDITOR
    static UnityAssertBridge()
    {
        InitializeOnLoad();
    }
#endif

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void InitializeOnLoad()
    {
        if (s_isInitialized)
        {
            return;
        }

        s_isInitialized = true;

        // Pass methods of the UnityEngine.Assertions.Assert class to the precompiled code.

        // Avoid stripping
        UnityAssert.IsTrue(true);
        UnityAssert.IsFalse(false);

        // Since a method to which ConditionalAttribute is granted cannot be converted to a delegate in the usual way,
        // it is forcibly converted to a delegate using reflection.
        var isTrueMethod = typeof(UnityAssert).GetMethod("IsTrue", new[] { typeof(bool), typeof(string) });
        var isFalseMethod = typeof(UnityAssert).GetMethod("IsFalse", new[] { typeof(bool), typeof(string) });
        UnityAssert.IsNotNull(isTrueMethod);
        UnityAssert.IsNotNull(isFalseMethod);

        var isTrueDelegate = (Assert.UnityAssertIsTrueDelegate)
            isTrueMethod.CreateDelegate(typeof(Assert.UnityAssertIsTrueDelegate));
        var isFalseDelegate = (Assert.UnityAssertIsTrueDelegate)
            isFalseMethod.CreateDelegate(typeof(Assert.UnityAssertIsTrueDelegate));
        UnityAssert.IsNotNull(isTrueDelegate);
        UnityAssert.IsNotNull(isFalseDelegate);

        Assert.SetUnityAssertionMethods(isTrueDelegate, isFalseDelegate);
    }
}

#endif
