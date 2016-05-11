using Data;
using Oracle.DataAccess;

namespace DataAlchemist.Test
{
    public class QueryTest 
    {
        using (var cnn = new OracleConnection("[User your connection string]")) //it can be OracleConnection or another object that implements IDbConnection
        {
            // Typed Objects Queries
            
            Console.WriteLine("All Persons");
            Console.WriteLine("");
            foreach(var p in cnn.Query<Person>())
            {
                Console.WriteLine("Id: "+p.Id);
                Console.WriteLine("Name: "+p.Name);
                Console.WriteLine("Age: "+p.Age);
                Console.WriteLine("____________________");
            }
            
            Console.WriteLine("Persons under 40");
            Console.WriteLine("");
            foreach(var p in cnn.Query<Person>("Age<40"))
            {
                Console.WriteLine("Id: "+p.Id);
                Console.WriteLine("Name: "+p.Name);
                Console.WriteLine("Age: "+p.Age);
                Console.WriteLine("____________________");
            }
            
            Console.WriteLine("Only one person, first one");
            Console.WriteLine("");
            foreach(var p in cnn.QuerySingle<Person>())
            {
                Console.WriteLine("Id: "+p.Id);
                Console.WriteLine("Name: "+p.Name);
                Console.WriteLine("Age: "+p.Age);
                Console.WriteLine("____________________");
            }
            
            Console.WriteLine("Only one person, first one under 40");
            Console.WriteLine("");
            foreach(var p in cnn.QuerySingle<Person>("Age<40"))
            {
                Console.WriteLine("Id: "+p.Id);
                Console.WriteLine("Name: "+p.Name);
                Console.WriteLine("Age: "+p.Age);
                Console.WriteLine("____________________");
            }
            
            // Dynamic Object Queries
            
            Console.WriteLine("All Persons");
            foreach(var p in cnn.QueryDynamic("SELECT Id,Name,Age FROM Person"))
            {
                Console.WriteLine("Id: "+p.Id);
                Console.WriteLine("Name: "+p.Name);
                Console.WriteLine("Age: "+p.Age);
                Console.WriteLine("____________________");
            }
            
            Console.WriteLine("Persons under 40");
            foreach(var p in cnn.QueryDynamic("SELECT Id,Name,Age FROM Person WHERE Age<40"))
            {
                Console.WriteLine("Id: "+p.Id);
                Console.WriteLine("Name: "+p.Name);
                Console.WriteLine("Age: "+p.Age);
                Console.WriteLine("____________________");
            }
            
            //ToDo: Implement QuerySingleDynamic!!!
        }
    }
}