using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExternalService;

namespace ricaun.Revit.DA.ExternalServer
{
    public class DesignAutomationExternalData : IExternalData
    {
        public Application Application { get; set; }
        public string FilePath { get; set; }
        public Document Document { get; set; }
    }
}