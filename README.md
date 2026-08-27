# Aplikacija za upravljanje skladistem

Desktop aplikacija za upravljanje skladistem. Izradjena u MVVM arhitekturi (WPF, .NET 8), sa Entity Framework Core i SQLite bazom.
Omogucava prijavu korisnika, kao i kreiranje, izmenu, brisanje i pretragu proizvoda po kategorijama. Preko naloga prijemnica i otpremnica sa stavkama prati se prijem i izdavanje robe. Podaci o proizvodima mogu se ucitati ili izgenerisati za izvoz u JSON ili XML formatu. Stanje zaliha se moze isporuciti u PDF dokument.

## Pokretanje


    dotnet build
    dotnet run --project AplikacijaZaUpravljanjeSkladistem


Baza se automatski kreira i pokrece se migracija pri prvom pokretanju.

Podrazumevani nalog za prijavu:
- Korisnicko ime: admin
- Lozinka: admin123

Testiranje

    dotnet test
