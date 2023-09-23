using Bogus;
using Bogus.DataSets;
using Infra.Connection;
using Infra.Enitty;
using Infra.Repository;
using Newtonsoft.Json;
using SqlTest.infra;
using System.Globalization;
using Xunit.Abstractions;

namespace SqlTest;

public class EntityFrameworkTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly PessoaRepositoryEntity _pessoaEntityFramework;
    private List<Pessoa> _listPessoa;

    public EntityFrameworkTest(ITestOutputHelper testOutputHelper)
    {
        var config = ReadFile.ReadConfig();
        _testOutputHelper = testOutputHelper;
        var x = new ContextEntityFramework(config.SqlServer);
        _pessoaEntityFramework = new PessoaRepositoryEntity(x);
        testOutputHelper.WriteLine($"INICIO GERACAO DADOS: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        Generate();
        testOutputHelper.WriteLine($"FIM GERACAO DADOS: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
    }

    /// <summary>
    /// Gerar massa de dados
    /// </summary>
    private void Generate()
    {
        var pessoaList = new List<Pessoa>();
        for (int i = 0; i < 500; i++)
        {
            var data = new Faker().Date.BetweenOffset(DateTime.Now.AddYears(-60), DateTime.Now.AddYears(-10));
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
    /// Search basico
    /// </summary>
    [Fact]
    public void Search()
    {
        var x = _pessoaEntityFramework.Search();
        _testOutputHelper.WriteLine(JsonConvert.SerializeObject(x, Formatting.Indented));
        Assert.True(true);
    }

    /// <summary>
    /// Insert unico
    /// </summary>
    [Fact]
    public void InsertSingle()
    {
        _testOutputHelper.WriteLine($"INICIO ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        _pessoaEntityFramework.Insert(_listPessoa.FirstOrDefault());
        _testOutputHelper.WriteLine($"FIM ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        Assert.True(true);

    }

    /// <summary>
    /// Insert de lista, utilizando o insert unico da entidade
    /// </summary>
    [Fact]
    public void InsertListOneOne()
    {
        _testOutputHelper.WriteLine($"INICIO ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        foreach (var pessoa in _listPessoa)
        {
            _pessoaEntityFramework.Insert(pessoa);
        }
        _testOutputHelper.WriteLine($"FIM ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        Assert.True(true);
    }

    /// <summary>
    /// Insert de lista, percorrendo a entidade e dando saveChanges em cada interacao
    /// </summary>
    [Fact]
    public void InsertListSaveEach()
    {
        _testOutputHelper.WriteLine($"INICIO ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        _pessoaEntityFramework.InsertListSaveEach(_listPessoa);
        _testOutputHelper.WriteLine($"FIM ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        Assert.True(true);
    }

    /// <summary>
    /// Insert de lista, percorrendo a entidade e dando saveChanges fora da interacao
    /// </summary>
    [Fact]
    public void InsertListSaveAll()
    {
        _testOutputHelper.WriteLine($"INICIO ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        _pessoaEntityFramework.InsertListSaveAll(_listPessoa);
        _testOutputHelper.WriteLine($"FIM ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        Assert.True(true);
    }

    /// <summary>
    /// Insert de lista, utilizando o AddRange do entity
    /// </summary>
    [Fact]
    public void InsertListRange()
    {
        _testOutputHelper.WriteLine($"INICIO ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        _pessoaEntityFramework.InsertListRange(_listPessoa);
        _testOutputHelper.WriteLine($"FIM ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        Assert.True(true);
    }
}