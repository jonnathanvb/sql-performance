using System.Data.SqlClient;
using System.Reflection;

namespace Infra.Repository;

public abstract class BaseRepositoryAbstract<T> where T : class
{

    private string CommandInsert(Type entity)
    {

        PropertyInfo[] properties = entity.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var tableName = entity.Name;
        var columns = string.Join(", ", properties.Where(x => !x.PropertyType.IsClass).Select(prop => prop.Name));
        var values = string.Join(", ", properties.Where(x => !x.PropertyType.IsClass).Select(prop => $"@{prop.Name}"));

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
        using SqlConnection connection = new SqlConnection("");
        connection.Open();
        using SqlCommand command = new SqlCommand(commandSql, connection);
        
        foreach (PropertyInfo prop in typeof(T).GetProperties())
        {
            command.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(entity));
        }
        
        command.ExecuteNonQuery();
    } 
}