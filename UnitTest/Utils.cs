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
            string? path;
            path = await GetGitRepositoryRoot();
            if (path != null) return path;
            path = await GetSvnRepositoryRoot();
            if (path != null) return path;
            throw new Exception("The root directory not found.");
        }

        private static async Task<string?> GetGitRepositoryRoot()
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
            p.WaitForExit();
            if (p.ExitCode == 0)
            {
                return output.Trim('\n');
            }
            else
            {
                return null;
            }
        }

        private static async Task<string?> GetSvnRepositoryRoot()
        {
            // svn info --show-item wc-root コマンドでルートディレクトリを取得する
            // svn は WorkingDirectory がバージョン管理外のときにパスを取得できないので、ディレクトリを順にさかのぼって取得できるまで試行する.
            string workingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly()!.Location)!;
            while (true)
            {
                var psi = new ProcessStartInfo("svn", "info --show-item wc-root");
                psi.StandardOutputEncoding = Encoding.UTF8;
                psi.WorkingDirectory = workingDirectory;
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                var p = Process.Start(psi);
                string output = await p.StandardOutput.ReadToEndAsync();
                p.WaitForExit();
                if (p.ExitCode == 0)
                {
                    return output.Replace("\r\n", "");
                }
                else
                {
                    // workingDirectory を1階層上にする. 
                    var parent = Directory.GetParent(workingDirectory);
                    if (parent == null) return null;
                    workingDirectory = parent.FullName;
                }

            }
        }

        // バージョン管理されたファイルの一覧をフルパスで返す
        // svn はディレクトリをバージョン管理できるが、ディレクトリのパスは戻り値に含めない.
        internal static async Task<string[]> GetVersionedFiles()
        {
            string[]? files;
            files = await GetGitVersionedFiles();
            if (files != null) return files;
            files = await GetSvnVersionedFiles();
            if (files != null) return files;

            throw new Exception("Failed to get versioned files.");
        }

        private static async Task<string[]?> GetGitVersionedFiles()
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
            p.WaitForExit();
            if (p.ExitCode != 0) return null;

            // 一覧をフルパスの配列にして返す
            var files = output.Trim('\0').Split('\0');
            return files.Select(v => $"{root}/{v}").ToArray();
        }

        private static async Task<string[]?> GetSvnVersionedFiles()
        {
            // svn list -R コマンドでファイルの一覧を取得する
            var root = await GetRepositoryRoot();
            var psi = new ProcessStartInfo("svn", "list -R");
            psi.StandardOutputEncoding = Encoding.UTF8;
            psi.WorkingDirectory = root;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            var p = Process.Start(psi);
            string output = await p.StandardOutput.ReadToEndAsync();
            p.WaitForExit();
            if (p.ExitCode != 0) return null;

            // 一覧をフルパスの配列にして返す
            var files = output.Split("\r\n");
            return files.Select(v => $"{root}/{v}").Where(v=>!v.EndsWith("/")).ToArray();
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
        internal static string[] jstsExts = new string[] { ".js", ".ts", ".jsx", ".tsx" };
        internal static string[] webExts = jstsExts.Union(new string[] { ".htm", ".html", ".css" }).ToArray();
        internal static string[] generalExts = new string[] {".md", ".txt", ".xml"};
        internal static string[] textExts = cppExts.Union(cppExts).Union(csExts).Union(webExts).Union(generalExts).ToArray();
    }
}
