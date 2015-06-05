namespace Ads.Repository
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Ads.Dominio;
    using System.Data.Entity.ModelConfiguration;

    public class DbAdsContext : DbContext
    {
        public DbAdsContext()
            : base("name=Adstmp")
        {
        }

        public virtual DbSet<articles> articles { get; set; }
        public virtual DbSet<articleTypes> articleTypes { get; set; }
        public virtual DbSet<categories> categories { get; set; }
        public virtual DbSet<customers> customers { get; set; }
        public virtual DbSet<resources> resources { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<articles>()
                .Property(e => e.detail)
                .IsUnicode(false);

            modelBuilder.Entity<articles>()
                .HasMany(e => e.resources)
                .WithRequired(e => e.articles)
                .HasForeignKey(e => e.article_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<articles>()
                .HasKey(x => x.Id)
                .Map<moto>(x => x.Requires("Type").HasValue("I").HasColumnType("char").HasMaxLength(1))
                .Map<auto>(x => x.Requires("Type").HasValue("E"));

            modelBuilder.Entity<categories>()
                .HasMany(e => e.articles)
                .WithOptional(e => e.categories)
                .HasForeignKey(e => e.category_Id);

            modelBuilder.Entity<categories>()
                .HasMany(e => e.articleTypes)
                .WithRequired(e => e.categories)
                .HasForeignKey(e => e.category_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<customers>()
                .HasMany(e => e.articles)
                .WithRequired(e => e.customers)
                .HasForeignKey(e => e.customer_id)
                .WillCascadeOnDelete(false);
        }
    }

}
