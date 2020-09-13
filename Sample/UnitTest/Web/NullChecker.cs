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
    public class NullChecker
    {
        private readonly ITestOutputHelper output;
        public NullChecker(ITestOutputHelper helper)
        {
            output = helper;
        }

        // JavaScript や TypeScript では null ではなく undefined を使う
        [Fact]
        public async Task UseOfNull()
        {
            var files = await VersioningUtils.GetVersionedFiles(UtilsChecker.jstsExts);
            var reg = new Regex(@"null");
            var failedList = new List<string>();
            foreach (var path in files)
            {
                // 例外
                if (path.EndsWith("serviceWorker.ts")) continue; // React が生成したファイル

                var text = File.ReadAllText(path, Encoding.UTF8);
                if (reg.IsMatch(text))
                {
                    failedList.Add(path);
                }
            }
            Assert.True(0 == failedList.Count(), $"JavaScript や TypeScript では、混乱を避けるために null の代わりに undefined を使用してください。\n{String.Join('\n', failedList)}");
        }
    }
}
