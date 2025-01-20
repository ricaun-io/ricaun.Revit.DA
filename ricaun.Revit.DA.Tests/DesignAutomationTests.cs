using Autodesk.Forge.Oss.DesignAutomation;
using Autodesk.Forge.Oss.DesignAutomation.Extensions;
using Autodesk.Forge.Oss.DesignAutomation.Services;
using NUnit.Framework;
using ricaun.Revit.DA.Tests.Utils;
using System;
using System.Threading.Tasks;

namespace ricaun.Revit.DA.Tests
{
    public class DesignAutomationTests : BundleFileTests
    {
        [TestCaseSource(nameof(GetBundles))]
        public void Test(string fileBundlePath)
        {
            Console.WriteLine(fileBundlePath);
            var fullPath = GetFullPath(fileBundlePath);
        }

        [TestCaseSource(nameof(GetBundles))]
        public async Task Execute(string fileBundlePath)
        {
            var fullPath = GetFullPath(fileBundlePath);
            var output = await RevitDesignAutomationUtils.Run(fullPath, "2023");
            Console.WriteLine(output.ToJson());
        }
    }
}