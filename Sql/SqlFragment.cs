using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql
{
    public class SqlFragment
    {
        public SqlFragment()
        {

        }
        public SqlFragment(int startIndex,int endIndex, string fragment)
        {
            StartIndex = startIndex;
            EndIndex = endIndex;
            Sql = fragment;
        }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public string Sql { get; set; }
        
    }
}
