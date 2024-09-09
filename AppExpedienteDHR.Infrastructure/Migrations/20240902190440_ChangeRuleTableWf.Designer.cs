﻿// <auto-generated />
using System;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppExpedienteDHR.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240902190440_ChangeRuleTableWf")]
    partial class ChangeRuleTableWf
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.ActionGroupWf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("GroupId");

                    b.ToTable("ActionGroupWfs");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.ActionRuleWf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Order")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("ResultStateId")
                        .HasColumnType("int");

                    b.Property<string>("RuleJson")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("ResultStateId");

                    b.ToTable("ActionRuleWfs");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.ActionWf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("EvaluationType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("NextStateId")
                        .HasColumnType("int");

                    b.Property<int?>("Order")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NextStateId");

                    b.HasIndex("StateId");

                    b.ToTable("ActionWfs");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.FlowHistoryWf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ActionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ActionPerformedId")
                        .HasColumnType("int");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("FlowId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("NewStateId")
                        .HasColumnType("int");

                    b.Property<string>("PerformedByUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PreviousStateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActionPerformedId");

                    b.HasIndex("FlowId");

                    b.HasIndex("NewStateId");

                    b.HasIndex("PerformedByUserId");

                    b.HasIndex("PreviousStateId");

                    b.ToTable("FlowHistoryWfs");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.FlowWf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Order")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("FlowWfs");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.GroupUserWf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupUserWfs");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.GroupWf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("FlowId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Order")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FlowId");

                    b.ToTable("GroupWfs");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.RequestFlowHistoryWf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FlowHistoryId")
                        .HasColumnType("int");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FlowHistoryId");

                    b.ToTable("RequestFlowHistoryWfs");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.StateWf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("FlowId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Order")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FlowId");

                    b.ToTable("StateWfs");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.IdentityEntities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.IdentityEntities.ApplicationUserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.ActionGroupWf", b =>
                {
                    b.HasOne("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.ActionWf", "Action")
                        .WithMany("ActionGroups")
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.GroupWf", "Group")
                        .WithMany("ActionGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Action");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.ActionRuleWf", b =>
                {
                    b.HasOne("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.ActionWf", "Action")
                        .WithMany("ActionRules")
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.StateWf", "ResultState")
                        .WithMany()
                        .HasForeignKey("ResultStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Action");

                    b.Navigation("ResultState");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.ActionWf", b =>
                {
                    b.HasOne("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.StateWf", "NextState")
                        .WithMany()
                        .HasForeignKey("NextStateId");

                    b.HasOne("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.StateWf", "State")
                        .WithMany("Actions")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("NextState");

                    b.Navigation("State");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.FlowHistoryWf", b =>
                {
                    b.HasOne("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.ActionWf", "ActionPerformed")
                        .WithMany()
                        .HasForeignKey("ActionPerformedId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.FlowWf", "Flow")
                        .WithMany()
                        .HasForeignKey("FlowId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.StateWf", "NewState")
                        .WithMany()
                        .HasForeignKey("NewStateId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("AppExpedienteDHR.Core.Domain.IdentityEntities.ApplicationUser", "PerformedByUser")
                        .WithMany()
                        .HasForeignKey("PerformedByUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.StateWf", "PreviousState")
                        .WithMany()
                        .HasForeignKey("PreviousStateId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ActionPerformed");

                    b.Navigation("Flow");

                    b.Navigation("NewState");

                    b.Navigation("PerformedByUser");

                    b.Navigation("PreviousState");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.GroupUserWf", b =>
                {
                    b.HasOne("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.GroupWf", "Group")
                        .WithMany("GroupUsers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AppExpedienteDHR.Core.Domain.IdentityEntities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.GroupWf", b =>
                {
                    b.HasOne("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.FlowWf", "Flow")
                        .WithMany("Groups")
                        .HasForeignKey("FlowId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Flow");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.RequestFlowHistoryWf", b =>
                {
                    b.HasOne("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.FlowHistoryWf", "FlowHistory")
                        .WithMany("RequestFlowHistories")
                        .HasForeignKey("FlowHistoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FlowHistory");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.StateWf", b =>
                {
                    b.HasOne("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.FlowWf", "Flow")
                        .WithMany("States")
                        .HasForeignKey("FlowId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Flow");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.IdentityEntities.ApplicationUserRole", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppExpedienteDHR.Core.Domain.IdentityEntities.ApplicationUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AppExpedienteDHR.Core.Domain.IdentityEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AppExpedienteDHR.Core.Domain.IdentityEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AppExpedienteDHR.Core.Domain.IdentityEntities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.ActionWf", b =>
                {
                    b.Navigation("ActionGroups");

                    b.Navigation("ActionRules");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.FlowHistoryWf", b =>
                {
                    b.Navigation("RequestFlowHistories");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.FlowWf", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("States");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.GroupWf", b =>
                {
                    b.Navigation("ActionGroups");

                    b.Navigation("GroupUsers");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities.StateWf", b =>
                {
                    b.Navigation("Actions");
                });

            modelBuilder.Entity("AppExpedienteDHR.Core.Domain.IdentityEntities.ApplicationUser", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}