using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.ExpressionEngine
{
    // Enum to hold filter operators. The chars 
    // are converted to their integer values.
    public enum FilterOperator
    {
        EqualTo = '=',
        LessThan = '<',
        GreaterThan = '>',
        NotEqualTo = '!',
        None = ' '
    }
}
