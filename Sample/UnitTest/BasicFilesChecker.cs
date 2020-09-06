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

// ルートにあるはずのファイルを確認する
namespace XUnitPattern
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
            var path = await Utils.GetRepositoryRoot();
            Assert.True(File.Exists(@$"{path}/README.md"));
        }

        // LICENSE.txt の存在確認
        [Fact]
        public async Task HasLicense()
        {
            var path = await Utils.GetRepositoryRoot();
            Assert.True(File.Exists(@$"{path}/LICENSE.txt"));
        }

        // .gitignore の存在確認
        [Fact]
        public async Task HasGitignore()
        {
            var path = await Utils.GetRepositoryRoot();
            Assert.True(File.Exists(@$"{path}/.gitignore"));
        }

        // .editorconfig の存在確認
        [Fact]
        public async Task HasEditorconfig()
        {
            var path = await Utils.GetRepositoryRoot();
            Assert.True(File.Exists(@$"{path}/.editorconfig"));
        }

        // プロジェクトにあわせてファイルのチェックを追加すると良いかもしれない
        // 例: .gitattributes

    }
}
