using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<PanelUser> PanelUsers { get; set; }
    public DbSet<Adres> Adresler { get; set; }
    public DbSet<Firma> Firmalar { get; set; }
    public DbSet<Il> Iller { get; set; }
    public DbSet<Ilce> Ilceler { get; set; }
    public DbSet<Kullanici> Kullanicilar { get; set; }
    public DbSet<KullaniciYetki> KullaniciYetkiler { get; set; }
    public DbSet<PanelYetki> PanelYetkiler { get; set; }
    public DbSet<Ulke> Ulkeler { get; set; }
    public DbSet<Unvan> Unvanlar { get; set; }
    public DbSet<VergiDairesi> VergiDairesi { get; set; }
    
    public DbSet<Package> Packages { get; set; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define relationships for Adres
        modelBuilder.Entity<Adres>()
            .HasOne(a => a.Firma)
            .WithMany()
            .HasForeignKey(a => a.FirmaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Adres>()
            .HasOne(a => a.Ulke)
            .WithMany()
            .HasForeignKey(a => a.UlkeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Adres>()
            .HasOne(a => a.Il)
            .WithMany()
            .HasForeignKey(a => a.IlId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Adres>()
            .HasOne(a => a.Ilce)
            .WithMany()
            .HasForeignKey(a => a.IlceId)
            .OnDelete(DeleteBehavior.Restrict);

        // Define relationships for Firma, Ulke, Il, Ilce, etc.
        modelBuilder.Entity<Firma>()
            .HasMany(f => f.Adresler)
            .WithOne(a => a.Firma)
            .HasForeignKey(a => a.FirmaId);

        modelBuilder.Entity<Ulke>()
            .HasMany(u => u.Adresler)
            .WithOne(a => a.Ulke)
            .HasForeignKey(a => a.UlkeId);

        modelBuilder.Entity<Il>()
            .HasMany(i => i.Adresler)
            .WithOne(a => a.Il)
            .HasForeignKey(a => a.IlId);

        modelBuilder.Entity<Ilce>()
            .HasMany(ic => ic.Adresler)
            .WithOne(a => a.Ilce)
            .HasForeignKey(a => a.IlceId);

        // Define other entity relationships if necessary
    }
}
