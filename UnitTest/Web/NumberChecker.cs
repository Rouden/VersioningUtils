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
    public class NumberChecker
    {
        private readonly ITestOutputHelper output;
        public NumberChecker(ITestOutputHelper helper)
        {
            output = helper;
        }

        // 通常は Number 関数より parseInt か parseFloat の方が可読性が良い
        [Fact]
        public async Task UseOfNumber()
        {
            var files = await Utils.GetVersionedFiles(Utils.jstsExts);
            var reg = new Regex(@"Number\(");
            var failedList = new List<string>();
            foreach (var path in files)
            {
                // 例外
                //if (path.Contains("something")) continue;

                var text = File.ReadAllText(path, Encoding.UTF8);
                if (reg.IsMatch(text))
                {
                    failedList.Add(path);
                }
            }
            Assert.True(0 == failedList.Count(), $"可読性のため Number 関数の代わりに、parseInt か parseFloat の使用を検討してください。\n{String.Join('\n', failedList)}");
        }
    }
}
