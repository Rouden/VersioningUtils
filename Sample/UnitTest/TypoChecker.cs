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
    public class TypoChecker
    {

        private readonly ITestOutputHelper output;
        public TypoChecker(ITestOutputHelper helper)
        {
            output = helper;
        }

        // よくある typo を検出する
        [Fact]
        public async Task CheckTypo()
        {
            // タイプミス / スペルミス辞書 
            // Add(誤, 正) で追加していく
            var typoDictionary = new Dictionary<Regex, string>();
            var failedList = new List<string>();
            typoDictionary.Add(new Regex("auguast", RegexOptions.IgnoreCase), "august");

            var paths = new List<string>();
            foreach (var path in await Utils.GetVersionedFiles(Utils.textExts))
            {

                // 例外
                if (path.EndsWith("TypoChecker.cs")) continue; // このファイル

                var text = File.ReadAllText(path);
                foreach (var pair in typoDictionary)
                {
                    var match = pair.Key.Match(text);
                    if (match.Success)
                    {
                        failedList.Add($"誤: {match.Value}\n正: {pair.Value}\npath: {path}");
                    }
                }
            }
            Assert.True(0 == failedList.Count(), $"タイプミスが見つかりました。\n{String.Join("\n----\n", failedList)}");
        }
    }
}
