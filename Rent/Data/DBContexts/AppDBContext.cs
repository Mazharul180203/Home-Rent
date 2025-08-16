using System;
using System.Collections.Generic;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.DBContexts;

public partial class AppDBContext : DbContext
{
    public AppDBContext()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<LoginUser> LoginUsers { get; set; }

    public virtual DbSet<OwnerContact> OwnerContacts { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<PropertyAvailability> PropertyAvailabilities { get; set; }

    public virtual DbSet<PropertyImage> PropertyImages { get; set; }

    public virtual DbSet<Riview> Riviews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLocation> UserLocations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-G8231R3B;Database=myapp_database;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__BOOKINGS__73951AED5661263D");

            entity.Property(e => e.BookingId).ValueGeneratedNever();
            entity.Property(e => e.BookingStatus).HasMaxLength(50);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Property).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PropertyId)
                .HasConstraintName("FK__BOOKINGS__Proper__6B24EA82");

            entity.HasOne(d => d.Renter).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RenterId)
                .HasConstraintName("FK__BOOKINGS__Renter__6C190EBB");
        });

        modelBuilder.Entity<OwnerContact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__OWNER_CO__5C66259BF18E97BC");

            entity.Property(e => e.ContactType).HasMaxLength(50);
            entity.Property(e => e.ContactValue).HasMaxLength(255);

            entity.HasOne(d => d.Owner).WithMany(p => p.OwnerContacts)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK__OWNER_CON__Owner__75A278F5");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__PAYMENTS__9B556A3868331727");

            entity.Property(e => e.PaymentId).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            entity.Property(e => e.TransactionId).HasMaxLength(255);

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__PAYMENTS__Bookin__6EF57B66");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.PropertyId).HasName("PK__PROPERTI__70C9A735A77A3FC2");

            entity.Property(e => e.PropertyId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.PricePerNight).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PropertyType).HasMaxLength(50);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.ZipCode).HasMaxLength(20);

            entity.HasOne(d => d.Owner).WithMany(p => p.Properties)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK__PROPERTIE__Owner__628FA481");
        });

        modelBuilder.Entity<PropertyAvailability>(entity =>
        {
            entity.HasKey(e => e.AvailabilityId).HasName("PK__PROPERTY__DA3979B13913252E");

            entity.ToTable("PropertyAvailability");

            entity.Property(e => e.AvailabilityId).ValueGeneratedNever();
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Property).WithMany(p => p.PropertyAvailabilities)
                .HasForeignKey(d => d.PropertyId)
                .HasConstraintName("FK__PROPERTY___Prope__68487DD7");
        });

        modelBuilder.Entity<PropertyImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__PROPERTY__7516F70C6C36C2E6");

            entity.Property(e => e.ImageId).ValueGeneratedNever();
            entity.Property(e => e.Caption).HasMaxLength(255);
            entity.Property(e => e.ImageUrl).HasMaxLength(1000);

            entity.HasOne(d => d.Property).WithMany(p => p.PropertyImages)
                .HasForeignKey(d => d.PropertyId)
                .HasConstraintName("FK__PROPERTY___Prope__656C112C");
        });

        modelBuilder.Entity<Riview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__REVIEWS__74BC79CEAA84C632");

            entity.Property(e => e.ReviewId).ValueGeneratedNever();

            entity.HasOne(d => d.Property).WithMany(p => p.Riviews)
                .HasForeignKey(d => d.PropertyId)
                .HasConstraintName("FK__REVIEWS__Propert__71D1E811");

            entity.HasOne(d => d.Renter).WithMany(p => p.Riviews)
                .HasForeignKey(d => d.RenterId)
                .HasConstraintName("FK__REVIEWS__RenterI__72C60C4A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USERS__1788CC4CCE2FE03C");

            entity.HasIndex(e => e.Email, "UQ__USERS__A9D10534C3AC816F").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.UserType).HasMaxLength(50);
        });

        modelBuilder.Entity<UserLocation>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__USER_LOC__E7FEA4978507591E");

            entity.ToTable("UserLocation");

            entity.Property(e => e.LocationId).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithMany(p => p.UserLocations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__USER_LOCA__UserI__5FB337D6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
