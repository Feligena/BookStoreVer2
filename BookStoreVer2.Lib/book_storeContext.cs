using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

/*
 Версия инструментов Entity Framework "6.0.2" старше версии среды выполнения "6.0.7". 
Обновите инструменты для получения последних функций и исправлений 
ошибок. См. https://aka.ms/AAc1fbw для получения дополнительной информации.
Чтобы защитить потенциально конфиденциальную информацию в строке подключения, 
вы должны удалить ее из исходного кода. Вы можете избежать формирования строки 
подключения, используя синтаксис Name= для ее чтения из 
конфигурации — см. https://go.microsoft.com/fwlink/?linkid=2131148. 
Дополнительные рекомендации по хранению строк подключения 
см. на странице http://go.microsoft.com/fwlink/?LinkId=723263.
 */

namespace BookStoreVer2.Lib
{
    public partial class book_storeContext : DbContext
    {
        public book_storeContext()
        {
        }

        public book_storeContext(DbContextOptions<book_storeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TablePerson> TablePersons { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
              //  optionsBuilder.UseMySql("server=server;database=database;uid=login;pwd=password", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
                optionsBuilder.UseMySql(" ", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<TablePerson>(entity =>
            {
                entity.HasKey(e => e.PersonId)
                    .HasName("PRIMARY");

                entity.ToTable("table_persons");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.FirstName).HasColumnName("first_name");

                entity.Property(e => e.LastName).HasColumnName("last_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
