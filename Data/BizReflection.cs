using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class BizReflection
    {
        public delegate T ToTDelegate<T>(IDataReader reader);
        public static ToTDelegate<T> BizInstanceBuilder<T>(IDataReader reader, IEnumerable<ReaderFieldProperties> properties)
        {
            Type[] methodArgs = { typeof(IDataReader) };
            var dm = new DynamicMethod("MapDatareader", typeof(T), methodArgs, typeof(T));

            var il = dm.GetILGenerator();
            il.DeclareLocal(typeof(T));

            il.Emit(OpCodes.Newobj, typeof(T).GetConstructors()[0]);
            il.Emit(OpCodes.Stloc_0);

            var getItemMethod = reader.GetType().GetMethod("get_Item", new Type[] { typeof(int) });
            var isDbNullMethod = typeof(IDataRecord).GetMethod("IsDBNull", new Type[] { typeof(int) });
            foreach (var info in properties)
            {
                var endIfLabel = il.DefineLabel();

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldc_I4, info.ColumnIndex);
                il.Emit(OpCodes.Callvirt, isDbNullMethod);
                il.Emit(OpCodes.Brtrue, endIfLabel);

                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldc_I4, info.ColumnIndex);
                il.Emit(OpCodes.Callvirt, getItemMethod);
                il.Emit(OpCodes.Nop);
                Type dataType;
                if (info.PropertyInfo.Type.IsNullableType())
                {
                    dataType = Nullable.GetUnderlyingType(info.PropertyInfo.Type);
                }
                else
                {
                    dataType = info.PropertyInfo.Type;
                }
                /*if (dataType == typeof(decimal) || dataType == typeof(Decimal))
                {
                    il.Emit(OpCodes.Call, typeof(Converter).GetMethod("ToDecimal", new Type[] { typeof(object) }));
                }
                else if (dataType == typeof(DateTime))
                {
                    il.Emit(OpCodes.Call, typeof(Converter).GetMethod("ToDateTime", new Type[] { typeof(object) }));
                }else
                {
                    il.Emit(OpCodes.Call, typeof(Converter).GetMethod("ToString", new Type[] { typeof(object) }));
                }*/
                il.Emit(OpCodes.Nop);
                il.Emit(OpCodes.Callvirt, typeof(T).GetMethod("set_" + info.PropertyInfo.Name, new Type[] { info.PropertyInfo.Type }));
                il.Emit(OpCodes.Nop);

                il.MarkLabel(endIfLabel);
            }

            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);
            return dm.CreateDelegate(typeof(ToTDelegate<T>)) as ToTDelegate<T>;
        }
    }
    public static class Converter
    {
        public static DateTime? ToDateTime(object value)
        {
            return Convert.ChangeType(value, typeof(DateTime)) as DateTime?;
        }
        public static decimal? ToDecimal(object value)
        {
            return Convert.ChangeType(value, typeof(decimal)) as decimal?;
        }
    }
}
