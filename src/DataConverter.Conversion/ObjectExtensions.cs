using System.Collections.Generic;

namespace DataConverter.Conversion
{
    public static class ObjectExtensions
    {
        public static Dictionary<string, object> AsDictionary(this object obj)
        {
            return obj as Dictionary<string, object>;
        }
    }
}