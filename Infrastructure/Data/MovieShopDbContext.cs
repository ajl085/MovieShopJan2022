using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        // inject the dbcontext options
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {
            
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> modelBuilder)
        {
            modelBuilder.ToTable("Purchase");
            modelBuilder.HasKey(p => p.Id);
            modelBuilder.Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Property(p => p.PurchaseNumber).ValueGeneratedOnAdd();
            modelBuilder.HasIndex(p => new { p.UserId, p.MovieId }).IsUnique();
            modelBuilder.HasOne(p => p.Customer).WithMany(p => p.Purchases).HasForeignKey(p => p.UserId);
            modelBuilder.HasOne(p => p.Movie).WithMany(p => p.Purchases).HasForeignKey(p => p.MovieId);

            modelBuilder.Property(p => p.TotalPrice).HasColumnType("decimal(18, 2)").HasDefaultValue(9.9m);
        }

        private void ConfigureFavorite(EntityTypeBuilder<Favorite> modelBuilder)
        {
            modelBuilder.ToTable("Favorite");
            modelBuilder.HasKey(f => f.Id);
            modelBuilder.HasOne(f => f.User).WithMany(f => f.Favorites).HasForeignKey(f => f.UserId);
            modelBuilder.HasOne(f => f.Movie).WithMany(f => f.Favorites).HasForeignKey(f => f.MovieId);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> modelBuilder)
        {
            modelBuilder.ToTable("Review");
            modelBuilder.HasKey(rw => new { rw.MovieId, rw.UserId });
            modelBuilder.HasOne(rw => rw.User).WithMany(rw => rw.Reviews).HasForeignKey(rw => rw.UserId);
            modelBuilder.HasOne(rw => rw.Movie).WithMany(rw => rw.Reviews).HasForeignKey(rw => rw.MovieId);

            modelBuilder.Property(rw => rw.Rating).HasColumnType("decimal(3, 2)").HasDefaultValue(9.9m);
        }

        private void ConfigureUserRole(EntityTypeBuilder<UserRole> modelBuilder)
        {
            modelBuilder.ToTable("UserRole");
            modelBuilder.HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.HasOne(ur => ur.User).WithMany(ur => ur.UserRoles).HasForeignKey(ur => ur.UserId);
            modelBuilder.HasOne(ur => ur.Role).WithMany(ur => ur.UserRoles).HasForeignKey(ur => ur.RoleId);
        }

        private void ConfigureRole(EntityTypeBuilder<Role> modelBuilder)
        {
            modelBuilder.ToTable("Role");
            modelBuilder.HasKey(r => r.Id);

            modelBuilder.Property(r => r.Name).HasMaxLength(20);
        }

        private void ConfigureUser(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.ToTable("User");
            modelBuilder.HasKey(u => u.Id);

            modelBuilder.HasIndex(u => u.Email).IsUnique();
            modelBuilder.Property(u => u.FirstName).HasMaxLength(128);
            modelBuilder.Property(u => u.LastName).HasMaxLength(128);
            modelBuilder.Property(u => u.Email).HasMaxLength(256);
            modelBuilder.Property(u => u.HashedPassword).HasMaxLength(1024);
            modelBuilder.Property(u => u.Salt).HasMaxLength(1024);
            modelBuilder.Property(u => u.PhoneNumber).HasMaxLength(16);
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> modelBuilder)
        {
            modelBuilder.ToTable("MovieCast");
            modelBuilder.HasKey(mc => new { mc.CastId, mc.MovieId, mc.Character });
            modelBuilder.HasOne(mc => mc.Movie).WithMany(mc => mc.MovieCasts).HasForeignKey(mc => mc.MovieId);
            modelBuilder.HasOne(mc => mc.Cast).WithMany(mc => mc.Movies).HasForeignKey(mc => mc.CastId);
        }

        private void ConfigureCast(EntityTypeBuilder<Cast> modelBuilder)
        {
            modelBuilder.ToTable("Cast");
            modelBuilder.HasKey(c => c.Id);
            modelBuilder.Property(c => c.Name).HasMaxLength(128);
            modelBuilder.Property(c => c.ProfilePath).HasMaxLength(2084);
            modelBuilder.Property(c => c.TmdbUrl).HasMaxLength(2084);
        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> modelBuilder)
        {
            modelBuilder.ToTable("MovieGenre");
            modelBuilder.HasKey(mg => new { mg.MovieId, mg.GenreId });
            modelBuilder.HasOne(m => m.Movie).WithMany(m => m.Genres).HasForeignKey(m => m.MovieId);
            modelBuilder.HasOne(m => m.Genre).WithMany(m => m.Movies).HasForeignKey(m => m.GenreId);
        }

        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2048);
            builder.Property(t => t.Name).HasMaxLength(256);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            // Fluent API Way
            // specify the rules for Movie Entity
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title).HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);

            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");

            builder.Ignore(m => m.Rating);
        }
    }
}
