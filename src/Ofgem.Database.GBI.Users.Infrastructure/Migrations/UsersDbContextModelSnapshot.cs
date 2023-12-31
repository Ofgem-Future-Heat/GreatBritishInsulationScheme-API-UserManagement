﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ofgem.Database.GBI.Users.Infrastructure;

#nullable disable

namespace Ofgem.Database.GBI.Users.Infrastructure.Migrations
{
    [DbContext(typeof(UsersDbContext))]
    partial class UsersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ofgem.Database.GBI.Users.Domain.Entities.ExternalUser", b =>
                {
                    b.Property<Guid>("ExternalUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<string>("UniqueUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("ExternalUserId");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.HasIndex("SupplierId");

                    b.HasIndex("UniqueUserId")
                        .IsUnique()
                        .HasFilter("[UniqueUserId] IS NOT NULL");

                    b.ToTable("ExternalUsers");
                });

            modelBuilder.Entity("Ofgem.Database.GBI.Users.Domain.Entities.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplierId"));

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SupplierId");

                    b.HasIndex("SupplierName")
                        .IsUnique();

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Ofgem.Database.GBI.Users.Domain.Entities.SupplierLicence", b =>
                {
                    b.Property<int>("SupplierLicenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplierLicenceId"));

                    b.Property<string>("LicenceName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LicenceNo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.Property<string>("SupplierLicenceReference")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("SupplierLicenceId");

                    b.HasIndex("SupplierId");

                    b.HasIndex("SupplierLicenceReference")
                        .IsUnique();

                    b.ToTable("SupplierLicences");
                });

            modelBuilder.Entity("Ofgem.Database.GBI.Users.Domain.Entities.ExternalUser", b =>
                {
                    b.HasOne("Ofgem.Database.GBI.Users.Domain.Entities.Supplier", "Supplier")
                        .WithMany("ExternalUsers")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Ofgem.Database.GBI.Users.Domain.Entities.SupplierLicence", b =>
                {
                    b.HasOne("Ofgem.Database.GBI.Users.Domain.Entities.Supplier", "Supplier")
                        .WithMany("SupplierLicences")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Ofgem.Database.GBI.Users.Domain.Entities.Supplier", b =>
                {
                    b.Navigation("ExternalUsers");

                    b.Navigation("SupplierLicences");
                });
#pragma warning restore 612, 618
        }
    }
}
