
> Note: このドキュメントは書きかけです。

# XUnitPattern
[@rouden](https://twitter.com/rouden) が良く目にする単体テストのパターンをまとめたリポジトリです。

バージョン管理されたファイルを静的解析して、問題を事前に発見することを目的としています。

各テストはファイルに小分けにされています。必要なテストを取り出してお使いください。

## できること
* 一般
    * ソースコードが UTF8 で保存されているかテストする
    * ソースコード内の表記ゆれ (例えば、製品名とか...) の検出
* C#
    * void を返す async 関数の検出
* TypeScript
    * null を使っていない (undefined を使っている) ことをチェックする
    * Number 関数を使っていない (parseInt か parseFloat を使っている) ことをチェックする
* etc...

## とりあえず動かしてみる
1. `git clone https://github.com/Rouden/XUnitPattern.git`
2. `cd .\XUnitPattern\UnitTest\`
3. `dotnet test`

## プロジェクトに組み込む
1. このリポジトリの UnitTest フォルダをコピーして配置する
2. `dotnet test` でテストする
    * github actions や Jenkins などで日常的に実行されるようにしておくとgood！

## 動作環境
* .NET Core 3.x が動作する環境なら大抵は動く...はず
* 動作確認済み
    * Windows 10 + Visual Studio 2019
    * Windows 10 + VS Code
    * GitHub actions (Ubuntu-latest)  

## Todo list
* gitコマンドが利用できない場合(例: GitHub Desktop だけインストールしたような環境)でも、それなりに動くように
* バージョン管理可のプロジェクトファイルの設定確認
