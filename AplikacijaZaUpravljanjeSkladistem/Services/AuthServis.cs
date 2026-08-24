using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AplikacijaZaUpravljanjeSkladistem.Data;
using AplikacijaZaUpravljanjeSkladistem.Models;

namespace AplikacijaZaUpravljanjeSkladistem.Services;

// Use case: prijava korisnika (autentifikacija)
public class AuthServis
{
    public static string Hesiraj(string lozinka)
    {
        var bajtovi = SHA256.HashData(Encoding.UTF8.GetBytes(lozinka));
        return Convert.ToHexString(bajtovi);
    }

    public Korisnik? PrijaviSe(string korisnickoIme, string lozinka)
    {
        var hash = Hesiraj(lozinka);

        using var kontekst = new AppDbContext();
        return kontekst.Korisnici.FirstOrDefault(k =>
            k.KorisnickoIme == korisnickoIme && k.LozinkaHash == hash);
    }
}
