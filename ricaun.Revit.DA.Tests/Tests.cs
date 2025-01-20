using NUnit.Framework;
using ricaun.Revit.DA.Tests.Utils;
using System;

namespace ricaun.Revit.DA.Tests
{
    public class Tests : BundleFileTests
    {
        [TestCaseSource(nameof(GetBundles))]
        public void Test(string fileBundlePath)
        {
            Console.WriteLine(fileBundlePath);
            var fullPath = GetFullPath(fileBundlePath);
        }
    }
}