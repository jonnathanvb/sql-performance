using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Infra.Connection;
using Infra.Utils;

namespace Infra.Repository;

public abstract class BaseRepositoryAbstract<T> where T : class
{
    protected readonly ContextAdoNet _contextAdoNet;


    public BaseRepositoryAbstract(ContextAdoNet contextAdoNet)
    {
        _contextAdoNet = contextAdoNet;
    }
    
    public void Insert(T entity)
    {
        var commandSql = typeof(T).CommandInsert();
        using SqlConnection connection = _contextAdoNet.GetConnection();
        using SqlCommand command = new SqlCommand(commandSql, connection);
        
        foreach (PropertyInfo prop in typeof(T).GetProperties())
        {
            command.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(entity));
        }
        
        command.ExecuteNonQuery();
    } 
    
    public void InsertMultipleRefletion(List<T> list, int batchSize = 1000)
    {
        var commandPart = typeof(T).CommandInsert(false);
        var page = list.Count.DividirIntUpValue(batchSize);

        using SqlConnection connection = _contextAdoNet.GetConnection();
   

        for (var i = 0; i < page; i++)
        {
            var batch = list.Skip(i * batchSize).Take(batchSize);
            var commandSql = commandPart + string.Join(", ", batch.Select(x => x.CommandValues()));
            
            using SqlCommand command = new SqlCommand(commandSql, connection);

            command.ExecuteNonQuery();
        }
    }
    
    public void InsertSafe(List<T> entityList)
    {
        var commandSql = typeof(T).CommandInsert();
        using SqlConnection connection = _contextAdoNet.GetConnection();
        using SqlCommand command = new SqlCommand(commandSql, connection);

        foreach (var entity in entityList)
        {
            command.Parameters.Clear();
            foreach (var prop in typeof(T).GetProperties())
            {
                command.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(entity));
            }
            command.ExecuteNonQuery();
        }
    }
    
    public void BulkInsert(List<T> entityList)
    {
        var tipo = typeof(T);
        using SqlConnection connection = _contextAdoNet.GetConnection();
        using SqlBulkCopy bulkCopy = new SqlBulkCopy(connection);
        bulkCopy.DestinationTableName = tipo.Name;
        var table = entityList.ToDataTable(tipo);
        bulkCopy.BatchSize = 10000;
        bulkCopy.EnableStreaming = true;
        bulkCopy.BulkCopyTimeout = 60000;
        bulkCopy.WriteToServer(table);

    }
    
}