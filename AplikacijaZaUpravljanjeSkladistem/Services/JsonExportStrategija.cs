using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AplikacijaZaUpravljanjeSkladistem.Services;

// Use case: izvoz podataka u JSON
public class JsonExportStrategija : IExportStrategija
{
    public void Izvezi(IEnumerable<ProizvodExportDto> stavke, string putanja)
    {
        var opcije = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(stavke, opcije);
        File.WriteAllText(putanja, json);
    }

    public List<ProizvodExportDto> Uvezi(string putanja)
    {
        var json = File.ReadAllText(putanja);
        return JsonSerializer.Deserialize<List<ProizvodExportDto>>(json) ?? new List<ProizvodExportDto>();
    }
}
