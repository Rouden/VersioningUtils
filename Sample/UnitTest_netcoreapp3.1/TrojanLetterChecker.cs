using System;
using System.Linq;
using System.Threading.Tasks;
using Versioning.Utils;
using Xunit;
#if NET8_0_OR_GREATER
#else
using Xunit.Abstractions;
#endif


namespace VersioningUtilsSample
{
    public class TrojanLetterChecker
    {

        private readonly ITestOutputHelper output;
        public TrojanLetterChecker(ITestOutputHelper helper)
        {
            output = helper;
        }

        // 制御文字を利用した悪意のあるコードが StackOverflow 等にある場合それらをコピペするのは危険である. 制御文字を含むソースコードがないか確認する.
        [Fact]
        public async Task CheckTrojanLetter()
        {
            var list = await VersioningUtils.FindTrojanLetters(new string[] { ".cs", ".html", ".htm", ".tsx", "ts", ".css", ".txt", ".md" });
            Assert.True(0 == list.Count(), $"通常利用しない制御文字を含むソースコードが見つかりました。StackOverflow 等にある悪意のあるコードがコピーされた可能性があります。\n{String.Join("\n----\n", list)}");
        }
    }
}
