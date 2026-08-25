using AplikacijaZaUpravljanjeSkladistem.Models;

namespace AplikacijaZaUpravljanjeSkladistem.ViewModels;

// Pomocna klasa za unos stavke naloga u dijalogu (nije EF entitet)
public class StavkaUnosa
{
    public Proizvod Proizvod { get; set; } = null!;
    public int Kolicina { get; set; }
}
