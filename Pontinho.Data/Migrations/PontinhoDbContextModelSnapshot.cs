using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Pontinho.Data;
using Pontinho.Domain;

namespace Pontinho.Data.Migrations
{
    [DbContext(typeof(PontinhoDbContext))]
    partial class PontinhoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Pontinho.Domain.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Pontinho.Domain.Competition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<DateTime>("End");

                    b.Property<decimal>("JoiningFee");

                    b.Property<int>("MaxPoints");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<string>("Name");

                    b.Property<int?>("PlayerId");

                    b.Property<decimal>("ReturnFee");

                    b.Property<DateTime>("Start");

                    b.Property<int?>("WinnerId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("WinnerId");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("Pontinho.Domain.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompetitionId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<DateTime>("Date");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<int?>("WinnerId");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("WinnerId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Pontinho.Domain.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CompetitionId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("Email");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<string>("Name");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("UserId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Pontinho.Domain.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CardingId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<int>("MatchId");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<int>("Order");

                    b.HasKey("Id");

                    b.HasIndex("CardingId");

                    b.HasIndex("MatchId");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("Pontinho.Domain.RoundPlayerStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<int>("CurrentScore");

                    b.Property<string>("ModifiedBy");

                    b.Property<DateTime>("ModifiedUtc");

                    b.Property<int>("Order");

                    b.Property<int>("PlayerId");

                    b.Property<int>("PointsLost");

                    b.Property<int>("RoundId");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("RoundId");

                    b.ToTable("RoundPlayerStats");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Pontinho.Domain.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Pontinho.Domain.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pontinho.Domain.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Pontinho.Domain.Competition", b =>
                {
                    b.HasOne("Pontinho.Domain.Player")
                        .WithMany("Competitions")
                        .HasForeignKey("PlayerId");

                    b.HasOne("Pontinho.Domain.Player", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId");
                });

            modelBuilder.Entity("Pontinho.Domain.Match", b =>
                {
                    b.HasOne("Pontinho.Domain.Competition", "Competition")
                        .WithMany("Matches")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pontinho.Domain.Player", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId");
                });

            modelBuilder.Entity("Pontinho.Domain.Player", b =>
                {
                    b.HasOne("Pontinho.Domain.Competition")
                        .WithMany("Players")
                        .HasForeignKey("CompetitionId");

                    b.HasOne("Pontinho.Domain.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Pontinho.Domain.Round", b =>
                {
                    b.HasOne("Pontinho.Domain.Player", "Carding")
                        .WithMany()
                        .HasForeignKey("CardingId");

                    b.HasOne("Pontinho.Domain.Match", "Match")
                        .WithMany("Rounds")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Pontinho.Domain.RoundPlayerStats", b =>
                {
                    b.HasOne("Pontinho.Domain.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Pontinho.Domain.Round", "Round")
                        .WithMany("Players")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
