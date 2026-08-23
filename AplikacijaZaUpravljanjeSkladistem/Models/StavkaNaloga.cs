namespace AplikacijaZaUpravljanjeSkladistem.Models;

public class StavkaNaloga
{
    public int Id { get; set; }

    public int NalogId { get; set; }
    public Nalog? Nalog { get; set; }

    public int ProizvodId { get; set; }
    public Proizvod? Proizvod { get; set; }

    public int Kolicina { get; set; }
}
