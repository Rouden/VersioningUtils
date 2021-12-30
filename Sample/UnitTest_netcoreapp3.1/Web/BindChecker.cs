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
    public class BindChecker
    {
        private readonly ITestOutputHelper output;
        public BindChecker(ITestOutputHelper helper)
        {
            output = helper;
        }

        // bind 関数によるスコープを
        [Fact]
        public async Task UseOfBind()
        {
            var files = await VersioningUtils.GetVersionedFiles(UtilsChecker.jstsExts);
            var reg = new Regex(@"\.bind\(");
            var failedList = new List<string>();
            foreach (var path in files)
            {
                // 例外
                // if (path.EndsWith("reportWebVitals.ts")) continue; // React が生成したファイル

                var text = File.ReadAllText(path, Encoding.UTF8);
                if (reg.IsMatch(text))
                {
                    failedList.Add(path);
                }
            }
            Assert.True(0 == failedList.Count(), $"\n{String.Join('\n', failedList)}");
        }
    }
}
