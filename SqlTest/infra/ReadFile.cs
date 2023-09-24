using Infra.Enitty;
using Newtonsoft.Json;

namespace SqlTest.infra;

public static class ReadFile
{
    public static Config ReadConfig()
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        var json = File.ReadAllText(path);
        var objeto = JsonConvert.DeserializeObject<Config>(json);
        return objeto;
    }
    
    public static List<T> CloneLista<T>(this List<T> listaOriginal)
    {
        // Serialize a lista original para JSON
        string json = JsonConvert.SerializeObject(listaOriginal);

        // Desserialize o JSON em uma nova lista (criando cópias independentes)
        List<T> listaClone = JsonConvert.DeserializeObject<List<T>>(json);

        return listaClone;
    }
}