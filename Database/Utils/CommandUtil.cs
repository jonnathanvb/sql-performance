using System.Reflection;

namespace Infra.Utils;

public static class CommandUtil
{
    public static string CommandInsert(this Type entity, bool withValue = true)
    {
            PropertyInfo[] properties = entity.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => !x.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase) && (!x.PropertyType.IsClass || x.PropertyType == typeof(string))).ToArray();

            var tableName = entity.Name;
            var columns = string.Join(", ", properties.Select(prop => prop.Name));
            var values = withValue ? $"({string.Join(", ", properties.Select(prop => $"@{prop.Name}"))})" : "" ;

            return $"INSERT INTO {tableName} ({columns}) VALUES {values}";
    }
    
    public static string CommandValues<T>(this T entity)
    {
        PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => !x.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase) && (!x.PropertyType.IsClass || x.PropertyType == typeof(string))).ToArray();
        
        var objetos = properties.Select(x => x.GetValue(entity).ToSql());

        return  $"({string.Join(", ", objetos)})";
    }
    
    public static string CommandUpdate(Type entity)
    {
      
        PropertyInfo[] properties = entity.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var tableName = entity.Name;
        var columns = string.Join(", ", properties.Where(x => x.Name != "Id" && !x.PropertyType.IsClass).Select(prop => $"{prop.Name} = @{prop.Name}"));

        return $"UPDATE {tableName} SET {columns} Where Id = @Id";
    }

    private static string ToSql(this object? value)
    {
        if (value?.GetType() == typeof(string))
        {
            return $"'{value}'";
        }

        return value?.ToString() ?? "Null";
    }
}