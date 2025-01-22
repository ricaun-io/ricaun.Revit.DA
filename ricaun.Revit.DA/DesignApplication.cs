using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using DesignAutomationFramework;
using ricaun.Revit.DA.ExternalServer;
using ricaun.Revit.DA.Loader;
using System;

namespace ricaun.Revit.DA
{
    /// <summary>
    /// Represents a design application that executes a specific type of design automation.
    /// </summary>
    public abstract class DesignApplication : IExternalDBApplication, IDesignAutomation
    {
        /// <summary>
        /// Use ExternalService to execute the IDesignAutomation.Execute, this make the Execute run in the AddIn Context.
        /// </summary>
        /// <remarks>The default value is 'true'.</remarks>
        public virtual bool UseExternalService => true;
        /// <summary>
        /// Use DesignApplicationLoader to load the correct version of the DesignApplication based in the `PackageContents.xml` configuration.
        /// </summary>
        /// <remarks>The default value is 'true'.</remarks>
        public virtual bool UseDesignApplicationLoader => true;
        /// <summary>
        /// Gets a value indicating whether to use console logging for the internal <see cref="DesignApplication"/> methods.
        /// </summary>
        /// <remarks>The default value is 'true'.</remarks>
        public virtual bool UseConsoleLog => true;
        /// <summary>
        /// Gets the controlled application.
        /// </summary>
        public ControlledApplication ControlledApplication { get; private set; }
        /// <summary>
        /// Method called when the application starts up.
        /// </summary>
        public virtual void OnStartup() { }
        /// <summary>
        /// Method called when the application shuts down.
        /// </summary>
        public virtual void OnShutdown() { }
        /// <summary>
        /// Executes the design automation.
        /// </summary>
        /// <param name="application">The Revit application.</param>
        /// <param name="filePath">The file path to the document.</param>
        /// <param name="document">The Revit document.</param>
        /// <returns>True if the execution is successful; otherwise, false.</returns>
        public abstract bool Execute(Application application, string filePath, Document document);

        private IExternalDBApplication designApplication;
        private DesignAutomationSingleExternalServer externalServer;

        /// <summary>
        /// Method called when the application starts up.
        /// </summary>
        /// <param name="application">The controlled application.</param>
        /// <returns>The result of the external DB application startup.</returns>
        public ExternalDBApplicationResult OnStartup(ControlledApplication application)
        {
            ControlledApplication = application;

            designApplication = this;

            if (UseDesignApplicationLoader)
            {
                DesignApplicationLoader.LogWriteLine = WriteLine;
                designApplication = DesignApplicationLoader.LoadVersion(this);
            }

            if (designApplication is IExternalDBApplication)
            {
                return designApplication.OnStartup(application);
            }

            WriteLine("----------------------------------------");
            WriteLine($"FullName: \t{GetType().Assembly.FullName}");
            WriteLine($"AddInName: \t{ControlledApplication.ActiveAddInId?.GetAddInName()}");
            WriteLine("----------------------------------------");

            try
            {
                externalServer = new DesignAutomationSingleExternalServer(this).Register();
            }
            catch (Exception ex)
            {
                WriteLine($"DesignAutomationSingleExternalServer: \t{ex.Message}");
            }

            OnStartup();
            DesignAutomationBridge.DesignAutomationReadyEvent += DesignAutomationReadyEvent;

            return ExternalDBApplicationResult.Succeeded;
        }

        /// <summary>
        /// Method called when the application shuts down.
        /// </summary>
        /// <param name="application">The controlled application.</param>
        /// <returns>The result of the external DB application shutdown.</returns>
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

            WriteLine("--------------------------------------------------");
            WriteLine($"RevitApp: {data.RevitApp} \tFilePath: {data.FilePath} \tRevitDoc: {data.RevitDoc} \tAddInName:{data.RevitApp.ActiveAddInId?.GetAddInName()}");
            WriteLine("--------------------------------------------------");

            if (externalServer is not null && UseExternalService)
            {
                e.Succeeded = externalServer.ExecuteService(data.RevitApp, data.FilePath, data.RevitDoc);
                return;
            }

            e.Succeeded = Execute(data.RevitApp, data.FilePath, data.RevitDoc);
        }

        private void WriteLine(string message)
        {
            if (!UseConsoleLog) return;

            Console.WriteLine(message);
        }
    }
}
