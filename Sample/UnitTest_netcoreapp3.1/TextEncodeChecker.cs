using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using Versioning.Utils;

namespace VersioningUtilsSample
{
    public class TextEncodeChecker
    {

        private readonly ITestOutputHelper output;
        public TextEncodeChecker(ITestOutputHelper helper)
        {
            output = helper;
        }

        // ソースコードは utf-8 で保存する
        // Shift_jis のようなロケール依存の文字コードで保存すると、環境にもよるが多言語OSでビルドできないことがある
        [Fact]
        public async Task useUTF8()
        {
            var files = await VersioningUtils.GetVersionedFiles(UtilsChecker.textExts);
            var failedFiles = new List<string>();
            foreach (var path in files)
            {
                // 例外
                if (path.Contains("/reactapp/src")) continue; // react-script 経由で確認されるので無視する.

                byte[] bin = File.ReadAllBytes(path);
                if(bin[0] == 0xEF && bin[1] == 0xBB && bin[2] == 0xBF)
                {
                    // ok
                }
                else
                {
                    failedFiles.Add(path);
                }
            }
            Assert.True(0 == failedFiles.Count(), $"UTF-8 でないか、BOMで始まらないファイルが見つかりました。非日本語環境でビルドに成功させるために、ソースコードはBOM付きUTF-8で保存してください。\n{String.Join("\n", failedFiles)}");
        }
    }
}
