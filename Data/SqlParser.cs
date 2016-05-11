using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Data
{
    public class SqlParser
    {
        public SqlParser()
        {

        }

        public string Parse(string sqlText)
        {
            string sql = sqlText.ToLower();
            if (sql.StartsWith("select ") && sql.Contains(" from "))
            {
                return "";
            }
            else
            {
                return "";
            }
        }

        private void ParseSelectColumns(string sqlText)
        {
            string sql=sqlText.ToLower();
            
        } 
    }
}
