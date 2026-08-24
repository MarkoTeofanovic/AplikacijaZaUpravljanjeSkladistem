using AplikacijaZaUpravljanjeSkladistem.Models;

namespace AplikacijaZaUpravljanjeSkladistem.Services;

// Drzi prijavljenog korisnika za trajanje sesije
public static class SesijaServis
{
    public static Korisnik? TrenutniKorisnik { get; set; }
}
