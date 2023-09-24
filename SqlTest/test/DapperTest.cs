
using SqlTest.infra;
using Xunit.Abstractions;

namespace SqlTest.test;

public class DapperTest: IClassFixture<RepositoryFixture>
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly RepositoryFixture _fixture; 

    public DapperTest(ITestOutputHelper testOutputHelper, RepositoryFixture fixture)
    {
        _testOutputHelper = testOutputHelper;
        _fixture = fixture;
    }

    
    
    [Fact]
    public void InsertSafe()
    {
        _fixture.pessoaRepositoryDapper.InsertSafe(_fixture.listPessoa);
        Assert.True(true);

    }
    
    
    [Fact]
    public void Insert()
    {
        _fixture.pessoaRepositoryDapper.Insert(_fixture.listPessoa);
        Assert.True(true);

    }
   
    
    [Fact]
    public void InsertMultiple()
    {
        _fixture.pessoaRepositoryDapper.InsertMultiple(_fixture.listPessoa);
        Assert.True(true);

    }
    
    [Fact]
    public void InsertMultipleReflection()
    {
        _fixture.pessoaRepositoryDapper.InsertMultipleReflaction(_fixture.listPessoa);
        Assert.True(true);

    }
    
    [Fact]
    public void InsertMultipleSafe()
    {
        _fixture.pessoaRepositoryDapper.InsertMultipleSafe(_fixture.listPessoa);
        Assert.True(true);

    }
    
}