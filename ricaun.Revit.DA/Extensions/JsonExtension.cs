using System;

namespace ricaun.Revit.DA.Extensions
{
    /// <summary>
    /// Provides extension methods for JSON serialization and deserialization.
    /// </summary>
    public static class JsonExtension
    {
        /// <summary>
        /// Serializes the specified object to a JSON string.
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="value">The object to serialize.</param>
        /// <returns>A JSON string representation of the object, or null if the object is null.</returns>
        public static string ToJson<T>(this T value)
        {
            if (value is null)
                return null;
            if (value is string valueString)
                return valueString;

            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Deserializes the specified JSON string to an object of type T.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="value">The JSON string to deserialize.</param>
        /// <returns>An object of type T deserialized from the JSON string.</returns>
        public static T FromJson<T>(this string value)
        {
            if (value is T t)
                return t;

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
        }
    }
}
