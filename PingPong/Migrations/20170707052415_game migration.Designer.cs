using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PingPong;

namespace PingPong.Migrations
{
    [DbContext(typeof(PingPongDb))]
    [Migration("20170707052415_game migration")]
    partial class gamemigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("PingPong.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PlayerOneId");

                    b.Property<string>("PlayerOneName");

                    b.Property<int>("PlayerTwoId");

                    b.Property<string>("PlayerTwoName");

                    b.Property<int>("Winner");

                    b.Property<bool>("complete");

                    b.HasKey("Id");

                    b.HasIndex("PlayerOneId");

                    b.HasIndex("PlayerTwoId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("PingPong.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Office");

                    b.Property<int>("numberLosses");

                    b.Property<int>("numberWins");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("PingPong.Entities.Game", b =>
                {
                    b.HasOne("PingPong.Entities.Player", "PlayerOne")
                        .WithMany()
                        .HasForeignKey("PlayerOneId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PingPong.Entities.Player", "PlayerTwo")
                        .WithMany()
                        .HasForeignKey("PlayerTwoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
