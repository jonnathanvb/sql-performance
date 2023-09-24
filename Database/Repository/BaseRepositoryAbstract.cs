using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Infra.Connection;

namespace Infra.Repository;

public abstract class BaseRepositoryAbstract<T> where T : class
{
    protected readonly ContextAdoNet _contextAdoNet;


    public BaseRepositoryAbstract(ContextAdoNet contextAdoNet)
    {
        _contextAdoNet = contextAdoNet;
    }

   

    private string CommandInsert(Type entity)
    {

        PropertyInfo[] properties = entity.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => !x.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase) && (!x.PropertyType.IsClass || x.PropertyType == typeof(string))).ToArray();

        var tableName = entity.Name;
        var columns = string.Join(", ", properties.Select(prop => prop.Name));
        var values = string.Join(", ", properties.Select(prop => $"@{prop.Name}"));

        return $"INSERT INTO {tableName} ({columns}) VALUES ({values});";
    }
    
     private DataTable GetDataTable(Type entity, List<T> list)
    {

        PropertyInfo[] properties = entity.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => !x.PropertyType.IsClass || x.PropertyType == typeof(string)).ToArray();
        var datatable = new DataTable();
        foreach (var prop in properties)
        {
            datatable.Columns.Add(prop.Name, prop.PropertyType);
        }

        foreach (var obj in list)
        {
            var valores = new object[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                valores[i] = properties[i].GetValue(obj);
            }

            datatable.Rows.Add(valores);
        }


        return datatable;
    }

    
    private string CommandUpdate(Type entity)
    {
      
        PropertyInfo[] properties = entity.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var tableName = entity.Name;
        var columns = string.Join(", ", properties.Where(x => x.Name != "Id" && !x.PropertyType.IsClass).Select(prop => $"{prop.Name} = @{prop.Name}"));

        return $"UPDATE {tableName} SET {columns} Where Id = @Id";
    }

    public void Insert(T entity)
    {
        var commandSql = CommandInsert(typeof(T));
        using SqlConnection connection = _contextAdoNet.GetConnection();
        using SqlCommand command = new SqlCommand(commandSql, connection);
        
        foreach (PropertyInfo prop in typeof(T).GetProperties())
        {
            command.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(entity));
        }
        
        command.ExecuteNonQuery();
    } 
    
    public void Insert(List<T> entityList)
    {
        var commandSql = CommandInsert(typeof(T));
        using SqlConnection connection = _contextAdoNet.GetConnection();
        using SqlCommand command = new SqlCommand(commandSql, connection);

        foreach (var entity in entityList)
        {
            command.Parameters.Clear();
            foreach (PropertyInfo prop in typeof(T).GetProperties())
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
        var table = GetDataTable(tipo, entityList);
        bulkCopy.BatchSize = 100;
        bulkCopy.EnableStreaming = true;
        bulkCopy.BulkCopyTimeout = 60000;
        bulkCopy.WriteToServer(table);

    }
    
}