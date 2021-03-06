// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using socialBrothersCase.DatabaseContexts;

namespace socialBrothersCase.Database
{
    [DbContext(typeof(AddressesContext))]
    partial class AddressesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("socialBrothersCase.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<int>("HouseNumber")
                        .HasMaxLength(250)
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Adresses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3884daa0-01e9-4cb8-9ded-08025419db4f"),
                            Country = "The Netherlands",
                            HouseNumber = 100,
                            Location = "Utrecht",
                            PostalCode = "3526 KS",
                            Street = "Europalaan"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
