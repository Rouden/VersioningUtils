using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;
using Versioning.Utlis;

namespace VersioningUtilsSample
{
    public class CodeChecker
    {
        private readonly ITestOutputHelper output;
        public CodeChecker(ITestOutputHelper helper)
        {
            output = helper;
        }

        // C# で async 関数が void を返すと終了タイミングを取得できなくなる。Task か Task<T> を返すべきである
        // 参考: http://neue.cc/2013/10/10_429.html
        [Fact]
        public async Task UseAsyncVoid()
        {
            // async void を許容する関数
            string[] extFunctions = new string[] {
                "OnInspectorGUI", // Unity エディタ拡張
            };

            var files = await VersioningUtils.GetVersionedFiles(new string[] { ".cs" });
            var reg = new Regex($@"\s+async\s+void\s+(?!{String.Join('|', extFunctions)})");
            var failedList = new List<string>();
            foreach (var path in files)
            {
                // 例外
                if (path.EndsWith("AsyncVoidChecker.cs")) continue; // このファイル

                var text = File.ReadAllText(path, Encoding.UTF8);
                if (reg.IsMatch(text))
                {
                    failedList.Add(path);
                }
            }
            Assert.True(0 == failedList.Count(), $"void を返す async 関数が見つかりました。戻り値を Task または Task<T> に変更してください。\n{String.Join('\n', failedList)}");
        }
    }
}
