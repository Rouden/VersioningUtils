using System;
using System.Linq;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using Versioning.Utils;

// このファイルは VersioningUtils が内部で利用する関数の動作確認用です。
namespace VersioningUtilsSample
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
            var path = await VersioningUtils.GetRepositoryRoot();
            output.WriteLine(@$"path = {path}");

            // パスが正しければ README.md と .gitignore が存在するはず
            Assert.True(File.Exists(@$"{path}/README.md"), "README.md not found");
            Assert.True(File.Exists(@$"{path}/.gitignore"), ".gitignore not found");
        }

        // バージョン管理されたファイルの一覧が正しく取得できているか確認する
        [Fact]
        public async Task GetVersionedFilesChecker()
        {
            var paths = await VersioningUtils.GetVersionedFiles();
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
            var paths = await VersioningUtils.GetVersionedFiles(new string[] { ".md" });

            output.WriteLine(String.Join("\n", paths));

            foreach (var path in paths)
            {
                Assert.True(File.Exists(path), $"Listed file does not exist.\npath = {path}");
            }

            Assert.True(1 <= paths.Where(text => text.EndsWith("README.md")).Count(), "README.md not found.");
            Assert.True(0 == paths.Where(text => text.EndsWith(".gitignore")).Count(), "extension is not filtered detected.");
        }

        // 拡張子のまとめ
        internal static string[] cppExts = new string[] { ".h", ".hpp", ".c", ".cpp" };
        internal static string[] csExts = new string[] { ".cs" };
        internal static string[] jstsExts = new string[] { ".js", ".ts", ".jsx", ".tsx" };
        internal static string[] webExts = jstsExts.Union(new string[] { ".htm", ".html", ".css" }).ToArray();
        internal static string[] generalExts = new string[] { ".md", ".txt", ".xml" };
        internal static string[] textExts = cppExts.Union(cppExts).Union(csExts).Union(webExts).Union(generalExts).ToArray();
    }
}
