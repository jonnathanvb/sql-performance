using System.Globalization;
using Bogus;
using Bogus.DataSets;
using Infra.Connection;
using Infra.Enitty;
using Infra.Repository;
using Newtonsoft.Json;
using SqlTest.infra;
using Xunit.Abstractions;

namespace SqlTest.test;

public partial class AdoNetTest: IClassFixture<RepositoryFixture>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly RepositoryFixture _fixture; 

    public AdoNetTest(ITestOutputHelper testOutputHelper, RepositoryFixture fixture)
    {
        _testOutputHelper = testOutputHelper;
        _fixture = fixture;
    }
    
    [Fact]
    public void Insert()
    {
        _fixture.pessoaRepository.Insert(_fixture.listPessoa);
        Assert.True(true);

    }
    
    [Fact]
    public void InsertSafe()
    {
        _fixture.pessoaRepository.InsertSafe(_fixture.listPessoa);
        Assert.True(true);

    }
    
    [Fact]
    public void InsertMultiple()
    {
        _fixture.pessoaRepository.InsertMultiple(_fixture.listPessoa);
        Assert.True(true);

    }
    
    [Fact]
    public void InsertMultipleReflection()
    {
        _fixture.pessoaRepository.InsertMultipleRefletion(_fixture.listPessoa);
        Assert.True(true);

    }
    
    [Fact()]
    public void InsertBulkList()
    {
        _fixture.pessoaRepository.BulkInsert(_fixture.listPessoa);
        Assert.True(true);

    }
}