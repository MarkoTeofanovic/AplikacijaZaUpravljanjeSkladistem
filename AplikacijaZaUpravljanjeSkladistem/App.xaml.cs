using System.Windows;
using Microsoft.EntityFrameworkCore;
using AplikacijaZaUpravljanjeSkladistem.Data;

namespace AplikacijaZaUpravljanjeSkladistem;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        using var kontekst = new AppDbContext();
        kontekst.Database.Migrate();
    }
}
