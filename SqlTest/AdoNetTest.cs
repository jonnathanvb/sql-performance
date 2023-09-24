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

public class AdoNetTest: IClassFixture<RepositoryFixture>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly RepositoryFixture _fixture; 

    public AdoNetTest(ITestOutputHelper testOutputHelper, RepositoryFixture fixture)
    {
        _testOutputHelper = testOutputHelper;
        _fixture = fixture;
    }

    

    /// <summary>
    /// Faz teste de perfomace de um insert usando insert basico
    /// </summary>
    // [Fact]
    // public void InsertOne()
    // {
    //     _testOutputHelper.WriteLine("Insert");
    //     var pessoa = _fixture.listPessoa.FirstOrDefault();
    //     _fixture.pessoa.Insert(pessoa);
    //     Assert.True(true);
    //
    // }
    
    [Fact]
    public void InsertList()
    {
        _fixture.pessoa.Insert(_fixture.listPessoa);
        Assert.True(true);

    }
    
    // [Fact]
    // public void InsertListWithForEach()
    // {
    //     foreach (var pessoa in _fixture.listPessoa)
    //     {
    //         _fixture.pessoa.Insert(pessoa);
    //     }
    //     Assert.True(true);
    //
    // }
    
    [Fact]
    public void InsertMultiplo()
    {
        _testOutputHelper.WriteLine("InsertMultiplo");
        _fixture.pessoa.InsertMultiplo(_fixture.listPessoa);
        Assert.True(true);

    }
    
    [Fact()]
    public void InsertBulkList()
    {
       
        _fixture.pessoa.BulkInsert(_fixture.listPessoa);
        Assert.True(true);

    }
}