

using System.Data.SqlClient;
using System.Reflection;
using Infra.Connection;
using Infra.Enitty;
using Infra.Utils;

namespace Infra.Repository;

public class PessoaRepository : BaseRepositoryAbstract<Pessoa>
{
    public PessoaRepository(ContextAdoNet contextAdoNet) : base(contextAdoNet)
    {
    }

    private string CommandInsert(Type entity)
    {
        PropertyInfo[] properties = entity.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x =>
            !x.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase) &&
            (!x.PropertyType.IsClass || x.PropertyType == typeof(string))).ToArray();

        var tableName = entity.Name;
        var columns = string.Join(", ", properties.Select(prop => prop.Name));

        return $"INSERT INTO {tableName} ({columns}) VALUES ";
    }

    public void InsertMultiplo(List<Pessoa> list, int batchSize = 1000)
    {
        var commandPart = CommandInsert(typeof(Pessoa));
        var page = list.Count.DividirIntUpValue(batchSize);

        using SqlConnection connection = _contextAdoNet.GetConnection();
   

        for (var i = 0; i < page; i++)
        {
            var inputMax = list.Skip(i * batchSize).Take(batchSize);
            var commandSql = commandPart + string.Join(", ",
                inputMax.Select(x =>
                        $"('{x.Nome}', '{x.Telefone}', '{x.Logradouro}', '{x.Uf}', {x.Ano}, {x.Mes}, {x.Dia})")
                    .ToList());
            
            using SqlCommand command = new SqlCommand(commandSql, connection);

            command.ExecuteNonQuery();
        }
    }
}