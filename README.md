
![CI](https://github.com/Rouden/XUnitPattern/workflows/CI/badge.svg)

# VersioningUtils

List version controlled file paths. Git and SVN are supported.

Unit test is the biggest target of this project. 

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

## Requirements
* .NET Core 3.0 or later (.NET Standard 2.1)
    * Tested environment
        * Windows 10 + Visual Studio 2019
        * Windows 10 + VS Code
        * GitHub actions (Ubuntu-latest)  
* Supported version control sysytem environment
    * git
        * git command is available or [GitHub Desktop](https://desktop.github.com/) is installed
    * svn
        * svn command is available
            * TortoiseSVN requires an installation option "command line client tools".
        * In svn mode, VersioningUtils ignores files if path has non ASCII letters.
