using Biz.Data;
using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBenchmark
{
    public interface IOrmBenchmark
    {
        string BechmarkName { get; }
        Afectado QuerySingle(IDbConnection cnn);
        T QuerySingleDynamic<T>(IDbConnection cnn);
        List<Afectado> Query100(IDbConnection cnn);
        List<T> QueryDynamic100<T>(IDbConnection cnn);
        List<Afectado> QueryFull(IDbConnection cnn);
        List<T> QueryDynamicFull<T>(IDbConnection cnn);
    }

}
