using Autodesk.Forge.Oss.DesignAutomation;
using Autodesk.Forge.Oss.DesignAutomation.Services;
using ricaun.Revit.DA.Example.Models;
using System.Threading.Tasks;

namespace ricaun.Revit.DA.Tests.Utils
{
    public static class RevitDesignAutomationUtils
    {
        public static async Task<OutputModel> Run(string packagePath, string engine, bool consoleEnabled = true)
        {
            IDesignAutomationService designAutomationService = new RevitDesignAutomationService(nameof(RevitDesignAutomationUtils))
            {
                EngineVersions = new[] { engine },
                EnableConsoleLogger = consoleEnabled,
                EnableParameterConsoleLogger = consoleEnabled,
                EnableReportConsoleLogger = consoleEnabled,
                RunTimeOutMinutes = 2.0,
            };
            try
            {
                await designAutomationService.Initialize(packagePath);
                var options = new RevitParameterOptions();
                var result = await designAutomationService.Run(options);
                return options.Output;
            }
            finally
            {
                await designAutomationService.Delete();
            }
        }
    }
}