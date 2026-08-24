using System.Windows;
using Microsoft.EntityFrameworkCore;
using AplikacijaZaUpravljanjeSkladistem.Data;
using AplikacijaZaUpravljanjeSkladistem.Views;

namespace AplikacijaZaUpravljanjeSkladistem;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Sprecava gasenje aplikacije kad se zatvori prozor za prijavu
        ShutdownMode = ShutdownMode.OnExplicitShutdown;

        using (var kontekst = new AppDbContext())
            kontekst.Database.Migrate();

        // Use case: prijava korisnika
        var prijava = new LoginWindow();
        if (prijava.ShowDialog() != true)
        {
            Shutdown();
            return;
        }

        var glavniProzor = new MainWindow();
        MainWindow = glavniProzor;
        ShutdownMode = ShutdownMode.OnMainWindowClose;
        glavniProzor.Show();
    }
}
