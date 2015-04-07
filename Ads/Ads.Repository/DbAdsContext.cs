namespace Ads.Repository
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Ads.Dominio;

    public class DbAdsContext : DbContext
    {
        public DbAdsContext()
            : base("name=Ads")
        {
        }

        public virtual DbSet<advertising> advertising { get; set; }
        public virtual DbSet<category> category { get; set; }
        public virtual DbSet<customer> customer { get; set; }
        public virtual DbSet<resource> resource { get; set; }
        public virtual DbSet<subtype> subtype { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<advertising>()
                .Property(e => e.detail)
                .IsUnicode(false);

            modelBuilder.Entity<advertising>()
                .Property(e => e.price)
                .HasPrecision(7, 2);

            modelBuilder.Entity<advertising>()
                .HasMany(e => e.resource)
                .WithRequired(e => e.advertising)
                .HasForeignKey(e => e.advertising_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<category>()
                .HasMany(e => e.subtype)
                .WithRequired(e => e.category)
                .HasForeignKey(e => e.category_id)
                .WillCascadeOnDelete(false);
        }
    }
}
