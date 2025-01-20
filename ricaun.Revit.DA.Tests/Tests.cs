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
        //[TestCase("2024", "4.7", "19", null)]
        //[TestCase("2025", "4.7", "19", null)]
        [TestCase("2019", "4.7", "19", "Example")]
        //[TestCase("2020", "4.7", "19", "Example")]
        [TestCase("2024", "4.8", "21", "Example")]
        [TestCase("2025", "8.0", "25", "Example")]
        public async Task ExecuteDesignAutomation(string engine, string frameworkNameContain, string referenceContain, string addInNameContain)
        {
            var bundlePaths = GetBundles();
            if (!bundlePaths.Any())
                Assert.Ignore("No bundle found!");

            var bundlePath = GetFullPath(bundlePaths.First());
            var output = await RevitDesignAutomationUtils.Run(bundlePath, engine, true);

            Console.WriteLine(output.ToJson());

            var versionNameContain = engine;

            Assert.IsTrue(output.VersionName.Contains(versionNameContain), $"VersionName {output.VersionName} not contains engine {versionNameContain}");
            Assert.IsTrue(output.FrameworkName.Contains(frameworkNameContain), $"FrameworkName {output.FrameworkName} not contains framework {frameworkNameContain}");
            Assert.IsTrue(output.Reference.Contains(referenceContain), $"Reference {output.Reference} not contains reference {referenceContain}");

            if (output.AddInName is not null)
                Assert.IsTrue(output.AddInName.Contains(addInNameContain), $"AddinName {output.AddInName} not contains addin {addInNameContain}");
            else
                Assert.AreEqual(addInNameContain, output.AddInName);
        }
    }
}