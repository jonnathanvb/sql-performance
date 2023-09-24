using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

public class ContextDapper : IDisposable
{
    private string _connectionString;

    public ContextDapper(string connectionString)
    {
        _connectionString = connectionString ;
    }
    

    public IDbConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public void Dispose()
    {
    }
}