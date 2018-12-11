namespace Reseptionist.models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ReseptionistContext : DbContext
    {
        public ReseptionistContext()
            : base("name=ReseptionistContext")
        {
        }

        public virtual DbSet<CLIENT> USERS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CLIENT>()
                .Property(e => e.MAIL)
                .IsUnicode(false);
        }
    }
}
