using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;

namespace ricaun.Revit.DA
{
    /// <summary>
    /// Interface for design automation tasks in Revit.
    /// </summary>
    public interface IDesignAutomation
    {
        /// <summary>
        /// Executes a design automation task.
        /// </summary>
        /// <param name="application">The Revit application instance.</param>
        /// <param name="filePath">The file path to the document.</param>
        /// <param name="document">The Revit document instance.</param>
        /// <returns>True if the task was executed successfully, otherwise false.</returns>
        bool Execute(Application application, string filePath, Document document);
    }
}
