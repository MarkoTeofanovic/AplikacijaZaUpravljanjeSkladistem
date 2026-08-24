using System.Collections.ObjectModel;
using System.Linq;
using AplikacijaZaUpravljanjeSkladistem.Data;
using AplikacijaZaUpravljanjeSkladistem.Models;
using Microsoft.EntityFrameworkCore;

namespace AplikacijaZaUpravljanjeSkladistem.ViewModels;

public class ProizvodDetaljiViewModel : ViewModelBase
{
    private readonly int? _id;

    public ObservableCollection<Kategorija> Kategorije { get; } = new();

    private string _naziv = string.Empty;
    public string Naziv
    {
        get => _naziv;
        set => SetField(ref _naziv, value);
    }

    private string _sifra = string.Empty;
    public string Sifra
    {
        get => _sifra;
        set => SetField(ref _sifra, value);
    }

    private decimal _cena;
    public decimal Cena
    {
        get => _cena;
        set => SetField(ref _cena, value);
    }

    private int _kolicinaNaStanju;
    public int KolicinaNaStanju
    {
        get => _kolicinaNaStanju;
        set => SetField(ref _kolicinaNaStanju, value);
    }

    private Kategorija? _izabranaKategorija;
    public Kategorija? IzabranaKategorija
    {
        get => _izabranaKategorija;
        set => SetField(ref _izabranaKategorija, value);
    }

    public bool Sacuvano { get; private set; }

    public RelayCommand SacuvajCommand { get; }
    public RelayCommand OtkaziCommand { get; }

    public event System.Action? ZatvoriProzor;

    public ProizvodDetaljiViewModel(Proizvod? postojeci)
    {
        using (var kontekst = new AppDbContext())
        {
            foreach (var kategorija in kontekst.Kategorije.OrderBy(k => k.Naziv))
                Kategorije.Add(kategorija);
        }

        if (postojeci != null)
        {
            _id = postojeci.Id;
            Naziv = postojeci.Naziv;
            Sifra = postojeci.Sifra;
            Cena = postojeci.Cena;
            KolicinaNaStanju = postojeci.KolicinaNaStanju;
            IzabranaKategorija = Kategorije.FirstOrDefault(k => k.Id == postojeci.KategorijaId);
        }

        SacuvajCommand = new RelayCommand(_ => Sacuvaj(), _ => MozeSacuvati());
        OtkaziCommand = new RelayCommand(_ => ZatvoriProzor?.Invoke());
    }

    private bool MozeSacuvati()
        => !string.IsNullOrWhiteSpace(Naziv) && !string.IsNullOrWhiteSpace(Sifra) && IzabranaKategorija != null;

    // Use case: kreiranje / izmena proizvoda
    private void Sacuvaj()
    {
        using var kontekst = new AppDbContext();

        Proizvod proizvod;
        if (_id.HasValue)
            proizvod = kontekst.Proizvodi.First(p => p.Id == _id.Value);
        else
        {
            proizvod = new Proizvod();
            kontekst.Proizvodi.Add(proizvod);
        }

        proizvod.Naziv = Naziv;
        proizvod.Sifra = Sifra;
        proizvod.Cena = Cena;
        proizvod.KolicinaNaStanju = KolicinaNaStanju;
        proizvod.KategorijaId = IzabranaKategorija!.Id;

        kontekst.SaveChanges();

        Sacuvano = true;
        ZatvoriProzor?.Invoke();
    }
}
