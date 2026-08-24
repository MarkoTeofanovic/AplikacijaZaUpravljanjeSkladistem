using System.Windows;
using AplikacijaZaUpravljanjeSkladistem.Models;
using AplikacijaZaUpravljanjeSkladistem.ViewModels;

namespace AplikacijaZaUpravljanjeSkladistem.Views;

public partial class ProizvodDetaljiWindow : Window
{
    public ProizvodDetaljiWindow(Proizvod? postojeci)
    {
        InitializeComponent();

        var viewModel = new ProizvodDetaljiViewModel(postojeci);
        viewModel.ZatvoriProzor += () =>
        {
            DialogResult = viewModel.Sacuvano;
            Close();
        };
        DataContext = viewModel;
    }
}
