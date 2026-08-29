using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AplikacijaZaUpravljanjeSkladistem.Data;
using AplikacijaZaUpravljanjeSkladistem.Models;
using AplikacijaZaUpravljanjeSkladistem.Views;

namespace AplikacijaZaUpravljanjeSkladistem.ViewModels;

// Use case: rad sa povezanim entitetima (nalog - stavke - proizvod)
public class NalogListViewModel : ViewModelBase
{
    public ObservableCollection<Nalog> Nalozi { get; } = new();

    public RelayCommand NoviNalogCommand { get; }

    public NalogListViewModel()
    {
        NoviNalogCommand = new RelayCommand(_ => NoviNalog());

        if (!DizajnMod)
            Ucitaj();
    }

    private void Ucitaj()
    {
        Nalozi.Clear();
        using var kontekst = new AppDbContext();
        foreach (var nalog in kontekst.Nalozi
                     .Include(n => n.Korisnik)
                     .Include(n => n.Stavke)
                     .OrderByDescending(n => n.Datum))
            Nalozi.Add(nalog);
    }

    private void NoviNalog()
    {
        var prozor = new NalogDetaljiWindow();
        if (prozor.ShowDialog() == true)
            Ucitaj();
    }
}
