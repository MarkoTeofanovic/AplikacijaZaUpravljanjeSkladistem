using System.Collections.Generic;

namespace AplikacijaZaUpravljanjeSkladistem.Services;

// Ponasajni sablon: Strategy
public interface IExportStrategija
{
    // Use case: serijalizacija i ucitavanje podataka (JSON/XML)
    void Izvezi(IEnumerable<ProizvodExportDto> stavke, string putanja);
    List<ProizvodExportDto> Uvezi(string putanja);
}
