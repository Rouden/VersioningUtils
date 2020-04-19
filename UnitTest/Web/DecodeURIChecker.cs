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
    public class DecodeUriChecker
    {
        private readonly ITestOutputHelper output;
        public DecodeUriChecker(ITestOutputHelper helper)
        {
            output = helper;
        }

        // decodeURI はエンコードが限定的なので、代わりに decodeURIComponent を使う。
        [Fact]
        public async Task UseDecodeUri()
        {
            var files = await Utils.GetVersionedFiles(Utils.jstsExts);
            var reg = new Regex(@"decodeURI", RegexOptions.IgnoreCase);
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
            Assert.True(0 == failedList.Count(), $"decodeURI 関数はエンコードが限定的なので、代わりに decodeURIComponent 関数を検討してください。\n{String.Join('\n', failedList)}");
        }
    }
}
