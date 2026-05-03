
# 本パッケージの開発方法

## リリース前のパッケージをテストする
* Sample フォルダ内のプロジェクトの単体テストを実行すれば良い。ただし、リポジトリ内のファイルは NuGet 上のパッケージを参照しているため、以下のいずれかの方法で、ローカルのパッケージを参照するように変更する
* 方法1
    1. 単体テストプロジェクトファイルを開き、NuGet パッケージの参照をローカルのプロジェクト参照に書き換える。
        ```xml
        <!--PackageReference Include="Versioning.Utils" Version="1.3.0" /-->
        <ProjectReference Include="../../VersioningUtils/VersioningUtils.csproj" />
        ```
    1. 単体テストを実行する
    1. デバッグが終わったらプロジェクトの設定を元に戻す
* 方法2
    1. ローカルに配置した NuGet パッケージを参照できるようにする
        * 参考: https://qiita.com/jugemjugemu/items/39c5e90c9897fda12ccc
    1. VersioningUtils のバージョン番号を `1.2.3-alpha1` のように書き換える
    1. VersioningUtils をビルドする
    1. 「1.」で設定したフォルダにビルドした zip ファイルを配置する
    1. 単体テストプロジェクトのパッケージ参照を更新する
    1. 単体テストを実行する
    1. デバッグが終わったら VersioningUtils のバージョン番号と、単体テストのパッケージ参照の設定を元に戻す

## パッケージをリリースする
