using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using ricaun.Revit.DA.Example.Models;
using ricaun.Revit.DA.Extensions;
using System;
using System.IO;

namespace ricaun.Revit.DA.Example.Revit
{
    public class App : DesignApplication
    {
        public override bool Execute(Application application, string filePath, Document document)
        {
            Console.WriteLine($"Execute: \t{application}");
            Console.WriteLine($"ActiveAddInId: \t{application.ActiveAddInId?.GetAddInName()}");

            var output = RevitOutputModel.Create(application);
            output.Save();

            return true;
        }

        public override void OnStartup()
        {
            Console.WriteLine($"ActiveAddInId: \t{Application.ActiveAddInId?.GetAddInName()}");
        }

        public override void OnShutdown()
        {

        }
    }
}