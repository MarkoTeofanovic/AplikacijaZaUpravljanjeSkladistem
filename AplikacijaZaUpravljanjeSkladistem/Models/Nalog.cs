namespace AplikacijaZaUpravljanjeSkladistem.Models;

public enum TipNaloga
{
    Prijemnica,
    Otpremnica
}

// Apstraktna klasa - nasledjuju je Prijemnica i Otpremnica
public abstract class Nalog
{
    public int Id { get; set; }
    public string BrojNaloga { get; set; } = string.Empty;
    public DateTime Datum { get; set; } = DateTime.Now;

    public int KorisnikId { get; set; }
    public Korisnik? Korisnik { get; set; }

    // Kompozicija: Nalog - StavkaNaloga
    public List<StavkaNaloga> Stavke { get; set; } = new();
}

// Nasledjivanje: prijem robe u skladiste
public class Prijemnica : Nalog
{
    public string Dobavljac { get; set; } = string.Empty;
}

// Nasledjivanje: izdavanje robe iz skladista
public class Otpremnica : Nalog
{
    public string Primalac { get; set; } = string.Empty;
}
