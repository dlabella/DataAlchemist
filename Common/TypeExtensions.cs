using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace Common
{
    public static class TypeExtensions
    {
        public static bool IsNullableType(this Type type)
        {
            return (type.IsGenericType &&
                type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }

        public static bool IsConvertable(this Type type,string str)
        {
            if (str == null)
                return type.IsClass || IsNullableType(type);

            object value;
            return ConvertFromString(type, str, out value);
        }

        public static bool ConvertFromString(this Type type, string str, out object value)
        {
            value = null;
            try
            {
                MethodInfo mi = type.GetMethod("FromString", BindingFlags.Public | BindingFlags.Static);
                if (mi != null)
                    value = mi.Invoke(null, new object[] { str });
                else if (type == typeof(DateTime))
                    value = DateTime.Parse(str);
                else if (type.BaseType == typeof(Enum))
                    value = Enum.Parse(type, str);
                else if (IsNullableType(type) )
                {
                    if (str == string.Empty || String.Compare(str, "null", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        value = null;
                    }
                    else
                    {
                        ConstructorInfo[] ci = type.GetConstructors();
                        ParameterInfo[] pi = ci[0].GetParameters();
                        object val = Convert.ChangeType(str, pi[0].ParameterType);
                        value = ci[0].Invoke(new[] {val});
                    }
                }
                else
                    value = Convert.ChangeType(str, type);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static PropertyInfo GetPropertyInfo(this Type type, string propertyName)
        {
            BindingFlags bf = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase;
            PropertyInfo info = type.GetProperty(propertyName, bf);
            if (info != null) return info;

            foreach (Type interfaceType in type.GetInterfaces())
            {
                info = interfaceType.GetProperty(propertyName, bf);
                if (info != null) return info;
            }

            return null;
        }

        public static bool MethodExists(this Type type, string methodName)
        {
            BindingFlags bf = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase;
            MethodInfo[] infos = type.GetMethods(bf);
            foreach (MethodInfo mi in infos)
                if (mi.Name == methodName) return true;
            return false;
        }

        public static MethodInfo GetMethodInfo(this Type type, string methodName, int argsCount)
        {
            BindingFlags bf = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase;
            MethodInfo[] infos = type.GetMethods(bf);
            foreach (MethodInfo mi in infos)
                if (mi.Name == methodName && mi.GetParameters().Length == argsCount) return mi;
            return null;
        }

        public delegate void InvokeHandler();
        public static void SafeInvoke(this System.Windows.Forms.Control control, InvokeHandler handler)
        {
            if (control.InvokeRequired) control.Invoke(handler);
            else handler();
        }

        public static T TryCast<T>(this object obj)
        {
            if (obj is T)
            {
                return (T)obj;
            }

            return default(T);
        }

        //public static void CopyTo(this object source, object target,HashSet<string> excludedProperties=null)
        //{
        //    if (source == null) return;
        //    foreach (var property in source.GetType().GetProperties())
        //    {
        //        if (property.CanRead)
        //        {
        //            var targetProp=target.GetType().GetProperty(property.Name);

        //            if (excludedProperties == null)
        //            {
        //                if (targetProp != null && targetProp.CanWrite )
        //                {
        //                    targetProp.SetValue(target, property.GetValue(source, null), null);
        //                }
        //            }
        //            else
        //            {
        //                if (targetProp != null && targetProp.CanWrite && !excludedProperties.Contains(targetProp.Name))
        //                {
        //                    targetProp.SetValue(target, property.GetValue(source, null), null);
        //                }
        //            }
        //        }
        //    }
        //}
        //public static void Clear(this object source,HashSet<string> excludedProperties=null)
        //{
        //    var init = Activator.CreateInstance(source.GetType());
        //    init.CopyTo(source,excludedProperties);
        //}
    }
    
}
