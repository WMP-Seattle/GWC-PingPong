using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PlayersAPI;

namespace PingPong.Migrations
{
    [DbContext(typeof(PingPongDb))]
    [Migration("20170706164250_Test")]
    partial class Test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("PlayersAPI.PlayerData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("LastName");

                    b.Property<int>("numberLosses");

                    b.Property<int>("numberWins");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });
        }
    }
}
