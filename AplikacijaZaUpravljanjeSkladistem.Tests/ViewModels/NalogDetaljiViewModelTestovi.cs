using AplikacijaZaUpravljanjeSkladistem.Data;
using AplikacijaZaUpravljanjeSkladistem.ViewModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AplikacijaZaUpravljanjeSkladistem.Tests.ViewModels;

public class NalogDetaljiViewModelTestovi
{
    public NalogDetaljiViewModelTestovi()
    {
        using var kontekst = new AppDbContext();
        kontekst.Database.Migrate();
    }

    [Fact]
    public void DodajStavkuCommand_BezIzabranogProizvoda_NijeMoguceIzvrsiti()
    {
        var viewModel = new NalogDetaljiViewModel();

        Assert.False(viewModel.DodajStavkuCommand.CanExecute(null));
    }

    [Fact]
    public void SacuvajCommand_BezStavki_NijeMoguceIzvrsiti()
    {
        var viewModel = new NalogDetaljiViewModel
        {
            BrojNaloga = "PR-100",
            Partner = "Dobavljac"
        };

        Assert.False(viewModel.SacuvajCommand.CanExecute(null));
    }
}
