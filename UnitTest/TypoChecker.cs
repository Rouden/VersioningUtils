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
            var typoDictionary = new Dictionary<string, string>();
            typoDictionary.Add("auguast", "august");

            var paths = new List<string>();
            foreach (var path in await Utils.GetVersionedFiles(new string[] { ".cs", ".cpp", ".h" }))
            {

                // 例外
                if (path.EndsWith("TypoChecker.cs")) continue; // このファイル

                var text = File.ReadAllText(path);
                foreach (var typoElement in typoDictionary.Keys)
                {
                    Assert.True(-1 == text.IndexOf(typoElement), $"タイプミスが見つかりました。\nfile: {Path.GetFileName(path)}\n誤: {typoElement}\n正: {typoDictionary[typoElement]}\npath: {path}");
                }
            }
            // 一括でエラーを出せるように
            // typoを正規表現に
        }
    }
}
