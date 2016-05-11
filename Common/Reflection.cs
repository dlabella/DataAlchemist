using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Common
{
    public class Reflection
    {
        private static ConcurrentDictionary<Type, MethodInfo> _converterCache = new ConcurrentDictionary<Type, MethodInfo>();
        public delegate void GenericSetter(object target, object value);
        public delegate object GenericGetter(object target);
        
        

        public static GenericSetter CreateSetMethod(PropertyInfo propertyInfo)
        {
            /*
            * If there's no setter return null
            */
            MethodInfo setMethod = propertyInfo.GetSetMethod();
            if (setMethod == null)
                return null;

            /*
            * Create the dynamic method
            */
            var arguments = new Type[2];
            arguments[0] = arguments[1] = typeof(object);

            var setter = new DynamicMethod(
              String.Concat("_Set", propertyInfo.Name, "_"),
              typeof(void), arguments, propertyInfo.DeclaringType);
            ILGenerator generator = setter.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
            generator.Emit(OpCodes.Ldarg_1);

            if (propertyInfo.PropertyType.IsClass)
                generator.Emit(OpCodes.Castclass, propertyInfo.PropertyType);
            else
                generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);

            generator.EmitCall(OpCodes.Callvirt, setMethod, null);
            generator.Emit(OpCodes.Ret);

            /*
            * Create the delegate and return it
            */
            return (GenericSetter)setter.CreateDelegate(typeof(GenericSetter));
        }
        
        //public static GenericSetter CreateSetMethod(PropertyInfo propertyInfo,Type forcedType)
        //{
        //    Convert.toin
        //    /*
        //    * If there's no setter return null
        //    */
        //    MethodInfo setMethod = propertyInfo.GetSetMethod();
        //    if (setMethod == null)
        //        return null;

        //    /*
        //    * Create the dynamic method
        //    */
        //    var arguments = new Type[2];
        //    arguments[0] = arguments[1] = typeof(object);

        //    var setter = new DynamicMethod(
        //      String.Concat("_Set", propertyInfo.Name, "_"),
        //      typeof(void), arguments, propertyInfo.DeclaringType);
        //    ILGenerator generator = setter.GetILGenerator();
        //    generator.Emit(OpCodes.Ldarg_0);
        //    generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
        //    generator.Emit(OpCodes.Ldarg_1);

        //    if (propertyInfo.PropertyType.IsClass)
        //        generator.Emit(OpCodes.Castclass, forcedType);
        //    else
        //        generator.Emit(OpCodes.Unbox_Any, forcedType);

        //    generator.EmitCall(OpCodes.Callvirt, setMethod, null);
        //    generator.Emit(OpCodes.Ret);

        //    /*
        //    * Create the delegate and return it
        //    */
        //    return (GenericSetter)setter.CreateDelegate(typeof(GenericSetter));
        //}

        ///
        /// Creates a dynamic getter for the property
        ///
        public static GenericGetter CreateGetMethod(PropertyInfo propertyInfo)
        {
            /*
            * If there's no getter return null
            */
            MethodInfo getMethod = propertyInfo.GetGetMethod();
            if (getMethod == null)
                return null;

            /*
            * Create the dynamic method
            */
            Type[] arguments = new Type[1];
            arguments[0] = typeof(object);

            var getter = new DynamicMethod(
              String.Concat("_Get", propertyInfo.Name, "_"),
              typeof(object), arguments, propertyInfo.DeclaringType);
            ILGenerator generator = getter.GetILGenerator();
            generator.DeclareLocal(typeof(object));
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
            generator.EmitCall(OpCodes.Callvirt, getMethod, null);

            if (!propertyInfo.PropertyType.IsClass)
                generator.Emit(OpCodes.Box, propertyInfo.PropertyType);

            generator.Emit(OpCodes.Ret);

            /*
            * Create the delegate and return it
            */
            return (GenericGetter)getter.CreateDelegate(typeof(GenericGetter));
        }

        /*public static object ChangeType(object value, Type conversionType)
        {
            if (conversionType == null)
            {
                throw new ArgumentNullException("conversionType");
            }
            if (conversionType.IsGenericType &&
              conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }
                var nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }*/
    }

    
}
