using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Build;

namespace BetterSharp.Editor;

internal class CsprojPostprocessor : AssetPostprocessor
{
    private const int DefaultLangVersion = 9_0;
    private const int AvailableLangVersion =
#if UNITY_2022_3_OR_NEWER
#if UNITY_2022_3_0 || UNITY_2022_3_1 || UNITY_2022_3_2 || UNITY_2022_3_3 || UNITY_2022_3_4 || UNITY_2022_3_5 || UNITY_2022_3_6 || UNITY_2022_3_7 || UNITY_2022_3_8 || UNITY_2022_3_9 || UNITY_2022_3_10 || UNITY_2022_3_11
        10_0;
#else // UNITY_2022_3_12_OR_NEWER
        11_0;
#endif
#else // !UNITY_2022_3_OR_NEWER
        DefaultLangVersion;
#endif
    private const string cscrspFileName = "csc.rsp";

    private static readonly Regex s_cscrspIncludePattern =
        new($@"<None Include=""(.*?)\\{cscrspFileName}"" />", RegexOptions.Compiled);

    private static readonly Regex s_langVersionPattern =
        new(@"<LangVersion>(.*?)</LangVersion>", RegexOptions.Compiled);

    private static int s_defaultLangVersion = DefaultLangVersion;

    private static bool OnPreGeneratingCSProjectFiles()
    {
        var buildTarget = EditorUserBuildSettings.activeBuildTarget;
        var buildTargetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);
        var namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);
        var arguments = PlayerSettings.GetAdditionalCompilerArguments(namedBuildTarget)
            .Select(arg => arg.Trim()).ToArray();
        if (TryGetLangVersionFromCompilerArguments(arguments, out var langVersion))
        {
            s_defaultLangVersion = langVersion;
        }

        return false;
    }

    private static string OnGeneratedCSProject(string path, string content)
    {
#if UNITY_2022_3_OR_NEWER
        var langVersion = GuessAssemblyLangVersion(content);
        content = ReplaceLangVersion(content, langVersion);
#endif
        return content;
    }

    private static bool TryGetLangVersionFromCompilerArguments(IReadOnlyList<string> arguments, out int langVersion)
    {
        langVersion = 0;

        foreach (var argument in arguments)
        {
            const string OptionPrefix = "-langVersion:";
            if (argument.Trim().StartsWith(OptionPrefix, StringComparison.OrdinalIgnoreCase))
            {
                var versionString = argument.Substring(OptionPrefix.Length);
                if (double.TryParse(versionString, out var version))
                {
                    langVersion = (int)(version * 10);
                }
                else
                {
                    // "default", "preview"
                    langVersion = AvailableLangVersion;
                }

                return true;
            }
        }

        return false;
    }

    private static int GuessAssemblyLangVersion(string content)
    {
        var match = s_cscrspIncludePattern.Match(content);
        if (!match.Success)
        {
            return s_defaultLangVersion;
        }

        var cscrspPath = Path.Combine(match.Groups[1].Value, cscrspFileName);
        if (File.Exists(cscrspPath))
        {
            var arguments = File.ReadAllText(cscrspPath).Split(' ');
            if (TryGetLangVersionFromCompilerArguments(arguments, out var langVersion))
            {
                return langVersion;
            }
        }

        return s_defaultLangVersion;
    }

    private static string ReplaceLangVersion(string content, int version)
    {
        var versionString = $"{version / 10}";
        var minorVersion = version % 10;
        if (minorVersion != 0)
        {
            versionString += ".{minorVersion}";
        }

        var replacement = $"<LangVersion>{versionString}</LangVersion>";
        return s_langVersionPattern.Replace(content, replacement);
    }
}
