using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Data
{
    public static class Globals
    {
        public static int DocumentStartCounter=0;
        public static Action<IDbConnection, BizObject> PostInsertAction;
    }
}
