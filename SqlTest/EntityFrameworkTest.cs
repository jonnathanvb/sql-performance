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

public class EntityFrameworkTest: IClassFixture<RepositoryFixture>
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    private readonly RepositoryFixture _fixture;
    private readonly List<Pessoa> _pessoas;

    public EntityFrameworkTest(ITestOutputHelper testOutputHelper, RepositoryFixture fixture)
    {
        _testOutputHelper = testOutputHelper;
        _fixture = fixture;
        _pessoas = fixture.listPessoa.CloneLista() ;
    }

   

    /// <summary>
    /// Search basico
    /// </summary>
    // [Fact]
    // public void Search()
    // {
    //     var x = _fixture.pessoaEntityFramework.Search();
    //     _testOutputHelper.WriteLine(JsonConvert.SerializeObject(x, Formatting.Indented));
    //     Assert.True(true);
    // }

    /// <summary>
    /// Insert unico
    /// </summary>
    // [Fact]
    // public void InsertSingle()
    // {
    //     _testOutputHelper.WriteLine($"INICIO ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
    //     _fixture.pessoaEntityFramework.Insert(_pessoas.FirstOrDefault());
    //     _testOutputHelper.WriteLine($"FIM ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
    //     Assert.True(true);
    //
    // }

    /// <summary>
    /// Insert de lista, utilizando o insert unico da entidade
    /// </summary>
    [Fact]
    public void InsertListForEachInternal()
    {
        _testOutputHelper.WriteLine($"INICIO ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        foreach (var pessoa in _pessoas)
        {
            _fixture.pessoaEntityFramework.Insert(pessoa);
        }
        _testOutputHelper.WriteLine($"FIM ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        Assert.True(true);
    }

    /// <summary>
    /// Insert de lista, percorrendo a entidade e dando saveChanges em cada interacao
    /// </summary>
    [Fact]
    public void InsertListForEachExternal()
    {
        _testOutputHelper.WriteLine($"INICIO ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        _fixture.pessoaEntityFramework.InsertListSaveEach(_pessoas);
        _testOutputHelper.WriteLine($"FIM ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        Assert.True(true);
    }

    /// <summary>
    /// Insert de lista, percorrendo a entidade e dando saveChanges fora da interacao
    /// </summary>
    [Fact]
    public void InsertListEntityAdd()
    {
        _testOutputHelper.WriteLine($"INICIO ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        _fixture.pessoaEntityFramework.InsertListSaveAll(_pessoas);
        _testOutputHelper.WriteLine($"FIM ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        Assert.True(true);
    }

    /// <summary>
    /// Insert de lista, utilizando o AddRange do entity
    /// </summary>
    [Fact]
    public void InsertListEntityAddRange()
    {
        _testOutputHelper.WriteLine($"INICIO ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        _fixture.pessoaEntityFramework.InsertListRange(_pessoas);
        _testOutputHelper.WriteLine($"FIM ESCRITA: {DateTime.Now.ToString(CultureInfo.InvariantCulture)}");
        Assert.True(true);
    }
}