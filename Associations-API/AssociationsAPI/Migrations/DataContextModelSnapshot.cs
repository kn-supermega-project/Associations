﻿// <auto-generated />
using AssociationsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AssociationsAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AssociationsAPI.Models.Words", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Word");

                    b.HasKey("Id");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("AssociationsAPI.Models.WordsRel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("FemalePercents");

                    b.Property<int>("FemalePoints");

                    b.Property<float>("MalePercents");

                    b.Property<int>("MalePoints");

                    b.Property<float>("TotalPercents");

                    b.Property<int>("TotalPoints");

                    b.Property<int>("WordId");

                    b.Property<int>("WordRelId");

                    b.HasKey("Id");

                    b.HasIndex("WordId");

                    b.ToTable("WordsRels");
                });

            modelBuilder.Entity("AssociationsAPI.Models.WordsRel", b =>
                {
                    b.HasOne("AssociationsAPI.Models.Words", "word")
                        .WithMany("Rels")
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
