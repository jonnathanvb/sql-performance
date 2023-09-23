using System.Data;
using System.Data.SqlClient;

namespace Infra.Connection;

public class ContextAdoNet: IDisposable
{
    private SqlCommand _connection;

    private readonly string connectionString;
    private SqlConnection connection;

    public ContextAdoNet(string connectionString)
    {
        this.connectionString = connectionString;
    }

    private SqlConnection GetConnection()
    {
        if (connection == null)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }
        else if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }

        return connection;
    }
    
    public void Dispose()
    {
        if (connection != null && connection.State != ConnectionState.Closed)
        {
            connection.Close();
            connection.Dispose();
        }
    }
}