using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ricaun.Revit.DA.Tests.Utils
{
    public class BundleFileTests
    {
        public static string DirectoryName => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string DirectorySolution { get; } = GetDirectorySolution();
        private static string GetDirectorySolution()
        {
            const string SolutionExtension = ".sln";
            var directory = DirectoryName;
            while (!Directory.GetFiles(directory, $"*{SolutionExtension}").Any())
            {
                directory = Directory.GetParent(directory).FullName;
            }
            return directory;
        }

        public static IEnumerable<string> GetBundles()
        {
            var files = Directory.GetFiles(DirectorySolution, "*.bundle.zip", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                yield return Path.GetFileName(file);
            }
        }

        public static string GetFullPath(string fileName)
        {
            var files = Directory.GetFiles(DirectorySolution, fileName, SearchOption.AllDirectories);
            return files.FirstOrDefault();
        }
    }
}