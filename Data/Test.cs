using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class Test
    {
        public static TestClass GenericBuilder(IDataRecord reader, IEnumerable<Tuple<int, string, Type, bool>> properties)
        {
            Type typeT = typeof(TestClass);
            var obj = new TestClass();
            var p = properties.FirstOrDefault();

            if (!reader.IsDBNull(p.Item1))
            {
                obj.Nombre = reader[p.Item1];
            }


            return obj;
        }
    }

    public class TestClass
    {
        public object Nombre { get; set; }
        public object Apellidos { get; set; }
    }
}
