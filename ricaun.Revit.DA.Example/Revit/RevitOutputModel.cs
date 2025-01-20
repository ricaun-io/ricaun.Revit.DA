using Autodesk.Revit.ApplicationServices;
using ricaun.Revit.DA.Example.Models;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;

namespace ricaun.Revit.DA.Example.Revit
{
    public static class RevitOutputModel
    {
        public static OutputModel Create(Application application)
        {
            var outputModel = new OutputModel();
            outputModel.AddInName = application.ActiveAddInId?.GetAddInName();
            outputModel.VersionName = application.VersionName;
            outputModel.VersionBuild = application.VersionBuild;
            outputModel.Reference = outputModel.GetType().Assembly.GetReferencedAssemblies().FirstOrDefault(e => e.Name.Contains("RevitAPI"))?.Version.ToString();
            outputModel.FrameworkName = outputModel.GetType().Assembly.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;
            return outputModel;
        }
    }
}