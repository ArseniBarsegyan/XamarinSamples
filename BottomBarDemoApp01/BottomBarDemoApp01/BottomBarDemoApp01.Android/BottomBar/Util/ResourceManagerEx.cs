using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BottomBarDemoApp01.Droid.BottomBar.Util
{
    public class ResourceManagerEx
    {
        public static int IdFromTitle(string title, Type type)
        {
            var withoutExtension = Path.GetFileNameWithoutExtension(title);
            return GetId(type, withoutExtension);
        }

        private static int GetId(Type type, string propertyName)
        {
            var fieldInfo = ((IEnumerable<FieldInfo>)type.GetFields()).Select<FieldInfo, FieldInfo>((Func<FieldInfo, FieldInfo>)(p => p)).FirstOrDefault<FieldInfo>((Func<FieldInfo, bool>)(p => p.Name == propertyName));
            if (fieldInfo != (FieldInfo)null)
                return (int)fieldInfo.GetValue((object)type);
            return 0;
        }
    }
}