using System.Windows.Controls;
using AplikacijaZaUpravljanjeSkladistem.ViewModels;

namespace AplikacijaZaUpravljanjeSkladistem.Views;

public partial class IzvestajView : UserControl
{
    public IzvestajView()
    {
        InitializeComponent();
        DataContext = new IzvestajViewModel();
    }
}
