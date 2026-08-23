namespace AplikacijaZaUpravljanjeSkladistem.Models;

// Lista korisnickih uloga
public enum UlogaKorisnika
{
    Administrator,
    Magacioner
}

public class Korisnik
{
    public int Id { get; set; }
    public string KorisnickoIme { get; set; } = string.Empty;
    public string LozinkaHash { get; set; } = string.Empty;
    public UlogaKorisnika Uloga { get; set; }
}
