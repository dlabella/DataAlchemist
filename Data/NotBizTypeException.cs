using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class NotBizTypeException:Exception
    {
        public NotBizTypeException(Type t) : base("The Type [" + t.Name + "] is not a subclass of BizObject") { }
    }
}
