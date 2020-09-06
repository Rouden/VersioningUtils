using System.IO;
using Xunit;
using Xunit.Abstractions;
using System.Threading.Tasks;
using Versioning.Utlis;

// ルートにあるはずのファイルを確認する
namespace VersioningUtilsSample
{
    public class BasicFilesChecker
    {
        private readonly ITestOutputHelper output;
        public BasicFilesChecker(ITestOutputHelper helper)
        {
            output = helper;
        }

        // README.md の存在確認
        [Fact]
        public async Task HasReadme()
        {
            var path = await VersioningUtils.GetRepositoryRoot();
            Assert.True(File.Exists(@$"{path}/README.md"));
        }

        // LICENSE.txt の存在確認
        [Fact]
        public async Task HasLicense()
        {
            var path = await VersioningUtils.GetRepositoryRoot();
            Assert.True(File.Exists(@$"{path}/LICENSE.txt"));
        }

        // .gitignore の存在確認
        [Fact]
        public async Task HasGitignore()
        {
            var path = await VersioningUtils.GetRepositoryRoot();
            Assert.True(File.Exists(@$"{path}/.gitignore"));
        }

        // .editorconfig の存在確認
        [Fact]
        public async Task HasEditorconfig()
        {
            var path = await VersioningUtils.GetRepositoryRoot();
            Assert.True(File.Exists(@$"{path}/.editorconfig"));
        }

        // プロジェクトにあわせてファイルのチェックを追加すると良いかもしれない
        // 例: .gitattributes

    }
}
