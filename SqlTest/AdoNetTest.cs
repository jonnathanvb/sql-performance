using System.Globalization;
using Bogus;
using Bogus.DataSets;
using Infra.Connection;
using Infra.Enitty;
using Infra.Repository;
using Newtonsoft.Json;
using SqlTest.infra;
using Xunit.Abstractions;

namespace SqlTest;

public class AdoNetTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly PessoaRepository _pessoaRepository;
    private List<Pessoa> _listPessoa;

    public AdoNetTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        var config = ReadFile.ReadConfig();
        var adonet = new ContextAdoNet(config.SqlServer);
        _pessoaRepository = new PessoaRepository(adonet);
        testOutputHelper.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        Generate();
        testOutputHelper.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
    }

    private void Generate()
    {
        var pessoaList = new List<Pessoa>();
        for (int i = 0; i < 500; i++)
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

        _listPessoa = pessoaList;
    }

    /// <summary>
    /// Faz teste de perfomace de um insert usando insert basico
    /// </summary>
    [Fact]
    public void InsertOne()
    {
        var pessoa = _listPessoa.FirstOrDefault();
        _pessoaRepository.Insert(pessoa);
        Assert.True(true);

    }
    
    [Fact]
    public void InsertLoteOne()
    {
        _pessoaRepository.InsertLoteOne(_listPessoa);
        Assert.True(true);

    }
}