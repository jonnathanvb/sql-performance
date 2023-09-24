using System.Data;
using System.Reflection;

namespace Infra.Utils;

public static class DataTableUtil
{
    public static DataTable ToDataTable<T>(this List<T> list, Type entity)
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
                valores[i] = properties[i].GetValue(obj) ?? DBNull.Value;
            }

            datatable.Rows.Add(valores);
        }


        return datatable;
    }
}