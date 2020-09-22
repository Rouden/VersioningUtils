
![CI](https://github.com/Rouden/XUnitPattern/workflows/CI/badge.svg)

# VersioningUtils

バージョン管理下にあるファイルの一覧を取得します。Git と SVN 環境で利用できます。

単体テストで、リポジトリ内のファイルを全チェックするときに便利です。

## 使い方
1. NuGet パッケージを追加する
```
dotnet add package Versioning.Utils
```
2. ファイル一覧を取得する
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

## 動作環境
* .NET Core 3.0 以降 (.NET Standard 2.1)
    * 動作確認済み
        * Windows 10 + Visual Studio 2019
        * Windows 10 + VS Code
        * GitHub actions (Ubuntu-latest)  
* 対応バージョン管理システム
    * git
        * git コマンドが利用可能な環境、もしくは [GitHub Desktop](https://desktop.github.com/) のみインストールされた環境
    * svn
        * svn コマンドが利用可能な環境
            * [TortoiseSVN](https://tortoisesvn.net/) では、インストールオプションで "command line client tools" が選択されているときに相当
        * svnのみ、パスが非ASCII文字を含んでいると対象になりません
