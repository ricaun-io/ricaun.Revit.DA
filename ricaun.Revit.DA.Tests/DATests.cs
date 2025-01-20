using Autodesk.Forge.Oss.DesignAutomation;
using Autodesk.Forge.Oss.DesignAutomation.Extensions;
using Autodesk.Forge.Oss.DesignAutomation.Services;
using NUnit.Framework;
using ricaun.Revit.DA.Tests.Utils;
using System;
using System.Threading.Tasks;

namespace ricaun.Revit.DA.Tests
{
    public class DATests : BundleFileTests
    {
        [TestCaseSource(nameof(GetBundles))]
        public async Task Test(string fileBundlePath)
        {
            Console.WriteLine(fileBundlePath);
            var fullPath = GetFullPath(fileBundlePath);

            IDesignAutomationService designAutomationService = new RevitDesignAutomationService(nameof(DATests))
            {
                EngineVersions = new[] { "2023" },
                EnableConsoleLogger = true,
                EnableParameterConsoleLogger = true,
                EnableReportConsoleLogger = true,
                RunTimeOutMinutes = 2.0,
            };
            try
            {
                await designAutomationService.Initialize(fullPath);
                var options = new RevitParameterOptions();
                var result = await designAutomationService.Run(options);

                Console.WriteLine(options.Output.ToJson());
            }
            finally
            {
                await designAutomationService.Delete();
            }
        }
    }
}