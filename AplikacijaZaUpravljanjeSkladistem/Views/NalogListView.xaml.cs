using System.Windows.Controls;
using AplikacijaZaUpravljanjeSkladistem.ViewModels;

namespace AplikacijaZaUpravljanjeSkladistem.Views;

public partial class NalogListView : UserControl
{
    public NalogListView()
    {
        InitializeComponent();
        DataContext = new NalogListViewModel();
    }
}
