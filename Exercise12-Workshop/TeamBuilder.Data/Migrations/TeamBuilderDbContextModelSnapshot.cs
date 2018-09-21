﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeamBuilder.Data;

namespace TeamBuilder.Data.Migrations
{
    [DbContext(typeof(TeamBuilderDbContext))]
    partial class TeamBuilderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TeamBuilder.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatorId");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .IsUnicode(true);

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(true);

                    b.Property<DateTime?>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("TeamBuilder.Models.EventTeam", b =>
                {
                    b.Property<int>("TeamId");

                    b.Property<int>("EventId");

                    b.HasKey("TeamId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("EventTeams");
                });

            modelBuilder.Entity("TeamBuilder.Models.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InvitedUserId");

                    b.Property<bool>("IsActive");

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("InvitedUserId");

                    b.HasIndex("TeamId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("TeamBuilder.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Acronym")
                        .IsRequired()
                        .IsFixedLength(true)
                        .HasMaxLength(3)
                        .IsUnicode(false);

                    b.Property<int>("CreatorId");

                    b.Property<string>("Description")
                        .HasMaxLength(32);

                    b.Property<bool>("IsDisbanded");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("Name", "Acronym", "Id")
                        .IsUnique();

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("TeamBuilder.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<string>("FirstName")
                        .HasMaxLength(25);

                    b.Property<int>("Gender");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName")
                        .HasMaxLength(25);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.HasIndex("Username", "Id")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TeamBuilder.Models.UserTeam", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("TeamId");

                    b.HasKey("UserId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("UserTeams");
                });

            modelBuilder.Entity("TeamBuilder.Models.Event", b =>
                {
                    b.HasOne("TeamBuilder.Models.User", "Creator")
                        .WithMany("EventsCreated")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TeamBuilder.Models.EventTeam", b =>
                {
                    b.HasOne("TeamBuilder.Models.Event", "Event")
                        .WithMany("TeamsParticipating")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TeamBuilder.Models.Team", "Team")
                        .WithMany("EventsAttended")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TeamBuilder.Models.Invitation", b =>
                {
                    b.HasOne("TeamBuilder.Models.User", "InvitedUser")
                        .WithMany("InvitationsReceived")
                        .HasForeignKey("InvitedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TeamBuilder.Models.Team", "Team")
                        .WithMany("InvitationsSent")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TeamBuilder.Models.Team", b =>
                {
                    b.HasOne("TeamBuilder.Models.User", "Creator")
                        .WithMany("TeamsCreated")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("TeamBuilder.Models.UserTeam", b =>
                {
                    b.HasOne("TeamBuilder.Models.Team", "Team")
                        .WithMany("Members")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TeamBuilder.Models.User", "User")
                        .WithMany("TeamsJoined")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
