using Biz.Data;
using OrmBenchmark.Benchmarks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrmBenchmark
{
    class Program
    {
        static string connectionStr = "Data Source={0};User Id={1};Password={2};";
        static void Main(string[] args)
        {
            Console.WriteLine("Benchmark Start ...");
            //Dtramos item;
            //Dtramos itemDynamic;
            //List<Dtramos> items;
            //List<Dtramos> itemsDynamic;
            var cnn = new Oracle.ManagedDataAccess.Client.OracleConnection(GetConnectionString("GEMMAATOS", "GEMMA_OWN", "GEMMA_OWN2014"));
            cnn.Open();
            cnn.Close();
            Stopwatch sw = new Stopwatch();
            cnn.Open();

            //var dapperBM = new DapperBenchmark();
            //Console.WriteLine("BENCHMARK: " + dapperBM.BechmarkName);
            //Console.WriteLine();
            //sw.Reset();

            //for (var i = 0; i < 5; i++)
            //{
            //    sw.Start();
            //    item = dapperBM.QuerySingle(cnn);
            //    sw.Stop();
            //    log("Single, Iteration[" + i + "]", sw);
            //}

            //sw.Start();
            //items = dapperBM.Query100(cnn);
            //sw.Stop();
            //log("Multiple, Count: [" + items.Count + "]", sw);

            //sw.Start();
            //items = dapperBM.QueryFull(cnn);
            //sw.Stop();
            //log("Multiple, Count: [" + items.Count + "]", sw);

            var dataBM = new DapperBenchmark();
            Console.WriteLine("BENCHMARK: " + dataBM.BechmarkName);
            Console.WriteLine();
            sw.Reset();

            //for (var i = 0; i < 5; i++)
            //{
            //    sw.Start();
            //    item = dataBM.QuerySingle(cnn);
            //    sw.Stop();
            //    log("Single, Iteration[" + i + "]", sw);
            //}

            sw.Start();
            var ditems = dataBM.QueryFull(cnn);
            sw.Stop();
            log("Multiple, Count: [" + ditems.Count + "]", sw);


            var dataBM2 = new DataBenchmark();
            Console.WriteLine("BENCHMARK: " + dataBM.BechmarkName);
            Console.WriteLine();
            sw.Reset();

            //for (var i = 0; i < 5; i++)
            //{
            //    sw.Start();
            //    item = dataBM.QuerySingle(cnn);
            //    sw.Stop();
            //    log("Single, Iteration[" + i + "]", sw);
            //}

            sw.Start();
            var ditems2 = dataBM2.QueryFull(cnn);
            sw.Stop();
            log("Multiple, Count: [" + ditems2.Count + "]", sw);

            sw.Start();
            ditems2 = dataBM2.QueryFull(cnn);
            sw.Stop();
            log("Multiple, Count: [" + ditems2.Count + "]", sw);
            //sw.Start();
            //ditems2 = dataBM2.QueryFull(cnn);
            //sw.Stop();
            //log("Multiple, Count: [" + ditems2.Count + "]", sw);
            //sw.Start();
            //items = dataBM.QueryFull(cnn);
            //sw.Stop();
            //log("Multiple, Count: [" + items.Count + "]", sw);




            cnn.Close();
            Console.ReadKey();

        }
        public static void log(string benchmark, Stopwatch sw)
        {
            Console.WriteLine(benchmark + " Elapsed: " + sw.ElapsedMilliseconds + "/ms");
            sw.Reset();
        }

        public static string GetConnectionString(string db, string user, string pwd)
        {
            return string.Format(connectionStr, db, user, pwd);
        }
    }
}
