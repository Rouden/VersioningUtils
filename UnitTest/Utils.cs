using System;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;

namespace XUnitPattern
{
    internal static class Utils
    {
        // リポジトリのルートディレクトリを得る
        internal static async Task<string> GetRepositoryRoot()
        {
            // git rev-parse --show-toplevel コマンドでルートディレクトリを取得する
            string workingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly()!.Location)!;
            var psi = new ProcessStartInfo("git", "rev-parse --show-toplevel");
            psi.StandardOutputEncoding = Encoding.UTF8;
            psi.WorkingDirectory = workingDirectory;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            var p = Process.Start(psi);
            string output = await p.StandardOutput.ReadToEndAsync();
            return output.Trim('\n');
        }

        // バージョン管理されたファイルの一覧をフルパスで返す
        internal static async Task<string[]> GetVersionedFiles()
        {
            // git ls-files コマンドでファイルの一覧を取得する
            var root = await GetRepositoryRoot();
            var psi = new ProcessStartInfo("git", "ls-files -z");
            psi.StandardOutputEncoding = Encoding.UTF8;
            psi.WorkingDirectory = root;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            var p = Process.Start(psi);
            string output = await p.StandardOutput.ReadToEndAsync();

            // 一覧をフルパスの配列にして返す
            var files = output.Trim('\0').Split('\0');
            return files.Select(v => $"{root}/{v}").ToArray();
        }

        // バージョン管理されたファイルで、指定の拡張子のファイルの一覧をフルパスで返す
        internal static async Task<string[]> GetVersionedFiles(string[] exts)
        {
            var list = await GetVersionedFiles();
            return list.Where(v => exts.Contains(Path.GetExtension(v))).ToArray();
        }

        // 拡張子のまとめ
        internal static string[] cppExts = new string[] { ".h", ".hpp", ".c", ".cpp" };
        internal static string[] csExts = new string[] { ".cs" };
        internal static string[] jsExts = new string[] { ".js", ".ts", ".jsx", ".tsx" };
        internal static string[] webExts = jsExts.Union(new string[] { ".htm", ".html", ".css" }).ToArray();
        internal static string[] generalExts = new string[] {".md", ".txt", ".xml"};
        internal static string[] textExts = cppExts.Union(cppExts).Union(csExts).Union(webExts).Union(generalExts).ToArray();
    }
}
