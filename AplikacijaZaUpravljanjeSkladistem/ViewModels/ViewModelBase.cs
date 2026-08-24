using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AplikacijaZaUpravljanjeSkladistem.ViewModels;

// ViewModel: logika i binding (INotifyPropertyChanged)
public abstract class ViewModelBase : INotifyPropertyChanged
{
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
