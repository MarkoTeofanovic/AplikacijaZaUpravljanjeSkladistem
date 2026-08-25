using System.Windows;
using AplikacijaZaUpravljanjeSkladistem.ViewModels;

namespace AplikacijaZaUpravljanjeSkladistem.Views;

public partial class NalogDetaljiWindow : Window
{
    public NalogDetaljiWindow()
    {
        InitializeComponent();

        var viewModel = new NalogDetaljiViewModel();
        viewModel.ZatvoriProzor += () =>
        {
            DialogResult = viewModel.Sacuvano;
            Close();
        };
        DataContext = viewModel;
    }
}
