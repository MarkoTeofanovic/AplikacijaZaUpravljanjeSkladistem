using System.Windows.Controls;
using AplikacijaZaUpravljanjeSkladistem.ViewModels;

namespace AplikacijaZaUpravljanjeSkladistem.Views;

public partial class ProizvodListView : UserControl
{
    public ProizvodListView()
    {
        InitializeComponent();
        DataContext = new ProizvodListViewModel();
    }
}
