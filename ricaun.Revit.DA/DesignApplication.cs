using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using DesignAutomationFramework;

namespace ricaun.Revit.DA
{
    public abstract class DesignApplication : IExternalDBApplication, IDesignAutomation
    {
        public ControlledApplication ControlledApplication { get; private set; }
        public abstract void OnStartup();
        public abstract void OnShutdown();
        public abstract bool Execute(Application application, string filePath, Document document);
        public ExternalDBApplicationResult OnStartup(ControlledApplication application)
        {
            ControlledApplication = application;
            OnStartup();
            DesignAutomationBridge.DesignAutomationReadyEvent += DesignAutomationReadyEvent;

            return ExternalDBApplicationResult.Succeeded;
        }

        public ExternalDBApplicationResult OnShutdown(ControlledApplication application)
        {
            ControlledApplication = application;
            OnShutdown();
            DesignAutomationBridge.DesignAutomationReadyEvent -= DesignAutomationReadyEvent;

            return ExternalDBApplicationResult.Succeeded;
        }

        private void DesignAutomationReadyEvent(object sender, DesignAutomationReadyEventArgs e)
        {
            DesignAutomationBridge.DesignAutomationReadyEvent -= DesignAutomationReadyEvent;

            var data = e.DesignAutomationData;

            e.Succeeded = Execute(data.RevitApp, data.FilePath, data.RevitDoc);
        }
    }

    public interface IDesignAutomation
    {
        bool Execute(Application application, string filePath, Document document);
    }
}
