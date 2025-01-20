using Autodesk.Revit.ApplicationServices;
using ricaun.Revit.DA.Extensions;
using System;
using System.IO;

namespace ricaun.Revit.DA.Example.Models
{
    public class OutputModel
    {
        public string AddInName { get; set; }
        public string VersionName { get; set; }
        public string VersionBuild { get; set; }
        public DateTime TimeStart { get; set; } = DateTime.UtcNow;
        public string Reference { get; set; }
        public string FrameworkName { get; set; }
        public void Save(string fileName = "output.json")
        {
            File.WriteAllText(fileName, this.ToJson());
        }
    }
}