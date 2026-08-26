using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using AplikacijaZaUpravljanjeSkladistem.Data;
using AplikacijaZaUpravljanjeSkladistem.Models;
using AplikacijaZaUpravljanjeSkladistem.Services;

namespace AplikacijaZaUpravljanjeSkladistem.ViewModels;

// Use case: izvoz i uvoz podataka (JSON/XML) - Strategy sablon bira nacin serijalizacije
public class ExportViewModel : ViewModelBase
{
    public RelayCommand IzvozJsonCommand { get; }
    public RelayCommand UvozJsonCommand { get; }
    public RelayCommand IzvozXmlCommand { get; }
    public RelayCommand UvozXmlCommand { get; }

    public ExportViewModel()
    {
        IzvozJsonCommand = new RelayCommand(_ => Izvezi(new JsonExportStrategija(), "JSON datoteke (*.json)|*.json", "json"));
        UvozJsonCommand = new RelayCommand(_ => Uvezi(new JsonExportStrategija(), "JSON datoteke (*.json)|*.json"));
        IzvozXmlCommand = new RelayCommand(_ => Izvezi(new XmlExportStrategija(), "XML datoteke (*.xml)|*.xml", "xml"));
        UvozXmlCommand = new RelayCommand(_ => Uvezi(new XmlExportStrategija(), "XML datoteke (*.xml)|*.xml"));
    }

    private void Izvezi(IExportStrategija strategija, string filter, string ekstenzija)
    {
        var dijalog = new SaveFileDialog { Filter = filter, DefaultExt = ekstenzija, FileName = $"proizvodi.{ekstenzija}" };
        if (dijalog.ShowDialog() != true)
            return;

        using var kontekst = new AppDbContext();
        var stavke = kontekst.Proizvodi.Include(p => p.Kategorija)
            .Select(p => new ProizvodExportDto
            {
                Id = p.Id,
                Naziv = p.Naziv,
                Sifra = p.Sifra,
                Cena = p.Cena,
                KolicinaNaStanju = p.KolicinaNaStanju,
                Kategorija = p.Kategorija!.Naziv
            })
            .ToList();

        strategija.Izvezi(stavke, dijalog.FileName);
        MessageBox.Show("Izvoz zavrsen.", "Izvoz", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void Uvezi(IExportStrategija strategija, string filter)
    {
        var dijalog = new OpenFileDialog { Filter = filter };
        if (dijalog.ShowDialog() != true)
            return;

        var stavke = strategija.Uvezi(dijalog.FileName);

        using var kontekst = new AppDbContext();
        foreach (var stavka in stavke)
        {
            var kategorija = kontekst.Kategorije.FirstOrDefault(k => k.Naziv == stavka.Kategorija);
            if (kategorija == null)
            {
                kategorija = new Kategorija { Naziv = stavka.Kategorija };
                kontekst.Kategorije.Add(kategorija);
                kontekst.SaveChanges();
            }

            var proizvod = kontekst.Proizvodi.FirstOrDefault(p => p.Sifra == stavka.Sifra);
            if (proizvod == null)
            {
                proizvod = new Proizvod { Sifra = stavka.Sifra };
                kontekst.Proizvodi.Add(proizvod);
            }

            proizvod.Naziv = stavka.Naziv;
            proizvod.Cena = stavka.Cena;
            proizvod.KolicinaNaStanju = stavka.KolicinaNaStanju;
            proizvod.KategorijaId = kategorija.Id;
        }

        kontekst.SaveChanges();
        MessageBox.Show($"Uvezeno stavki: {stavke.Count}.", "Uvoz", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
