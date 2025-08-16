
![CI](https://github.com/Rouden/XUnitPattern/workflows/CI/badge.svg)

# VersioningUtils

List version controlled file paths. Git and SVN are supported.

Unit tests are the main target of this project. 

## Usage
1. Add NuGet package
```
dotnet add package Versioning.Utils
```
2. Get file paths
```CSharp
string[] paths = await VersioningUtils.GetVersionedFiles();
Console.WriteLine(String.Join("\n", paths));
/*
C:/projects/VersioningUtils/.editorconfig
C:/projects/VersioningUtils/.github/workflows/ci.yml
C:/projects/VersioningUtils/.gitignore
C:/projects/VersioningUtils/LICENSE.txt
...
*/
```

## Supported version control sysytem
* git
* svn
 
## Requirements
* .NET Core 3.0 or later (.NET Standard 2.1)
* Version control sysytem client
    * git command if the target repo is git
    * svn command if the target repo is svn
* Limitation with svn
    * This package ignores files where the path has non ASCII letters.
    * This package ignores missing files in your local workspace.
 
## Tips
* [GitHub Desktop](https://desktop.github.com/) contains git client.
* [TortoiseSVN](https://tortoisesvn.net/) requires an installation option "command line client tools".

## Other Utilities

* `VersioningUtils.FindTrojanLetters(...)`
    * This utility function detects [Unicode characters that your IDE does not show you](https://dev.to/dotnetsafer/rip-copy-and-paste-from-stackoverflow-trojan-source-solution-4p8f).

    ```csharp
    // Sample unit test code:
    var exts = new string[]{".cs"};
    var list = await VersioningUtils.FindTrojanLetters(exts);
    Assert.True(0 == list.Count(), $"Trojan source(s) detected.\n{String.Join("\n----\n", list)}");
    ```
