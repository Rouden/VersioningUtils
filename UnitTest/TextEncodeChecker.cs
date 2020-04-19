using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;

namespace XUnitPattern
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
            var files = await Utils.GetVersionedFiles(new string[] { ".cs", ".h", ".cpp", ".md", ".puml" });
            foreach (var path in files)
            {
                // 例外
                // if (path.Contains("something")) continue;

                byte[] bin = File.ReadAllBytes(path);
                if(bin[0] == 0xEF && bin[1] == 0xBB && bin[2] == 0xBF)
                {
                    // ok
                }
                else
                {
                    Assert.True(false, $"UTF-8 でないか、BOMで始まらないファイルが見つかりました。非日本語環境でビルドに成功させるために、ソースコードはBOM付きUTF-8で保存してください。\npath={path}");
                }
            }
        }
    }
}
