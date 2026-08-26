using System.Windows;
using Microsoft.Win32;
using AplikacijaZaUpravljanjeSkladistem.Services;

namespace AplikacijaZaUpravljanjeSkladistem.ViewModels;

public class IzvestajViewModel : ViewModelBase
{
    private readonly IzvestajServis _izvestajServis = new();

    public RelayCommand GenerisiCommand { get; }

    public IzvestajViewModel()
    {
        GenerisiCommand = new RelayCommand(_ => Generisi());
    }

    // Use case: izvestaj / eksport podataka u PDF
    private void Generisi()
    {
        var dijalog = new SaveFileDialog { Filter = "PDF datoteke (*.pdf)|*.pdf", DefaultExt = "pdf", FileName = "izvestaj.pdf" };
        if (dijalog.ShowDialog() != true)
            return;

        _izvestajServis.GenerisiIzvestajZaliha(dijalog.FileName);
        MessageBox.Show("Izvestaj je generisan.", "Izvestaj", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
