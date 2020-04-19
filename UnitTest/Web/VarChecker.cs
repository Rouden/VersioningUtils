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
    public class VarChecker
    {
        private readonly ITestOutputHelper output;
        public VarChecker(ITestOutputHelper helper)
        {
            output = helper;
        }

        // TypeScript では var の代わりに let を使う
        [Fact]
        public async Task UseOfVar()
        {
            var files = await Utils.GetVersionedFiles(new string[] { ".ts", ".tsx" });
            var reg = new Regex(@"[\(\s^{]+var\s+");
            var failedList = new List<string>();
            foreach (var path in files)
            {
                // 例外
                if (path.EndsWith(".d.ts")) continue; // 型定義ファイル

                var text = File.ReadAllText(path, Encoding.UTF8);
                if (reg.IsMatch(text))
                {
                    failedList.Add(path);
                }
            }
            Assert.True(0 == failedList.Count(), $"変数宣言は var ではなく let を使ってください。\n{String.Join('\n', failedList)}");
        }
    }
}
