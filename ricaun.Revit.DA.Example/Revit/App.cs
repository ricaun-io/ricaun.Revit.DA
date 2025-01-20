using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using System;

namespace ricaun.Revit.DA.Example.Revit
{
    public class App : DesignApplication<DesignAutomation>
    {

    }

    public class DesignAutomation : IDesignAutomation
    {
        public bool Execute(Application application, string filePath, Document document)
        {
            var output = RevitOutputModel.Create(application);
            output.Save();
            return true;
        }
    }
}