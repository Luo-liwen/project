using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MyMVCDemoo.Models.EF
{
    public partial class MiniQQContext : DbContext
    {
        public MiniQQContext()
        {
        }

        public MiniQQContext(DbContextOptions<MiniQQContext> options)
            : base(options)
        {
        }

        public virtual DbSet<T001用户表> T001用户表s { get; set; }
        public virtual DbSet<T002好友表> T002好友表s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=MiniQQ;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T001用户表>(entity =>
            {
                entity.HasKey(e => e.Qq号);

                entity.ToTable("T001用户表");

                entity.Property(e => e.Qq号)
                    .HasMaxLength(50)
                    .HasColumnName("QQ号");

                entity.Property(e => e.密码)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.性别)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.昵称)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.生日).HasColumnType("date");

                entity.Property(e => e.邮箱)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<T002好友表>(entity =>
            {
                entity.ToTable("T002好友表");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id");

                entity.Property(e => e.Qq号)
                    .HasMaxLength(50)
                    .HasColumnName("QQ号");

                entity.Property(e => e.姓名).HasMaxLength(50);

                entity.Property(e => e.电话).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
