using Bogus;
using Bogus.DataSets;
using Infra.Connection;
using Infra.Enitty;
using Infra.Repository;

namespace SqlTest.infra;

public class RepositoryFixture
{
    public readonly PessoaRepository pessoa;
    public List<Pessoa> listPessoa;
    
    public readonly PessoaRepositoryEntity pessoaEntityFramework;

    public RepositoryFixture()
    {
        var config = ReadFile.ReadConfig();
        var adonet = new ContextAdoNet(config.SqlServer);
        pessoa = new PessoaRepository(adonet);
        listPessoa = Generate();
        
        var contextEntity = new ContextEntityFramework(config.SqlServer);
        pessoaEntityFramework = new PessoaRepositoryEntity(contextEntity);
    }

    
    private List<Pessoa> Generate()
    {
        var pessoaList = new List<Pessoa>();
        for (int i = 0; i < 5000; i++)
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