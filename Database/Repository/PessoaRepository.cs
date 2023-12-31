

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

    public void Insert(List<Pessoa> entityList)
    {
        var commandPart = typeof(Pessoa).CommandInsert(false);
        using SqlConnection connection = _contextAdoNet.GetConnection();
        
        foreach (var x in entityList)
        {
            var commandSql = $"{commandPart} ('{x.Nome}', '{x.Telefone}', '{x.Logradouro}', '{x.Uf}', {x.Ano}, {x.Mes}, {x.Dia})";
            
            using SqlCommand command = new SqlCommand(commandSql, connection);
            command.ExecuteNonQuery();
        }
    }

    public void InsertMultiple(List<Pessoa> list, int batchSize = 1000)
    {
        var commandPart = typeof(Pessoa).CommandInsert(false);
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