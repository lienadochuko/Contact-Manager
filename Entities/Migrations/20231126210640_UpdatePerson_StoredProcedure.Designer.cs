﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Entities.Migrations
{
    [DbContext(typeof(PersonsDbContext))]
    [Migration("20231126210640_UpdatePerson_StoredProcedure")]
    partial class UpdatePersonStoredProcedure
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Country", b =>
                {
                    b.Property<Guid>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryID");

                    b.ToTable("Countries", (string)null);

                    b.HasData(
                        new
                        {
                            CountryID = new Guid("ee69e6b7-641c-4abb-80b6-1d3971edc904"),
                            CountryName = "ENGLAND"
                        },
                        new
                        {
                            CountryID = new Guid("a5386467-0411-44ca-90c8-e3a26b655b94"),
                            CountryName = "AUSTRALIA"
                        },
                        new
                        {
                            CountryID = new Guid("84312db0-4672-407d-b212-87e550c74428"),
                            CountryName = "EIGHT-MAN-EMPIRE"
                        },
                        new
                        {
                            CountryID = new Guid("bf9830c7-8e3d-485c-8fb1-9c7f9652fc75"),
                            CountryName = "BULGARIA"
                        },
                        new
                        {
                            CountryID = new Guid("1e00daea-f817-4375-9ec9-a6e6bd48bace"),
                            CountryName = "ONARIA"
                        },
                        new
                        {
                            CountryID = new Guid("09508eb3-57db-49ce-b368-0a00ffb75828"),
                            CountryName = "ZERIOBIA"
                        },
                        new
                        {
                            CountryID = new Guid("40db2e52-37ab-43c3-8b22-eb24a2084ded"),
                            CountryName = "ONSLOW"
                        },
                        new
                        {
                            CountryID = new Guid("e801e3c0-7835-4760-9e0b-27078011a2e5"),
                            CountryName = "ETHOPIA"
                        },
                        new
                        {
                            CountryID = new Guid("6e0a41b7-baf4-4f2b-bace-a654e87c664b"),
                            CountryName = "HOLDFAST"
                        },
                        new
                        {
                            CountryID = new Guid("7680241d-34b8-4f39-b494-87c27831866c"),
                            CountryName = "ISREAL"
                        },
                        new
                        {
                            CountryID = new Guid("a6939d16-ea43-442c-adee-68738a2b39cb"),
                            CountryName = "BENIN"
                        },
                        new
                        {
                            CountryID = new Guid("9ce71166-8a69-481d-9c5c-2ea5aac9e73b"),
                            CountryName = "ALGERIA"
                        },
                        new
                        {
                            CountryID = new Guid("c3ee9030-141f-4b92-b1ac-df45a84a9046"),
                            CountryName = "BELGIUM"
                        });
                });

            modelBuilder.Entity("Entities.Person", b =>
                {
                    b.Property<Guid>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid?>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("NIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonName")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<bool>("RecieveNewsLetter")
                        .HasColumnType("bit");

                    b.HasKey("PersonID");

                    b.ToTable("Persons", (string)null);

                    b.HasData(
                        new
                        {
                            PersonID = new Guid("c03bbe45-9aeb-4d24-99e0-4743016ffce9"),
                            Address = "324 DavidsViews",
                            CountryID = new Guid("ee69e6b7-641c-4abb-80b6-1d3971edc904"),
                            DOB = new DateTime(2023, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "davids.Grace@gmail.com",
                            Gender = "Male",
                            NIN = "SDBBBB-1",
                            PersonName = "Grace Davids",
                            RecieveNewsLetter = true
                        },
                        new
                        {
                            PersonID = new Guid("c3abddbd-cf50-41d2-b6c4-cc7d5a750928"),
                            Address = "2 Washington Trail",
                            CountryID = new Guid("a5386467-0411-44ca-90c8-e3a26b655b94"),
                            DOB = new DateTime(2001, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "whucknall1n@google.it",
                            Gender = "Male",
                            NIN = "SDBBBB-1A1",
                            PersonName = "Wheeler Hucknall",
                            RecieveNewsLetter = true
                        },
                        new
                        {
                            PersonID = new Guid("c6d50a47-f7e6-4482-8be0-4ddfc057fa6e"),
                            Address = "933 Kennedy Hill",
                            CountryID = new Guid("84312db0-4672-407d-b212-87e550c74428"),
                            DOB = new DateTime(2000, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "oduigan1o@plala.or.jp",
                            Gender = "Male",
                            NIN = "SDBBBB-1A2",
                            PersonName = "Oliver Duigan",
                            RecieveNewsLetter = true
                        },
                        new
                        {
                            PersonID = new Guid("d15c6d9f-70b4-48c5-afd3-e71261f1a9be"),
                            Address = "0958 Bashford Park",
                            CountryID = new Guid("bf9830c7-8e3d-485c-8fb1-9c7f9652fc75"),
                            DOB = new DateTime(1992, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "kcowwell1p@mediafire.com",
                            Gender = "Female",
                            NIN = "SDBBBB-1A3",
                            PersonName = "Kessiah Cowwell",
                            RecieveNewsLetter = true
                        },
                        new
                        {
                            PersonID = new Guid("89e5f445-d89f-4e12-94e0-5ad5b235d704"),
                            Address = "20 Johnson Point",
                            CountryID = new Guid("1e00daea-f817-4375-9ec9-a6e6bd48bace"),
                            DOB = new DateTime(1999, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "rdawidowitz1q@goo.gl",
                            Gender = "Female",
                            NIN = "SDBBBB-1A4",
                            PersonName = "Rosalyn Dawidowitz",
                            RecieveNewsLetter = true
                        },
                        new
                        {
                            PersonID = new Guid("2a6d3738-9def-43ac-9279-0310edc7ceca"),
                            Address = "00098 Hanson Hill",
                            CountryID = new Guid("09508eb3-57db-49ce-b368-0a00ffb75828"),
                            DOB = new DateTime(1995, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "balgar1r@altervista.org",
                            Gender = "Male",
                            NIN = "SDBBBB-1A5",
                            PersonName = "Blaine Algar",
                            RecieveNewsLetter = true
                        },
                        new
                        {
                            PersonID = new Guid("29339209-63f5-492f-8459-754943c74abf"),
                            Address = "4 Bultman Junction",
                            CountryID = new Guid("40db2e52-37ab-43c3-8b22-eb24a2084ded"),
                            DOB = new DateTime(1992, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mgrotty1s@zdnet.com",
                            Gender = "Female",
                            NIN = "SDBBBB-1A6",
                            PersonName = "Maureen Grotty",
                            RecieveNewsLetter = true
                        },
                        new
                        {
                            PersonID = new Guid("ac660a73-b0b7-4340-abc1-a914257a6189"),
                            Address = "84 Barnett Avenue",
                            CountryID = new Guid("e801e3c0-7835-4760-9e0b-27078011a2e5"),
                            DOB = new DateTime(1993, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "llaurenceau1t@cafepress.com",
                            Gender = "Female",
                            NIN = "SDBBBB-1A7",
                            PersonName = "Lethia Laurenceau",
                            RecieveNewsLetter = true
                        },
                        new
                        {
                            PersonID = new Guid("012107df-862f-4f16-ba94-e5c16886f005"),
                            Address = "86 Rowland Avenue",
                            CountryID = new Guid("6e0a41b7-baf4-4f2b-bace-a654e87c664b"),
                            DOB = new DateTime(2001, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "cpentelo1u@posterous.com",
                            Gender = "Female",
                            NIN = "SDBBBB-1B0",
                            PersonName = "Clarie Pentelo",
                            RecieveNewsLetter = true
                        },
                        new
                        {
                            PersonID = new Guid("cb035f22-e7cf-4907-bd07-91cfee5240f3"),
                            Address = "05043 Katie Parkway",
                            CountryID = new Guid("7680241d-34b8-4f39-b494-87c27831866c"),
                            DOB = new DateTime(1991, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "chutchason1v@theguardian.com",
                            Gender = "Male",
                            NIN = "SDBBBB-1B1",
                            PersonName = "Chandler Hutchason",
                            RecieveNewsLetter = true
                        },
                        new
                        {
                            PersonID = new Guid("28d11936-9466-4a4b-b9c5-2f0a8e0cbde9"),
                            Address = "892 Nova Place",
                            CountryID = new Guid("a6939d16-ea43-442c-adee-68738a2b39cb"),
                            DOB = new DateTime(1998, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "kmatisse1w@a8.net",
                            Gender = "Male",
                            NIN = "SDBBBB-1B2",
                            PersonName = "Keen Matisse",
                            RecieveNewsLetter = true
                        },
                        new
                        {
                            PersonID = new Guid("a3b9833b-8a4d-43e9-8690-61e08df81a9a"),
                            Address = "60021 Westend Junction",
                            CountryID = new Guid("9ce71166-8a69-481d-9c5c-2ea5aac9e73b"),
                            DOB = new DateTime(1992, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mczajkowski1x@ning.com",
                            Gender = "Male",
                            NIN = "SDBBBB-1B4",
                            PersonName = "Mickey Czajkowski",
                            RecieveNewsLetter = true
                        },
                        new
                        {
                            PersonID = new Guid("0378baa8-586b-4ec6-985d-78c2a80d47cc"),
                            Address = "87545 Village Green Hill",
                            CountryID = new Guid("c3ee9030-141f-4b92-b1ac-df45a84a9046"),
                            DOB = new DateTime(2000, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "chansom2r@hugedomains.com",
                            Gender = "Male",
                            NIN = "SDBBBB-1B3",
                            PersonName = "Calhoun Hansom",
                            RecieveNewsLetter = true
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
