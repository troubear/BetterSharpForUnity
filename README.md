# BetterSharpForUnity

BetterSharpForUnity provides a foundation for developing with C#10 or C#11 in Unity.

## Caution!

This library is currently under development and subject to destructive changes.

## Supported Unity versions

Unity 2022.3 or newer

## How to Install

Add the following line to manifest.json

```json
"com.github-troubear.bettersharpforunity": "https://github.com/troubear/BetterSharpForUnity.git?path=Packages/BetterSharpForUnity",
```

Or, install from `Package Manager` -> `Add package from git URL...`

## How to use the new C#

### C#10

#### Apply to the entire project:

`ProjectSettings` -> `Player` -> `Additional Compiler Arguments` -> Add `-langVersion:10`

<img width="373" alt="image" src="https://github.com/troubear/BetterSharpForUnity/assets/21675144/2c66e15d-8eea-4632-921d-5828ab20b771">

#### Applies to the specified assembly:

1. Create `csc.rsp` in the same folder as `*.asmdef`
2. Add `-langVersion:10` to `csc.rsp`

### C#11 (2022.3.12+)

Just change `-langVersion:10` to `-langVersion:preview`.
