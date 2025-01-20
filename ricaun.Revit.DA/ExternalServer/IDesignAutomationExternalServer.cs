using Autodesk.Revit.DB.ExternalService;
using ricaun.Revit.DA.ExternalServer;

namespace ricaun.Revit.DA.ExternalServer
{
    public interface IDesignAutomationExternalServer : IExternalServer
    {
        public bool Execute(DesignAutomationExternalData data);
    }
}