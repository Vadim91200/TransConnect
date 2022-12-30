using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;

namespace TransConnect
{
    public interface IConvert
    {
        public static T ConvertTo<T>(object value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}