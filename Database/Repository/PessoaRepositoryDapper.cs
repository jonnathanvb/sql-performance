using Dapper;
using Infra.Enitty;
using Infra.Utils;

namespace Infra.Repository;

public class PessoaRepositoryDapper: BaseRepositoryDapperAbstract<Pessoa>
{
    public PessoaRepositoryDapper(ContextDapper dapper) : base(dapper)
    {
        
    }
    
    public void Insert(List<Pessoa> entityList)
    {
        var commandPart = typeof(Pessoa).CommandInsert(false);
        using var connection = _dapper.GetConnection();
        foreach (var x in entityList)
        {
            var commandSql = $"{commandPart} ('{x.Nome}', '{x.Telefone}', '{x.Logradouro}', '{x.Uf}', {x.Ano}, {x.Mes}, {x.Dia})";
            connection.Execute(commandSql);
        }
    }
    

    public void InsertMultiple(List<Pessoa> list, int batchSize = 1000)
    {
        var commandPart = typeof(Pessoa).CommandInsert(false);
        using var connection = _dapper.GetConnection();
        var page = list.Count.DividirIntUpValue(batchSize);


        for (var i = 0; i < page; i++)
        {
            var inputMax = list.Skip(i * batchSize).Take(batchSize);
            var commandSql = commandPart + string.Join(", ",
                inputMax.Select(x =>
                        $"('{x.Nome}', '{x.Telefone}', '{x.Logradouro}', '{x.Uf}', {x.Ano}, {x.Mes}, {x.Dia})")
                    .ToList());
            

            connection.Execute(commandSql);
        }
    }
}