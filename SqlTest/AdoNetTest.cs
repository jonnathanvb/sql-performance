using Bogus;
using Bogus.DataSets;
using Infra.Enitty;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace SqlTest;

public class AdoNetTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public AdoNetTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    /// <summary>
    /// Faz teste de perfomace de um insert usando insert basico
    /// </summary>
    [Fact]
    public void InsertOne()
    {
      
        var pessoa = new Faker<Pessoa>("pt_BR")
            .RuleFor(x => x.Nome, x => x.Name.FullName(x.PickRandom<Name.Gender>()))
            .RuleFor(x => x.Telefone, x => x.Phone.PhoneNumber())
            .RuleFor(x => x.Logradouro, x => x.Address.StreetName())
            .RuleFor(x => x.Uf, x => x.Address.StateAbbr())
            .RuleFor(x => x.Ano, )
            .RuleFor(x => x.Mes, )
            .RuleFor(x => x.Dia, )
            .Generate();
        
        _testOutputHelper.WriteLine(JsonConvert.SerializeObject(pessoa, Formatting.Indented));
        
        Assert.True(true);

    }
}