
![CI](https://github.com/Rouden/XUnitPattern/workflows/CI/badge.svg)

> Note: このファイルは書きかけです。

# VersioningUtils

バージョン管理下にあるファイルの一覧を取得します。

単体テストで、リポジトリ内のファイルを全チェックするときに便利です。

## 使い方
> todo: NuGet から取り込んで関数を実行するところまで

## 動作環境
* .NET Core 3.x が動作する環境
    * 動作確認済み
        * Windows 10 + Visual Studio 2019
        * Windows 10 + VS Code
        * GitHub actions (Ubuntu-latest)  
* 対応バージョン管理システム
    * git
        * git コマンドが利用可能な環境、もしくは [GitHub Desktop](https://desktop.github.com/) のみインストールされた環境
    * svn
        * svn コマンドが利用可能な環境
            * TortoiseSVN では、インストールオプションで "command line client tools" が選択されているときに相当
        * svnのみ、パスが非ASCII文字を含んでいるとテスト対象になりません
