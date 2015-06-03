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
            : base("name=Ads")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<article> article { get; set; }
        public virtual DbSet<articleType> articleType { get; set; }
        public virtual DbSet<category> category { get; set; }
        public virtual DbSet<customer> customer { get; set; }
        public virtual DbSet<resource> resource { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<article>()
                .Property(e => e.detail)
                .IsUnicode(false);

            modelBuilder.Entity<article>()
                .HasMany(e => e.resource)
                .WithRequired(e => e.article)
                .HasForeignKey(e => e.article_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<article>()
                .Map<moto>(x => x.Requires("articleType").HasValue("Moto").HasColumnType("string").HasMaxLength(20))
                .Map<auto>(x => x.Requires("articleType").HasValue("Auto"));

            modelBuilder.Entity<category>()
                .HasMany(e => e.articleType)
                .WithRequired(e => e.category)
                .HasForeignKey(e => e.category_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.article)
                .WithRequired(e => e.customer)
                .HasForeignKey(e => e.customer_id)
                .WillCascadeOnDelete(false);

            //modelBuilder.Configurations.Add(new articleMap());
        }
    }

}
