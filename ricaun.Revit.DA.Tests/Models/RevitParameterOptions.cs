using Autodesk.Forge.Oss.DesignAutomation.Attributes;
using ricaun.Revit.DA.Example.Models;

namespace ricaun.Revit.DA.Tests
{
    public class RevitParameterOptions
    {
        [ParameterOutput("output.json", Description = "Output file.")]
        public OutputModel Output { get; set; }
    }
}