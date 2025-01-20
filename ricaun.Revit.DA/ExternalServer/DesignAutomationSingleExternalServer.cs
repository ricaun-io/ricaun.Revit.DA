using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExternalService;
using System;
using System.Collections.Generic;

namespace ricaun.Revit.DA.ExternalServer
{
    /// <summary>
    /// DesignAutomationSingleExternalServer is a single server with a default single service.
    /// </summary>
    /// <remarks>
    /// This external server is used to execute IDesignAutomation when DesignAutomationReadyEvent is triggered. 
    /// Because the external server is registered before the Revit finish initialize the executed service run in the same ActiveAddIn when the external service is registered.
    /// Fix the issue that DesignAutomationReadyEvent triggers without the ActiveAddIn context.
    /// </remarks>
    public class DesignAutomationSingleExternalServer : ISingleServerService, IDesignAutomationExternalServer
    {
        private readonly IDesignAutomation designAutomation;
        public DesignAutomationSingleExternalServer(IDesignAutomation designAutomation)
        {
            this.designAutomation = designAutomation;
        }
        public ExternalServiceId ServiceId { get; } = new ExternalServiceId(Guid.NewGuid());
        public Guid ServerId { get; } = Guid.NewGuid();

        #region ExecuteService

        public bool ExecuteService(Application application, string filePath, Document document)
        {
            var externalData = new DesignAutomationExternalData()
            {
                Application = application,
                FilePath = filePath,
                Document = document,
            };
            return ExecuteService(externalData);
        }

        public bool ExecuteService(DesignAutomationExternalData externalData)
        {
            var service = ExternalServiceRegistry.GetService(ServiceId) as SingleServerService;
            var result = ExternalServiceRegistry.ExecuteService(service.GetPublicAccessKey(), externalData);
            Console.WriteLine($"ExecuteService: \t{result} \t{externalData}");
            return result == ExternalServiceResult.Succeeded;
        }

        #endregion

        #region Register/Unregister
        public DesignAutomationSingleExternalServer Register()
        {
            var options = new ExternalServiceOptions()
            {
                IsPublic = true,
            };
            var privateKeyExecute = ExternalServiceRegistry.RegisterService(this, options);
            return this.AddServer();
        }
        public DesignAutomationSingleExternalServer AddServer(IDesignAutomationExternalServer designAutomationExternalServer = null)
        {
            designAutomationExternalServer ??= this;
            var service = ExternalServiceRegistry.GetService(ServiceId) as SingleServerService;
            if (!service.IsRegisteredServerId(designAutomationExternalServer.GetServerId()))
            {
                service.AddServer(designAutomationExternalServer);
                service.SetActiveServer(designAutomationExternalServer.GetServerId());
            }
            return this;
        }
        public DesignAutomationSingleExternalServer RemoveServer()
        {
            var service = ExternalServiceRegistry.GetService(ServiceId) as SingleServerService;
            foreach (var guid in service.GetRegisteredServerIds())
            {
                service.RemoveServer(guid);
            }
            return this;
        }
        #endregion

        #region ISingleServerService
        public bool Execute(IExternalServer server, Document document, IExternalData data)
        {
            if (server is IDesignAutomationExternalServer designAutomationExternalServer)
            {
                return designAutomationExternalServer.Execute(data as DesignAutomationExternalData);
            }
            return false;
        }
        public bool IsValidServer(IExternalServer server)
        {
            return server is IDesignAutomationExternalServer;
        }
        #endregion

        #region IDesignAutomationExternalServer
        public bool Execute(DesignAutomationExternalData data)
        {
            return designAutomation.Execute(data.Application, data.FilePath, data.Document);
        }

        public Guid GetServerId() => ServerId;
        #endregion

        #region IExternalService
        public string GetDescription() => GetType().Name;
        public string GetName() => GetType().Name;
        public ExternalServiceId GetServiceId() => ServiceId;
        public string GetVendorId() => "ricaun";

        /// <summary>
        /// This method may only be invoked in a recordable service. Services registered as non-recordable never receive this call.
        /// </summary>
        public void OnServersChanged(Document document, ServerChangeCause cause, IList<Guid> oldServers) { }

        /// <summary>
        /// This method may only be invoked in a recordable service. Services registered as non-recordable never receive this call.
        /// </summary>
        public DisparityResponse OnServersDisparity(Document document, IList<Guid> oldServers) => DisparityResponse.ApplyDefaults;
        #endregion
    }
}