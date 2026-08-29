using System;
using System.Collections.ObjectModel;
using System.Linq;
using AplikacijaZaUpravljanjeSkladistem.Data;
using AplikacijaZaUpravljanjeSkladistem.Models;
using AplikacijaZaUpravljanjeSkladistem.Services;

namespace AplikacijaZaUpravljanjeSkladistem.ViewModels;

// Use case: kreiranje naloga (prijemnica/otpremnica) sa stavkama
public class NalogDetaljiViewModel : ViewModelBase
{
    public ObservableCollection<TipNaloga> Tipovi { get; } = new(Enum.GetValues<TipNaloga>());
    public ObservableCollection<Proizvod> Proizvodi { get; } = new();
    public ObservableCollection<StavkaUnosa> Stavke { get; } = new();

    private TipNaloga _tip = TipNaloga.Prijemnica;
    public TipNaloga Tip
    {
        get => _tip;
        set => SetField(ref _tip, value);
    }

    private string _brojNaloga = string.Empty;
    public string BrojNaloga
    {
        get => _brojNaloga;
        set => SetField(ref _brojNaloga, value);
    }

    // Dobavljac (za prijemnicu) ili primalac (za otpremnicu)
    private string _partner = string.Empty;
    public string Partner
    {
        get => _partner;
        set => SetField(ref _partner, value);
    }

    private Proizvod? _izabranProizvod;
    public Proizvod? IzabranProizvod
    {
        get => _izabranProizvod;
        set => SetField(ref _izabranProizvod, value);
    }

    private int _kolicinaZaDodavanje = 1;
    public int KolicinaZaDodavanje
    {
        get => _kolicinaZaDodavanje;
        set => SetField(ref _kolicinaZaDodavanje, value);
    }

    private StavkaUnosa? _izabranaStavka;
    public StavkaUnosa? IzabranaStavka
    {
        get => _izabranaStavka;
        set => SetField(ref _izabranaStavka, value);
    }

    public bool Sacuvano { get; private set; }

    public RelayCommand DodajStavkuCommand { get; }
    public RelayCommand UkloniStavkuCommand { get; }
    public RelayCommand SacuvajCommand { get; }
    public RelayCommand OtkaziCommand { get; }

    public event Action? ZatvoriProzor;

    public NalogDetaljiViewModel()
    {
        if (!DizajnMod)
        {
            using var kontekst = new AppDbContext();
            foreach (var proizvod in kontekst.Proizvodi.OrderBy(p => p.Naziv))
                Proizvodi.Add(proizvod);
        }

        DodajStavkuCommand = new RelayCommand(_ => DodajStavku(), _ => IzabranProizvod != null && KolicinaZaDodavanje > 0);
        UkloniStavkuCommand = new RelayCommand(_ => UkloniStavku(), _ => IzabranaStavka != null);
        SacuvajCommand = new RelayCommand(_ => Sacuvaj(), _ => MozeSacuvati());
        OtkaziCommand = new RelayCommand(_ => ZatvoriProzor?.Invoke());
    }

    private void DodajStavku()
    {
        Stavke.Add(new StavkaUnosa { Proizvod = IzabranProizvod!, Kolicina = KolicinaZaDodavanje });
        KolicinaZaDodavanje = 1;
    }

    private void UkloniStavku()
    {
        if (IzabranaStavka != null)
            Stavke.Remove(IzabranaStavka);
    }

    private bool MozeSacuvati()
        => !string.IsNullOrWhiteSpace(BrojNaloga) && !string.IsNullOrWhiteSpace(Partner) && Stavke.Count > 0;

    // Use case: kreiranje naloga sa stavkama - azurira kolicinu na stanju
    private void Sacuvaj()
    {
        using var kontekst = new AppDbContext();

        var nalog = NalogFactory.Kreiraj(Tip);
        nalog.BrojNaloga = BrojNaloga;
        nalog.Datum = DateTime.Now;
        nalog.KorisnikId = SesijaServis.TrenutniKorisnik!.Id;

        if (nalog is Prijemnica prijemnica)
            prijemnica.Dobavljac = Partner;
        else if (nalog is Otpremnica otpremnica)
            otpremnica.Primalac = Partner;

        foreach (var unos in Stavke)
        {
            var proizvod = kontekst.Proizvodi.First(p => p.Id == unos.Proizvod.Id);

            nalog.Stavke.Add(new StavkaNaloga { ProizvodId = proizvod.Id, Kolicina = unos.Kolicina });

            proizvod.KolicinaNaStanju += Tip == TipNaloga.Prijemnica ? unos.Kolicina : -unos.Kolicina;
        }

        kontekst.Add(nalog);
        kontekst.SaveChanges();

        Sacuvano = true;
        ZatvoriProzor?.Invoke();
    }
}
