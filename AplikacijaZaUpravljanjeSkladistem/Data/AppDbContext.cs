using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using AplikacijaZaUpravljanjeSkladistem.Models;

namespace AplikacijaZaUpravljanjeSkladistem.Data;

// Entity Framework: entiteti povezani relacijama, CRUD operacije preko DbSet-ova
public class AppDbContext : DbContext
{
    public DbSet<Kategorija> Kategorije => Set<Kategorija>();
    public DbSet<Proizvod> Proizvodi => Set<Proizvod>();
    public DbSet<Korisnik> Korisnici => Set<Korisnik>();
    public DbSet<Nalog> Nalozi => Set<Nalog>();
    public DbSet<StavkaNaloga> StavkeNaloga => Set<StavkaNaloga>();

    private static readonly string DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "skladiste.db");

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Nasledjivanje - Prijemnica i Otpremnica u jednoj tabeli (TPH)
        modelBuilder.Entity<Nalog>()
            .HasDiscriminator<string>("TipNaloga")
            .HasValue<Prijemnica>("Prijemnica")
            .HasValue<Otpremnica>("Otpremnica");

        // Agregacija: brisanje kategorije ne brise proizvode
        modelBuilder.Entity<Proizvod>()
            .HasOne(p => p.Kategorija)
            .WithMany(k => k.Proizvodi)
            .HasForeignKey(p => p.KategorijaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Kompozicija: brisanje naloga brise njegove stavke
        modelBuilder.Entity<StavkaNaloga>()
            .HasOne(s => s.Nalog)
            .WithMany(n => n.Stavke)
            .HasForeignKey(s => s.NalogId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StavkaNaloga>()
            .HasOne(s => s.Proizvod)
            .WithMany()
            .HasForeignKey(s => s.ProizvodId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Kategorija>().HasData(
            new Kategorija { Id = 1, Naziv = "Elektronika" },
            new Kategorija { Id = 2, Naziv = "Alat" },
            new Kategorija { Id = 3, Naziv = "Kancelarijski materijal" },
            new Kategorija { Id = 4, Naziv = "Ostalo" }
        );

        // Podrazumevani nalog: admin / admin123
        modelBuilder.Entity<Korisnik>().HasData(
            new Korisnik
            {
                Id = 1,
                KorisnickoIme = "admin",
                LozinkaHash = "240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9",
                Uloga = UlogaKorisnika.Administrator
            }
        );
    }
}
