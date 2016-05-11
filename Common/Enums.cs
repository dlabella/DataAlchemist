using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public  class Enums
    {
        public enum ParameterDirectionEnum
        {
            Input,
            Output,
            Returning
        }
        public enum ParameterTypeEnum
        {
            Clob,
            Blob,
            Varchar,
            Int,
            Decimal,
            DateTime
        }
    }
}
