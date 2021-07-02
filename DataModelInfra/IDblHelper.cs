using System.Collections.Generic;
using System.Data;

namespace DataModelInfrastructure
{
    public interface IDblHelper
    {
        IDataReader ExecuteDataReader(string commandText, CommandType commandType, IEnumerable<IDataParameter> commandParameters = null);

        int ExecuteNonQuery(string commandText);

        string GetConnectionString();
    }
}