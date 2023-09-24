using Bogus;
using Bogus.DataSets;
using Infra.Connection;
using Infra.Enitty;
using Infra.Repository;

namespace SqlTest.infra;

public class RepositoryFixture
{
    
    public List<Pessoa> listPessoa;
    public readonly PessoaRepository pessoaRepository;
    public readonly PessoaRepositoryDapper pessoaRepositoryDapper;
    public readonly PessoaRepositoryEntity pessoaEntityFramework;
    private readonly Config _config;

    public RepositoryFixture()
    {
        
        _config = ConfiguracaoUtil.ReadConfig();
       
        listPessoa = Generate();
        // AdoNet
        var adonet = new ContextAdoNet(_config.SqlServer);
        pessoaRepository = new PessoaRepository(adonet);
        
        // Dapper
        var contextDapper = new ContextDapper(_config.SqlServer);
        pessoaRepositoryDapper = new PessoaRepositoryDapper(contextDapper);
        
        // Entity
        var contextEntity = new ContextEntityFramework(_config.SqlServer);
        pessoaEntityFramework = new PessoaRepositoryEntity(contextEntity);
    }

    
    private List<Pessoa> Generate()
    {
        var pessoaList = new List<Pessoa>();
        for (int i = 0; i < _config.Quantidade; i++)
        {
            var data = new Faker().Date.BetweenOffset(DateTime.Now.AddYears(-50), DateTime.Now.AddYears(-20));
            var pessoa = new Faker<Pessoa>("pt_BR")
                .RuleFor(x => x.Nome, x => x.Name.FullName(x.PickRandom<Name.Gender>()))
                .RuleFor(x => x.Telefone, x => x.Phone.PhoneNumber())
                .RuleFor(x => x.Logradouro, x => x.Address.StreetName())
                .RuleFor(x => x.Uf, x => x.Address.StateAbbr())
                .RuleFor(x => x.Ano, data.Year)
                .RuleFor(x => x.Mes, data.Month)
                .RuleFor(x => x.Dia, data.Day)
                .Generate();
            pessoaList.Add(pessoa);
        }

        return pessoaList;
    }
}