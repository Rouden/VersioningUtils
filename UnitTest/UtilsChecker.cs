using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;

// このファイルは XUnitPattern が内部で利用する関数の動作確認用です。
namespace XUnitPattern
{
    public class UtilsChecker
    {
        private readonly ITestOutputHelper output;
        public UtilsChecker(ITestOutputHelper helper)
        {
            output = helper;
        }

        // リポジトリルートのパスが正しく取得できているか確認する. 
        [Fact]
        public async Task GetRepositoryRootChecker()
        {
            var path = await Utils.GetRepositoryRoot();
            output.WriteLine(@$"path = {path}");

            // パスが正しければ README.md と .gitignore が存在するはず
            Assert.True(File.Exists(@$"{path}/README.md"), "README.md not found");
            Assert.True(File.Exists(@$"{path}/.gitignore"), ".gitignore not found");
        }

        // バージョン管理されたファイルの一覧が正しく取得できているか確認する
        [Fact]
        public async Task GetVersionedFilesChecker()
        {
            var paths = await Utils.GetVersionedFiles();
            output.WriteLine(String.Join("\n", paths));

            foreach(var path in paths)
            {
                Assert.True(File.Exists(path), $"Listed file does not exist.\npath = {path}");
            }

            Assert.True(1 <= paths.Where(text => text.EndsWith("README.md")).Count(), "README.md not found.");
        }

        // 上記の拡張子指定バージョンで正しく取得できているか確認する
        [Fact]
        public async Task GetVersionedFilesChecker2()
        {
            var paths = await Utils.GetVersionedFiles(new string[] { ".md" });

            output.WriteLine(String.Join("\n", paths));

            foreach (var path in paths)
            {
                Assert.True(File.Exists(path), $"Listed file does not exist.\npath = {path}");
            }

            Assert.True(1 <= paths.Where(text => text.EndsWith("README.md")).Count(), "README.md not found.");
            Assert.True(0 == paths.Where(text => text.EndsWith(".gitignore")).Count(), "extension is not filtered detected.");
        }
    }
}
