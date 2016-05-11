using System.Collections.Generic;
using System.Linq;
using Data;
using Biz.Data;
using System.Data;
using System;
using System.Threading.Tasks;

namespace OrmBenchmark.Benchmarks
{
    public class DataBenchmark
    {
        public string BechmarkName
        {
            get
            {
                return "Data Benchmark";
            }
        }

        public Workitem QuerySingle(IDbConnection cnn)
        {
            Workitem item;
            item = cnn.QuerySingle<Workitem>("ROWNUM=1");
            return item;
        }
        public Workitem QuerySingleDynamic<T>(IDbConnection cnn)
        {
            Workitem item;
            item = cnn.QuerySingleDynamic<Workitem>("SELECT coddtramo FROM GEMMA_OWN.WORKITEM WHERE ROWNUM=1");
            return item;
        }

        public List<Workitem> Query100(IDbConnection cnn)
        {
            List<Workitem> item;
            item = cnn.Query<Workitem>("ROWNUM<100").ToList();
            return item;
        }
        public List<Workitem> QueryDynamic100(IDbConnection cnn)
        {
            //var item = cnn.QueryDynamic("SELECT * FROM SITREM.Dtramos WHERE ROWNUM<100").ToList();
            //return item;
            return cnn.QueryDynamic<Workitem>("SELECT * FROM GEMMA_OWN.WORKITEM WHERE ROWNUM<100").ToList();
        }

        public List<Workitem> QueryFull(IDbConnection cnn)
        {
            List<Workitem> item;
            item = cnn.Query<Workitem>("SELECT * FROM GEMMA_OWN.WORKITEM WHERE ROWNUM<31465").ToList();
            return item;
        }

        public async Task<IList<Workitem>> QueryFullAsync(IDbConnection cnn)
        {
            return await cnn.QueryAsync<Workitem>("SELECT * FROM GEMMA_OWN.WORKITEM WHERE ROWNUM<31465");
        }

        public async Task<IList<BizObject>> QueryDynamicFull(IDbConnection cnn)
        {
            //List<Dtramos> item;
            //item = cnn.QueryDynamic<Dtramos>("SELECT * FROM SITREM.Dtramos WHERE 1=1").ToList();
            //return item;
            return await cnn.QueryDynamicAsync("SELECT * FROM GEMMA_OWN.WORKITEM WHERE VISIBLE='S'");
        }
    }
}
