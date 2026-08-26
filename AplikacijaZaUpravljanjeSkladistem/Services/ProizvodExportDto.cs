namespace AplikacijaZaUpravljanjeSkladistem.Services;

// Ravan oblik proizvoda za izvoz (bez kruzne reference ka Kategoriji)
public class ProizvodExportDto
{
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public string Sifra { get; set; } = string.Empty;
    public decimal Cena { get; set; }
    public int KolicinaNaStanju { get; set; }
    public string Kategorija { get; set; } = string.Empty;
}
