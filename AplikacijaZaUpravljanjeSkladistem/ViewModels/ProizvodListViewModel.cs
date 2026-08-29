using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using AplikacijaZaUpravljanjeSkladistem.Data;
using AplikacijaZaUpravljanjeSkladistem.Models;
using AplikacijaZaUpravljanjeSkladistem.Views;

namespace AplikacijaZaUpravljanjeSkladistem.ViewModels;

// Use case: pregled i pretraga/filtriranje proizvoda
public class ProizvodListViewModel : ViewModelBase
{
    private readonly ICollectionView _pregled;

    public ObservableCollection<Proizvod> Proizvodi { get; } = new();

    private string _pretraga = string.Empty;
    public string Pretraga
    {
        get => _pretraga;
        set
        {
            if (SetField(ref _pretraga, value))
                _pregled.Refresh();
        }
    }

    private Proizvod? _izabran;
    public Proizvod? Izabran
    {
        get => _izabran;
        set => SetField(ref _izabran, value);
    }

    public RelayCommand DodajCommand { get; }
    public RelayCommand IzmeniCommand { get; }
    public RelayCommand ObrisiCommand { get; }

    public ProizvodListViewModel()
    {
        _pregled = CollectionViewSource.GetDefaultView(Proizvodi);
        _pregled.Filter = Filtriraj;

        DodajCommand = new RelayCommand(_ => OtvoriDijalog(null));
        IzmeniCommand = new RelayCommand(_ => OtvoriDijalog(Izabran), _ => Izabran != null);
        ObrisiCommand = new RelayCommand(_ => Obrisi(), _ => Izabran != null);

        if (!DizajnMod)
            Ucitaj();
    }

    private bool Filtriraj(object obj)
    {
        if (string.IsNullOrWhiteSpace(Pretraga))
            return true;

        var proizvod = (Proizvod)obj;
        return proizvod.Naziv.Contains(Pretraga, System.StringComparison.OrdinalIgnoreCase)
            || proizvod.Sifra.Contains(Pretraga, System.StringComparison.OrdinalIgnoreCase);
    }

    private void Ucitaj()
    {
        Proizvodi.Clear();
        using var kontekst = new AppDbContext();
        foreach (var proizvod in kontekst.Proizvodi.Include(p => p.Kategorija).OrderBy(p => p.Naziv))
            Proizvodi.Add(proizvod);
    }

    // Use case: kreiranje i izmena proizvoda
    private void OtvoriDijalog(Proizvod? postojeci)
    {
        var prozor = new ProizvodDetaljiWindow(postojeci);
        if (prozor.ShowDialog() == true)
            Ucitaj();
    }

    // Use case: brisanje proizvoda
    private void Obrisi()
    {
        if (Izabran == null)
            return;

        var potvrda = MessageBox.Show($"Obrisati proizvod '{Izabran.Naziv}'?", "Potvrda",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (potvrda != MessageBoxResult.Yes)
            return;

        using var kontekst = new AppDbContext();
        var zaBrisanje = kontekst.Proizvodi.Find(Izabran.Id);
        if (zaBrisanje != null)
        {
            kontekst.Proizvodi.Remove(zaBrisanje);
            kontekst.SaveChanges();
        }

        Ucitaj();
    }
}
