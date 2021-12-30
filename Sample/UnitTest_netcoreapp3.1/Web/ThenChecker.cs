using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;
using Versioning.Utils;

namespace VersioningUtilsSample
{
    public class ThenChecker
    {
        private readonly ITestOutputHelper output;
        public ThenChecker(ITestOutputHelper helper)
        {
            output = helper;
        }

        // 非同期処理は then関数, catch関数の代わりに、async, await (と try..catch...) を使う
        [Fact]
        public async Task UseOfThen()
        {
            var files = await VersioningUtils.GetVersionedFiles(UtilsChecker.jstsExts);
            var reg = new Regex(@"\.then\(");
            var failedList = new List<string>();
            foreach (var path in files)
            {
                // 例外
                if (path.EndsWith("reportWebVitals.ts")) continue; // React が生成したファイル

                var text = File.ReadAllText(path, Encoding.UTF8);
                if (reg.IsMatch(text))
                {
                    failedList.Add(path);
                }
            }
            Assert.True(0 == failedList.Count(), $"非同期処理は then関数, catch関数の代わりに、async, await (と try..catch...) を使用してください。\n{String.Join('\n', failedList)}");
        }
    }
}
