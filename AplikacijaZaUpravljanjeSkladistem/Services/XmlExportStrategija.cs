using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace AplikacijaZaUpravljanjeSkladistem.Services;

// Use case: izvoz podataka u XML
public class XmlExportStrategija : IExportStrategija
{
    private static readonly XmlSerializer Serializer =
        new(typeof(List<ProizvodExportDto>), new XmlRootAttribute("Proizvodi"));

    public void Izvezi(IEnumerable<ProizvodExportDto> stavke, string putanja)
    {
        using var writer = new StreamWriter(putanja);
        Serializer.Serialize(writer, new List<ProizvodExportDto>(stavke));
    }

    public List<ProizvodExportDto> Uvezi(string putanja)
    {
        using var reader = new StreamReader(putanja);
        return (List<ProizvodExportDto>)(Serializer.Deserialize(reader) ?? new List<ProizvodExportDto>());
    }
}
