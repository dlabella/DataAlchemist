using System.Collections.Generic;
using System.Linq;
using Biz.Data;
using System.Data;
using Dapper;

namespace OrmBenchmark.Benchmarks
{
    public class DapperBenchmark//:IOrmBenchmark
    {
        public string BechmarkName
        {
            get
            {
                return "Dapper Benchmark";
            }
        }
        public Workitem QuerySingle(IDbConnection cnn)
        {
            var items = cnn.Query<Workitem>("SELECT * FROM GEMMA_OWN.WORKITEM WHERE ROWNUM=1");
            return items.FirstOrDefault();
        }
        public dynamic QuerySingleDynamic(IDbConnection cnn)
        {
            var items = cnn.Query("SELECT * FROM GEMMA_OWN.WORKITEM WHERE ROWNUM=1");
            return items.FirstOrDefault();
        }

        public List<Workitem> Query100(IDbConnection cnn)
        {
            List<Workitem> item;
            item = cnn.Query<Workitem>("SELECT * FROM GEMMA_OWN.WORKITEM WHERE ROWNUM<100").ToList();
            return item;
        }
        public List<dynamic> QueryDynamic100(IDbConnection cnn)
        {
            var items =  cnn.Query("SELECT * FROM GEMMA_OWN.WORKITEM WHERE ROWNUM<100").ToList();
            return items;
        }

        public List<Workitem> QueryFull(IDbConnection cnn)
        {
            List<Workitem> item;
            item = cnn.Query<Workitem>("SELECT * FROM GEMMA_OWN.WORKITEM WHERE ROWNUM<31465").ToList();
            return item;
        }
        public List<dynamic> QueryDynamicFull(IDbConnection cnn)
        {
            var items = cnn.Query("SELECT * FROM GEMMA_OWN.WORKITEM WHERE VISIBLE='S'").ToList();
            return items;
        }
    }
}
