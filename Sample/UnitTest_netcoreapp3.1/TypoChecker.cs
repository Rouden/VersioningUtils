using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Versioning.Utils;
using Xunit;
#if NET8_0_OR_GREATER
#else
using Xunit.Abstractions;
#endif


namespace VersioningUtilsSample
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
            typoDictionary.Add(new Regex("Utli", RegexOptions.IgnoreCase), "Util");

            var paths = new List<string>();
            foreach (var path in await VersioningUtils.GetVersionedFiles(UtilsChecker.textExts))
            {
                var text = File.ReadAllText(path);
                foreach (var pair in typoDictionary)
                {
                    // 例外
                    if (path.EndsWith("TypoChecker.cs")) continue; // このファイル
                    if (pair.Value == "Util" && path.EndsWith("CHANGELOG.md")) continue; // ドキュメント内に昔の typo についての記載がある

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
