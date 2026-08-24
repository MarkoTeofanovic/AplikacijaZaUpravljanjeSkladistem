using System.Windows;
using AplikacijaZaUpravljanjeSkladistem.ViewModels;

namespace AplikacijaZaUpravljanjeSkladistem.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();

        var viewModel = new LoginViewModel();
        viewModel.ZatvoriProzor += () =>
        {
            DialogResult = viewModel.Uspesno;
            Close();
        };
        DataContext = viewModel;
    }
}
