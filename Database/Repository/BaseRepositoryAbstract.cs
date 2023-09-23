using System.Data.SqlClient;
using System.Reflection;
using Infra.Connection;

namespace Infra.Repository;

public abstract class BaseRepositoryAbstract<T> where T : class
{
    private readonly ContextAdoNet _contextAdoNet;


    public BaseRepositoryAbstract(ContextAdoNet contextAdoNet)
    {
        _contextAdoNet = contextAdoNet;
    }

   

    private string CommandInsert(Type entity)
    {

        PropertyInfo[] properties = entity.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var tableName = entity.Name;
        var columns = string.Join(", ", properties.Where(x => !x.PropertyType.IsClass || x.PropertyType == typeof(string)).Select(prop => prop.Name));
        var values = string.Join(", ", properties.Where(x => !x.PropertyType.IsClass || x.PropertyType == typeof(string)).Select(prop => $"@{prop.Name}"));

        return $"INSERT INTO {tableName} ({columns}) VALUES ({values});";
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
    
    public void InsertLoteOne(List<T> entityList)
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
    
}