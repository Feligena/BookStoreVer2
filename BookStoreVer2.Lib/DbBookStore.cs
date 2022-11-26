using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DbConfigLib;

namespace BookStoreVer2.Lib
{
    public partial class DbBookStore : DbContext
    {
        public DbBookStore()
        {
        }

        public DbBookStore(DbContextOptions<DbBookStore> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Authorization> Authorizations { get; set; }
        public virtual DbSet<Book> Books { get; set; } 
        public virtual DbSet<BookReservation> BookReservations { get; set; } 
        public virtual DbSet<Employee> Employees { get; set; } 
        public virtual DbSet<Genre> Genres { get; set; } 
        public virtual DbSet<Human> Humans { get; set; } 
        public virtual DbSet<JobTitle> JobTitles { get; set; } 
        public virtual DbSet<PublishingHouse> PublishingHouses { get; set; }
        public virtual DbSet<User> Users { get; set; } 
        public virtual DbSet<WriteOff> WriteOffs { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            
                //  optionsBuilder.UseMySql("server=localhost;database=book_store;uid=user;pwd=****", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
                var ver = Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql");
                var conn = DbConfig.ImportFromJson("db.json").ToString();
                optionsBuilder.UseMySql(conn , ver);
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");

                entity.HasIndex(e => e.IdHuman, "id_human");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdHuman).HasColumnName("id_human");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.HasOne(d => d.IdHumanNavigation)
                    .WithMany(p => p.Authors)
                    .HasForeignKey(d => d.IdHuman)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("author_ibfk_1");
            });

            modelBuilder.Entity<Authorization>(entity =>
            {
                entity.ToTable("authorization");

                entity.HasIndex(e => e.IdEmployee, "id_employee");

                entity.HasIndex(e => e.Login, "login")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdEmployee).HasColumnName("id_employee");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.Login).HasColumnName("login");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Authorizations)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("authorization_ibfk_1");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books");

                entity.HasIndex(e => e.IdAuthor, "id_author");

                entity.HasIndex(e => e.IdGenre, "id_genre");

                entity.HasIndex(e => e.IdPubHouse, "id_pub_house");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CostPrice).HasColumnName("cost_price");

                entity.Property(e => e.IdAuthor).HasColumnName("id_author");

                entity.Property(e => e.IdGenre).HasColumnName("id_genre");

                entity.Property(e => e.IdPubHouse).HasColumnName("id_pub_house");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.NameBook)
                    .HasColumnType("text")
                    .HasColumnName("name_book");

                entity.Property(e => e.NumberPages).HasColumnName("number_pages");

                entity.Property(e => e.SellingPrice).HasColumnName("selling_price");

                entity.Property(e => e.YearPublishing).HasColumnName("year_publishing");

                entity.HasOne(d => d.IdAuthorNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.IdAuthor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("books_ibfk_1");

                entity.HasOne(d => d.IdGenreNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.IdGenre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("books_ibfk_3");

                entity.HasOne(d => d.IdPubHouseNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.IdPubHouse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("books_ibfk_2");
            });

            modelBuilder.Entity<BookReservation>(entity =>
            {
                entity.ToTable("book_reservation");

                entity.HasIndex(e => e.IdBook, "id_book");

                entity.HasIndex(e => e.IdUser, "id_user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.IdBook).HasColumnName("id_book");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IsRedeem).HasColumnName("is_redeem");

                entity.HasOne(d => d.IdBookNavigation)
                    .WithMany(p => p.BookReservations)
                    .HasForeignKey(d => d.IdBook)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("book_reservation_ibfk_1");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.BookReservations)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("book_reservation_ibfk_2");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.HasIndex(e => e.IdHuman, "id_human");

                entity.HasIndex(e => e.IdJobTitle, "id_job_title");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdHuman).HasColumnName("id_human");

                entity.Property(e => e.IdJobTitle).HasColumnName("id_job_title");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.HasOne(d => d.IdHumanNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdHuman)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employees_ibfk_1");

                entity.HasOne(d => d.IdJobTitleNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdJobTitle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employees_ibfk_2");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genres");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NameGenre)
                    .HasMaxLength(255)
                    .HasColumnName("name_genre");
            });

            modelBuilder.Entity<Human>(entity =>
            {
                entity.ToTable("human");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .HasColumnName("last_name");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(100)
                    .HasColumnName("patronymic");
            });

            modelBuilder.Entity<JobTitle>(entity =>
            {
                entity.ToTable("job_titles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NameTitle)
                    .HasMaxLength(100)
                    .HasColumnName("name_title");
            });

            modelBuilder.Entity<PublishingHouse>(entity =>
            {
                entity.ToTable("publishing_house");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.NamePubHouse)
                    .HasMaxLength(150)
                    .HasColumnName("name_pub_house");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.IdHuman, "id_human");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdHuman).HasColumnName("id_human");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.HasOne(d => d.IdHumanNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdHuman)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_ibfk_1");
            });

            modelBuilder.Entity<WriteOff>(entity =>
            {
                entity.ToTable("write_offs");

                entity.HasIndex(e => e.IdBook, "id_book");

                entity.HasIndex(e => e.IdEmployee, "id_employee");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.DateWriteOffs)
                    .HasColumnType("datetime")
                    .HasColumnName("date_write_offs")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IdBook).HasColumnName("id_book");

                entity.Property(e => e.IdEmployee).HasColumnName("id_employee");

                entity.HasOne(d => d.IdBookNavigation)
                    .WithMany(p => p.WriteOffs)
                    .HasForeignKey(d => d.IdBook)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("write_offs_ibfk_1");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.WriteOffs)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("write_offs_ibfk_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
