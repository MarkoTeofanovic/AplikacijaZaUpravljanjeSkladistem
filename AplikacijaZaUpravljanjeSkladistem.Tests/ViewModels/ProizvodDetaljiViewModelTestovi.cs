using AplikacijaZaUpravljanjeSkladistem.Data;
using AplikacijaZaUpravljanjeSkladistem.ViewModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AplikacijaZaUpravljanjeSkladistem.Tests.ViewModels;

public class ProizvodDetaljiViewModelTestovi
{
    public ProizvodDetaljiViewModelTestovi()
    {
        using var kontekst = new AppDbContext();
        kontekst.Database.Migrate();
    }

    [Fact]
    public void SacuvajCommand_PraznaPolja_NijeMoguceIzvrsiti()
    {
        var viewModel = new ProizvodDetaljiViewModel(null);

        Assert.False(viewModel.SacuvajCommand.CanExecute(null));
    }

    [Fact]
    public void SacuvajCommand_PopunjenaPolja_MoguceIzvrsiti()
    {
        var viewModel = new ProizvodDetaljiViewModel(null)
        {
            Naziv = "Test proizvod",
            Sifra = "T-1"
        };
        viewModel.IzabranaKategorija = viewModel.Kategorije[0];

        Assert.True(viewModel.SacuvajCommand.CanExecute(null));
    }
}
