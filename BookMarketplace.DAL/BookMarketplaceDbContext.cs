using Microsoft.EntityFrameworkCore;
using BookMarketplace.Model;

namespace BookMarketplace.DAL;

public class BookMarketplaceDbContext : DbContext
{
    public BookMarketplaceDbContext(DbContextOptions<BookMarketplaceDbContext> options) : base(options)
    {}

    public DbSet<Korisnik> Korisnici { get; set; }
    public DbSet<Oglas> Oglasi { get; set; }
    public DbSet<Knjiga> Knjige { get; set; }
    public DbSet<DrustvenaIgra> DrustveneIgre { get; set; }
    public DbSet<Grad> Gradovi { get; set; }
    public DbSet<Slika> Slike { get; set; }
    public DbSet<Poruka> Poruke { get; set; }
    public DbSet<Favorit> Favoriti { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Konfiguracija za Favorit (veza između Korisnik i Oglas)
        modelBuilder.Entity<Favorit>()
            .HasKey(f => new { f.KorisnikId, f.OglasId }); // Složeni ključ

        modelBuilder.Entity<Favorit>()
            .HasOne(f => f.Korisnik)
            .WithMany(k => k.Favoriti)
            .HasForeignKey(f => f.KorisnikId)
            .OnDelete(DeleteBehavior.Restrict);;

        modelBuilder.Entity<Favorit>()
            .HasOne(f => f.Oglas)
            .WithMany(o => o.Favoriti)
            .HasForeignKey(f => f.OglasId)
            .OnDelete(DeleteBehavior.Restrict);

        // Konfiguracija za Poruku
        modelBuilder.Entity<Poruka>()
            .HasOne(p => p.Posiljatelj)
            .WithMany(k => k.PoslanePoruke)
            .HasForeignKey(p => p.PosiljateljId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Poruka>()
            .HasOne(p => p.Primatelj)
            .WithMany(k => k.PrimljenePoruke)
            .HasForeignKey(p => p.PrimateljId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Oglas>()
            .Property(o => o.Cijena)
            .HasPrecision(18, 2);
    }

}