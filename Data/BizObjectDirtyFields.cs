using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class BizObjectDirtyFields
    {
        public HashSet<string> Items { get; set; }
        public BizObjectDirtyFields()
        {
            Items = new HashSet<string>();
        }
        public BizObjectDirtyFields(IEnumerable<string> dirtyFields)
        {
            Items = new HashSet<string>(dirtyFields);
        }
    }
}
