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
        [TestCase("2024", "4.7", "19", null)]
        [TestCase("2025", "4.7", "19", null)]
        public async Task ExecuteDesignAutomation(string engine, string frameworkNameContain, string referenceContain, string addInNameContain)
        {
            var bundlePaths = GetBundles();
            if (!bundlePaths.Any())
                Assert.Ignore("No bundle found!");

            var bundlePath = GetFullPath(bundlePaths.First());
            var output = await RevitDesignAutomationUtils.Run(bundlePath, engine, true);

            Console.WriteLine(output.ToJson());

            Assert.IsTrue(output.VersionName.Contains(engine), $"VersionName contains engine {engine}");
            Assert.IsTrue(output.FrameworkName.Contains(frameworkNameContain), $"FrameworkName contains framework {frameworkNameContain}");
            Assert.IsTrue(output.Reference.Contains(referenceContain), $"Reference contains reference {referenceContain}");

            if (output.AddInName is not null)
                Assert.IsTrue(output.AddInName.Contains(addInNameContain), $"AddinName contains addin {addInNameContain}");
            else
                Assert.AreEqual(addInNameContain, output.AddInName);
        }
    }
}