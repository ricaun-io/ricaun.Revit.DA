using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using ricaun.Revit.DA.Extensions;
using System;

namespace ricaun.Revit.DA.Example.Revit
{
    public class App : DesignApplication<DesignAutomation>
    {
        public override bool UseConsoleLog => true;
    }

    public class DesignAutomation : IDesignAutomation
    {
        public bool Execute(Application application, string filePath, Document document)
        {
            var output = RevitOutputModel.Create(application);
            output.Save();

            Console.WriteLine(output.ToJson());

            return true;
        }
    }
}