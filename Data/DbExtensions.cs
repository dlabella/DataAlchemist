using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Data
{
    public static class DbExtensions
    {
        public async static Task<IDataReader> ExecuteReaderAsync(this IDbCommand self)
        {
            var dbCommand = self as DbCommand;
            if (dbCommand != null)
            {
                return await dbCommand.ExecuteReaderAsync();
            }
            else
            {
                return await Task.Run(() => self.ExecuteReader());
            }
        }
    }
}
