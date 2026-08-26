using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using AplikacijaZaUpravljanjeSkladistem.Data;

namespace AplikacijaZaUpravljanjeSkladistem.Services;

// Use case: izvestaj o stanju zaliha (PDF)
public class IzvestajServis
{
    public void GenerisiIzvestajZaliha(string putanja)
    {
        using var kontekst = new AppDbContext();
        var proizvodi = kontekst.Proizvodi
            .Include(p => p.Kategorija)
            .OrderBy(p => p.Kategorija!.Naziv)
            .ThenBy(p => p.Naziv)
            .ToList();

        Document.Create(dokument =>
        {
            dokument.Page(stranica =>
            {
                stranica.Margin(30);
                stranica.Header().Text("Izvestaj o stanju zaliha").FontSize(18).Bold();

                stranica.Content().Table(tabela =>
                {
                    tabela.ColumnsDefinition(kolone =>
                    {
                        kolone.RelativeColumn(3);
                        kolone.RelativeColumn(2);
                        kolone.RelativeColumn(2);
                        kolone.RelativeColumn(2);
                        kolone.RelativeColumn(2);
                    });

                    tabela.Header(zaglavlje =>
                    {
                        zaglavlje.Cell().Text("Naziv").Bold();
                        zaglavlje.Cell().Text("Sifra").Bold();
                        zaglavlje.Cell().Text("Kategorija").Bold();
                        zaglavlje.Cell().Text("Cena").Bold();
                        zaglavlje.Cell().Text("Kolicina").Bold();
                    });

                    foreach (var proizvod in proizvodi)
                    {
                        tabela.Cell().Text(proizvod.Naziv);
                        tabela.Cell().Text(proizvod.Sifra);
                        tabela.Cell().Text(proizvod.Kategorija?.Naziv ?? "");
                        tabela.Cell().Text(proizvod.Cena.ToString("0.00"));
                        tabela.Cell().Text(proizvod.KolicinaNaStanju.ToString());
                    }
                });

                stranica.Footer().AlignCenter().Text(tekst =>
                {
                    tekst.CurrentPageNumber();
                    tekst.Span(" / ");
                    tekst.TotalPages();
                });
            });
        }).GeneratePdf(putanja);
    }
}
