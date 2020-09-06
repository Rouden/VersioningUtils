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
    public class DefaultDesktopOnlyChecker
    {
        private readonly ITestOutputHelper output;
        public DefaultDesktopOnlyChecker(ITestOutputHelper helper)
        {
            output = helper;
        }

        // MessageBoxOptions.DefaultDesktopOnly は使用するべきではない
        // 「常に表示」レイヤーにダイアログを表示すると、親ウインドウに対する操作を抑制できない。ほとんどの場合、これは想定されない動作である。
        [Fact]
        public async Task UseDefaultDesktopOnly()
        {
            var files = await VersioningUtils.GetVersionedFiles(new string[] { ".cs" });
            var reg = new Regex(@"MessageBoxOptions\.DefaultDesktopOnly");
            var failedList = new List<string>();
            foreach (var path in files)
            {
                // 例外
                if (path.Contains("DefaultDesktopOnlyChecker.cs")) continue; // このファイルを除外

                var text = File.ReadAllText(path, Encoding.UTF8);
                if (reg.IsMatch(text))
                {
                    failedList.Add(path);
                }
            }
            Assert.True(0 == failedList.Count(), $"MessageBoxOptions.DefaultDesktopOnly は親ウインドウに対するイベントが遮断されなくなります。ほとんどの場合で、あなたが想定していない動作と思われます。指定を削除してください。 \n{String.Join('\n', failedList)}");
        }

        [Fact]
        public async Task UseServiceNotification()
        {
            var files = await VersioningUtils.GetVersionedFiles(new string[] { ".cs" });
            var reg = new Regex(@"MessageBoxOptions\.ServiceNotification");
            var failedList = new List<string>();
            foreach (var path in files)
            {
                // 例外
                if (path.Contains("DefaultDesktopOnlyChecker.cs")) continue; // このファイルを除外

                var text = File.ReadAllText(path, Encoding.UTF8);
                if (reg.IsMatch(text))
                {
                    failedList.Add(path);
                }
            }
            Assert.True(0 == failedList.Count(), $"MessageBoxOptions.ServiceNotification は親ウインドウに対するイベントが遮断されなくなります。ほとんどの場合で、あなたが想定していない動作と思われます。指定を削除してください。\n{String.Join('\n', failedList)}");
        }
    }
}
