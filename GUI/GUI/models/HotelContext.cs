namespace GUI.models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HotelContext : DbContext
    {
        public HotelContext()
            : base("name=HotelContext")
        {
        }

        public virtual DbSet<ADDSERVISE> ADDSERVISE { get; set; }
        public virtual DbSet<CLIENT> CLIENT { get; set; }
        public virtual DbSet<PAYCARD> PAYCARD { get; set; }
        public virtual DbSet<ROOM> ROOM { get; set; }
        public virtual DbSet<RESERVATION> RESERVATION { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ADDSERVISE>()
                .Property(e => e.SERV_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<ADDSERVISE>()
                .Property(e => e.DESCR)
                .IsUnicode(false);

            modelBuilder.Entity<ADDSERVISE>()
                .Property(e => e.COST)
                .HasPrecision(19, 4);


            modelBuilder.Entity<CLIENT>()
                .Property(e => e.MAIL)
                .IsUnicode(false);

            modelBuilder.Entity<PAYCARD>()
                .Property(e => e.CARD_OWNER)
                .IsUnicode(false);

            modelBuilder.Entity<PAYCARD>()
                .Property(e => e.CARD_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<PAYCARD>()
                .Property(e => e.CARD_NUMBER)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ROOM>()
                .Property(e => e.COST)
                .HasPrecision(19, 4);
        }
    }
}
