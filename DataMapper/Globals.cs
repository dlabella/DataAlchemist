using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public static class Globals
    {
        public static IDbConnection CreateConnection()
        {
            return new SQLiteConnection("Data Source=db.sqlite;Version=3;");
        }
    }
}
