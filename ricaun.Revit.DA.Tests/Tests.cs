using Autodesk.Forge.Oss.DesignAutomation.Extensions;
using NUnit.Framework;
using ricaun.Revit.DA.Tests.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ricaun.Revit.DA.Tests
{
    public class Tests : BundleFileTests
    {
        [TestCase("2024", "4.7", "19")]
        [TestCase("2025", "4.7", "19")]
        public async Task ExecuteDesignAutomation(string engine, string frameworkName, string reference)
        {
            var bundlePaths = GetBundles();
            if (!bundlePaths.Any())
                Assert.Ignore("No bundle found!");

            var bundlePath = GetFullPath(bundlePaths.First());
            var output = await RevitDesignAutomationUtils.Run(bundlePath, engine, true);

            Console.WriteLine(output.ToJson());

            Assert.IsTrue(output.VersionName.Contains(engine), $"VersionName contains engine {engine}");
            Assert.IsTrue(output.FrameworkName.Contains(frameworkName), $"FrameworkName contains framework {frameworkName}");
            Assert.IsTrue(output.Reference.Contains(reference), $"Reference contains reference {reference}");
        }
    }
}