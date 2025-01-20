using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using System;

namespace ricaun.Revit.DA
{
    /// <summary>
    /// Represents a design application that executes a specific type of design automation.
    /// </summary>
    /// <typeparam name="T">The type of design automation to execute.</typeparam>
    public abstract class DesignApplication<T> : DesignApplication where T : IDesignAutomation
    {
        /// <summary>
        /// Executes the design automation of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="application">The Revit application.</param>
        /// <param name="filePath">The file path to the document.</param>
        /// <param name="document">The Revit document.</param>
        /// <returns>True if the execution is successful; otherwise, false.</returns>
        public override bool Execute(Application application, string filePath, Document document)
        {
            return Activator.CreateInstance<T>().Execute(application, filePath, document);
        }
    }
}
