
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

## サポートしているバージョン管理システム
* git
* svn

## 動作環境
* .NET Core 3.0 以降 (.NET Standard 2.1)
* 対応バージョン管理システム
    * git コマンドが利用可能な環境
    * svn コマンドが利用可能な環境
* svn 利用時の制限事項
    * 非ASCII文字を含むパスは無視されます
    * 作業コピーのファイルが存在しない場合は無視されます

## Tips
* [GitHub Desktop](https://desktop.github.com/) に同梱される git クライアントに対応
* [TortoiseSVN](https://tortoisesvn.net/) では、インストールオプションで "command line client tools" が選択されているときに svn コマンドが利用可能になります。

## おまけ関数

* `VersioningUtils.FindTrojanLetters(...)`
    * この関数は [Unicodeの制御文字を利用した悪意のあるソースコード](https://qiita.com/rana_kualu/items/3b03961deb003a8a2f1d) を検出します.

    ```csharp
    // 以下は、単体テストの実装例です
    var exts = new string[]{".cs"};
    var list = await VersioningUtils.FindTrojanLetters(exts);
    Assert.True(0 == list.Count(), $"不自然な制御文字を含むソースコードが見つかりました。\n{String.Join("\n----\n", list)}");
    ```
