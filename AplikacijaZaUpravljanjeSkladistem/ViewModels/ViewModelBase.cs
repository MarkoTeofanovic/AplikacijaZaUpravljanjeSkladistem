using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AplikacijaZaUpravljanjeSkladistem.ViewModels;

// ViewModel: logika i binding (INotifyPropertyChanged)
public abstract class ViewModelBase : INotifyPropertyChanged
{
    // Sprecava pristup bazi kada XAML dizajner u Visual Studiju iscrtava prikaz
    protected static bool DizajnMod => DesignerProperties.GetIsInDesignMode(new DependencyObject());

    public event PropertyChangedEventHandler? PropertyChanged;

    protected bool SetField<T>(ref T polje, T vrednost, [CallerMemberName] string? naziv = null)
    {
        if (EqualityComparer<T>.Default.Equals(polje, vrednost))
            return false;

        polje = vrednost;
        OnPropertyChanged(naziv);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string? naziv = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(naziv));
}
