using Data;
using Oracle.DataAccess;

namespace DataAlchemist.Test
{
    public class QueryTest 
    {
        var p=new Person();
        p.Id=1;
        p.Name="Daniel";
        p.Age=37;
        
        using (var cnn = new OracleConnection("[User your connection string]")) //it can be OracleConnection or another object that implements IDbConnection
        {
            Console.WriteLine("Insert Test");
            var inserts = cnn.Insert<Person>(p);
            if (inserts>0) 
            {
                Console.WriteLine("Insert Test: OK");
                Console.WriteLine("Update Test");
                p.Name="Daniel Labella de la Cruz";
                var updates = cnn.Update<Person>(p);
                if (updates>0) 
                {
                    Console.WriteLine("Update Test: OK");
                    Console.WriteLine("Delete Test");
                    var deletes = cnn.Delete<Person>(p);
                    if (deletes>0)
                    {
                        Console.WriteLine("Delete Test: OK");
                        Console.WriteLine("Tests Completed Ok");
                        return;
                    }
                }
            }
            Console.WriteLine("Ups!, something went wrong...");            
        }
    }
}