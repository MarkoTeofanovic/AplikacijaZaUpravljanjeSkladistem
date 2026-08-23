namespace AplikacijaZaUpravljanjeSkladistem.Models;

public class Kategorija
{
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public string? Opis { get; set; }

    // Agregacija: Kategorija - Proizvod
    public List<Proizvod> Proizvodi { get; set; } = new();
}
