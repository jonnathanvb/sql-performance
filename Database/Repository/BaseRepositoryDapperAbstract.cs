using Dapper;
using Infra.Utils;

namespace Infra.Repository;

public class BaseRepositoryDapperAbstract<T>
{
    protected readonly ContextDapper _dapper;

    public BaseRepositoryDapperAbstract(ContextDapper dapper)
    {
        _dapper = dapper;
    }
    
    public void InsertSafe(List<T> entityList)
    {
        var commandSql = typeof(T).CommandInsert();
        using var connection = _dapper.GetConnection();
        foreach (var entity in entityList)
        {
            connection.Execute(commandSql, entity);
        }
    }
    
    public void InsertMultipleSafe(List<T> list, int batchSize = 1000)
    {
        var commandSql = typeof(T).CommandInsert();
        using var connection = _dapper.GetConnection();
        var page = list.Count.DividirIntUpValue(batchSize);
   

        for (var i = 0; i < page; i++)
        {
            var batchObj = list.Skip(i * batchSize).Take(batchSize);
            

            connection.Execute(commandSql, batchObj.ToArray());
        }
    }
    public void InsertMultipleReflaction(List<T> list, int batchSize = 1000)
    {
        var commandPart = typeof(T).CommandInsert(false);
        using var connection = _dapper.GetConnection();
        var page = list.Count.DividirIntUpValue(batchSize);
   

        for (var i = 0; i < page; i++)
        {
            var batchObj = list.Skip(i * batchSize).Take(batchSize);
            var commandSql = commandPart + string.Join(", ", batchObj.Select(x => x.CommandValues()));

            connection.Execute(commandSql);
        }
    }
}