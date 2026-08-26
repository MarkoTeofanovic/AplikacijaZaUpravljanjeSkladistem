using System.Windows.Controls;
using AplikacijaZaUpravljanjeSkladistem.ViewModels;

namespace AplikacijaZaUpravljanjeSkladistem.Views;

public partial class ExportView : UserControl
{
    public ExportView()
    {
        InitializeComponent();
        DataContext = new ExportViewModel();
    }
}
