using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;

namespace XUnitPattern
{
    public class DoctypeChecker
    {
        private readonly ITestOutputHelper output;
        public DoctypeChecker(ITestOutputHelper helper)
        {
            output = helper;
        }

        // 文書型宣言が HTML5 のものであることを確認する
        [Fact]
        public async Task UseHtml5Doctype()
        {
            var files = await Utils.GetVersionedFiles(new string[] { ".htm", ".html" });
            var reg = new Regex(@"^<!DOCTYPE html>", RegexOptions.IgnoreCase);
            var failedList = new List<string>();
            foreach (var path in files)
            {
                // 例外
                //if (path.Contains("something")) continue;

                var text = File.ReadAllText(path, Encoding.UTF8);
                if (!reg.IsMatch(text))
                {
                    failedList.Add(path);
                }
            }
            Assert.True(0 == failedList.Count(), $"文書型宣言がHTML5のものでない .htm ファイルが見つかりました。HTMLではファイルの先頭に「<!DOCTYPE html>」を指定してください。\n{String.Join('\n', failedList)}");
        }
    }
}
