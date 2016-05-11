using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Common.ExpressionEngine
{
    public struct SingleFilterInfo
    {
        public string PropName;
        public PropertyDescriptor PropDesc;
        public Reflection.GenericGetter GetValueOf;
        public Object CompareValue;
        public FilterOperator OperatorValue;
    }
}
