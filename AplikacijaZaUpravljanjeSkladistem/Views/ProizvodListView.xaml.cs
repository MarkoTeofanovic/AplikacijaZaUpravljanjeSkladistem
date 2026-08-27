using System.Windows.Controls;
using AplikacijaZaUpravljanjeSkladistem.ViewModels;

namespace AplikacijaZaUpravljanjeSkladistem.Views;

// View: XAML, bez code-behind logike osim inicijalizacije
public partial class ProizvodListView : UserControl
{
    public ProizvodListView()
    {
        InitializeComponent();
        DataContext = new ProizvodListViewModel();
    }
}
