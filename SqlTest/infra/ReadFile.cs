using Infra.Enitty;
using Newtonsoft.Json;

namespace SqlTest.infra;

public class ReadFile
{
    public static Config ReadConfig()
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        var json = File.ReadAllText(path);
        var objeto = JsonConvert.DeserializeObject<Config>(json);
        return objeto;
    }
}