using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BottomBarDemoApp01.Droid.BottomBar.Util
{
    public class ReflectedProxy<T> where T : class
    {
        private object _target;
        private readonly Dictionary<string, PropertyInfo> _cachedPropertyInfo;
        private readonly Dictionary<string, MethodInfo> _cachedMethodInfo;
        private readonly IEnumerable<PropertyInfo> _targetPropertyInfoList;
        private readonly IEnumerable<MethodInfo> _targetMethodInfoList;

        public ReflectedProxy(T target)
        {
            this._target = (object)target;
            this._cachedPropertyInfo = new Dictionary<string, PropertyInfo>();
            this._cachedMethodInfo = new Dictionary<string, MethodInfo>();
            TypeInfo typeInfo = typeof(T).GetTypeInfo();
            this._targetPropertyInfoList = typeInfo.GetRuntimeProperties();
            this._targetMethodInfoList = typeInfo.GetRuntimeMethods();
        }

        public void SetPropertyValue(object value, [CallerMemberName] string propertyName = "")
        {
            this.GetPropertyInfo(propertyName).SetValue(this._target, value);
        }

        public TPropertyValue GetPropertyValue<TPropertyValue>([CallerMemberName] string propertyName = "")
        {
            return (TPropertyValue)this.GetPropertyInfo(propertyName).GetValue(this._target);
        }

        public object Call([CallerMemberName] string methodName = "", object[] parameters = null)
        {
            if (!this._cachedMethodInfo.ContainsKey(methodName))
                this._cachedMethodInfo[methodName] = this._targetMethodInfoList.Single<MethodInfo>((Func<MethodInfo, bool>)(mi =>
                {
                    if (mi.Name != methodName)
                        return mi.Name.Contains("." + methodName);
                    return true;
                }));
            return this._cachedMethodInfo[methodName].Invoke(this._target, parameters);
        }

        private PropertyInfo GetPropertyInfo(string propertyName)
        {
            if (!this._cachedPropertyInfo.ContainsKey(propertyName))
                this._cachedPropertyInfo[propertyName] = this._targetPropertyInfoList.Single<PropertyInfo>((Func<PropertyInfo, bool>)(pi =>
                {
                    if (pi.Name != propertyName)
                        return pi.Name.Contains("." + propertyName);
                    return true;
                }));
            return this._cachedPropertyInfo[propertyName];
        }
    }
}