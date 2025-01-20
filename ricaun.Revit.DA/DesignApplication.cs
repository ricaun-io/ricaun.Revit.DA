using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using DesignAutomationFramework;
using ricaun.Revit.DA.ExternalServer;
using ricaun.Revit.DA.Loader;
using System;

namespace ricaun.Revit.DA
{
    public abstract class DesignApplication<T> : DesignApplication where T : IDesignAutomation
    {
        public override bool Execute(Application application, string filePath, Document document)
        {
            return Activator.CreateInstance<T>().Execute(application, filePath, document);
        }
    }

    public abstract class DesignApplication : IExternalDBApplication, IDesignAutomation
    {
        /// <summary>
        /// Use ExternalService to execute the IDesignAutomation.Execute, this make the Execute run in the AddIn Context.
        /// </summary>
        public virtual bool UseExternalService => true;
        /// <summary>
        /// Use DesignApplicationLoader to load the correct version of the DesignApplication based in the `PackageContents.xml` configuration.
        /// </summary>
        public virtual bool UseDesignApplicationLoader => true;
        public ControlledApplication ControlledApplication { get; private set; }
        public virtual void OnStartup() { }
        public virtual void OnShutdown() { }
        public abstract bool Execute(Application application, string filePath, Document document);

        private IExternalDBApplication designApplication;
        private DesignAutomationSingleExternalServer externalServer;
        public ExternalDBApplicationResult OnStartup(ControlledApplication application)
        {
            ControlledApplication = application;

            designApplication = this;

            if (UseDesignApplicationLoader)
                designApplication = DesignApplicationLoader.LoadVersion(this);

            if (designApplication is IExternalDBApplication)
            {
                return designApplication.OnStartup(application);
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"FullName: \t{GetType().Assembly.FullName}");
            Console.WriteLine($"AddInName: \t{ControlledApplication.ActiveAddInId?.GetAddInName()}");
            Console.WriteLine("----------------------------------------");

            try
            {
                externalServer = new DesignAutomationSingleExternalServer(this).Register();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DesignAutomationSingleExternalServer: \t{ex.Message}");
            }

            OnStartup();
            DesignAutomationBridge.DesignAutomationReadyEvent += DesignAutomationReadyEvent;

            return ExternalDBApplicationResult.Succeeded;
        }

        public ExternalDBApplicationResult OnShutdown(ControlledApplication application)
        {
            ControlledApplication = application;

            if (designApplication is IExternalDBApplication)
            {
                try
                {
                    return designApplication.OnShutdown(application);
                }
                finally
                {
                    DesignApplicationLoader.Dispose();
                }
            }

            externalServer?.RemoveServer();

            OnShutdown();
            DesignAutomationBridge.DesignAutomationReadyEvent -= DesignAutomationReadyEvent;

            return ExternalDBApplicationResult.Succeeded;
        }


        private void DesignAutomationReadyEvent(object sender, DesignAutomationReadyEventArgs e)
        {
            DesignAutomationBridge.DesignAutomationReadyEvent -= DesignAutomationReadyEvent;

            var data = e.DesignAutomationData;

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"RevitApp: {data.RevitApp} \tFilePath: {data.FilePath} \tRevitDoc: {data.RevitDoc} \tAddInName:{data.RevitApp.ActiveAddInId?.GetAddInName()}");
            Console.WriteLine("--------------------------------------------------");

            if (externalServer is not null && UseExternalService)
            {
                e.Succeeded = externalServer.ExecuteService(data.RevitApp, data.FilePath, data.RevitDoc);
                return;
            }

            e.Succeeded = Execute(data.RevitApp, data.FilePath, data.RevitDoc);
        }
    }
}
