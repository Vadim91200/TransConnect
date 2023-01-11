using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;

namespace TransConnect
{
    /// <summary>
    /// Provides a method for converting an object to a specified type.
    /// </summary>
    public interface IConvert
    {
        /// <summary>
        /// Converts the specified object to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to convert the object to.</typeparam>
        /// <param name="value">The object to convert.</param>
        /// <returns>The converted object.</returns>
        public static T ConvertTo<T>(object value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}