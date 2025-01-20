using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExternalService;

namespace ricaun.Revit.DA.ExternalServer
{
    /// <summary>
    /// Represents the external data for design automation in Revit.
    /// </summary>
    internal class DesignAutomationExternalData : IExternalData
    {
        /// <summary>
        /// Gets or sets the Revit application.
        /// </summary>
        public Application Application { get; set; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the Revit document.
        /// </summary>
        public Document Document { get; set; }
    }
}