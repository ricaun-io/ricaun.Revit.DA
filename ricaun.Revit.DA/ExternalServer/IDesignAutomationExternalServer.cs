using Autodesk.Revit.DB.ExternalService;

namespace ricaun.Revit.DA.ExternalServer
{
    /// <summary>
    /// Interface for design automation external server.
    /// </summary>
    internal interface IDesignAutomationExternalServer : IExternalServer
    {
        /// <summary>
        /// Executes the design automation with the provided data.
        /// </summary>
        /// <param name="data">The data required for design automation.</param>
        /// <returns>True if execution is successful, otherwise false.</returns>
        public bool Execute(DesignAutomationExternalData data);
    }
}