using AplikacijaZaUpravljanjeSkladistem.ViewModels;
using Xunit;

namespace AplikacijaZaUpravljanjeSkladistem.Tests.ViewModels;

// Bar 3 jedinicna testa za logiku u ViewModelu
public class LoginViewModelTestovi
{
    [Fact]
    public void PocetnoStanje_NijePrijavljenIBezPoruke()
    {
        var viewModel = new LoginViewModel();

        Assert.False(viewModel.Uspesno);
        Assert.Equal(string.Empty, viewModel.Poruka);
    }
}
