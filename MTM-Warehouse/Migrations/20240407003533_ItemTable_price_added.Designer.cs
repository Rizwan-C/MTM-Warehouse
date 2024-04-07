﻿// <auto-generated />
using System;
using MTM_Warehouse.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MTM_Warehouse.Migrations
{
    [DbContext(typeof(AllDbContext))]
    [Migration("20240407003533_ItemTable_price_added")]
    partial class ItemTable_price_added
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MTM_Warehouse.Entities.ApprovalJobs", b =>
                {
                    b.Property<int>("ApprovalJobsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApprovalJobsId"));

                    b.Property<string>("Approval_Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("From_Warehouse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Item_Quantity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Item_Space")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("JobProgressId")
                        .HasColumnType("int");

                    b.Property<string>("To_Warehouse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransportItem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApprovalJobsId");

                    b.HasIndex("JobProgressId");

                    b.ToTable("ApprovalJobs_DbData");
                });

            modelBuilder.Entity("MTM_Warehouse.Entities.Approvals", b =>
                {
                    b.Property<string>("ApprovalsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApplicantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApplicantName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ApprovalJobsId")
                        .HasColumnType("int");

                    b.Property<string>("Approval_Job")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LoginEmpId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApprovalsId");

                    b.HasIndex("ApprovalJobsId")
                        .IsUnique();

                    b.HasIndex("LoginEmpId");

                    b.ToTable("Approvals_DbData");
                });

            modelBuilder.Entity("MTM_Warehouse.Entities.EmpData", b =>
                {
                    b.Property<int>("EmpDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmpDataId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PinCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WarehouseInfoId")
                        .HasColumnType("int");

                    b.HasKey("EmpDataId");

                    b.HasIndex("WarehouseInfoId");

                    b.ToTable("EmpData_DbData");
                });

            modelBuilder.Entity("MTM_Warehouse.Entities.JobProgress", b =>
                {
                    b.Property<int>("JobProgressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobProgressId"));

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JobProgressId");

                    b.ToTable("JobProgress_DbData");
                });

            modelBuilder.Entity("MTM_Warehouse.Entities.LoginEmp", b =>
                {
                    b.Property<int>("LoginEmpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoginEmpId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WarehouseInfoId")
                        .HasColumnType("int");

                    b.HasKey("LoginEmpId");

                    b.HasIndex("WarehouseInfoId");

                    b.ToTable("loginEmps_DbData");
                });

            modelBuilder.Entity("MTM_Warehouse.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

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

            modelBuilder.Entity("MTM_Warehouse.Entities.WarehouseInfo", b =>
                {
                    b.Property<int>("WarehouseInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarehouseInfoId"));

                    b.Property<string>("W_Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("W_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("W_PercentFull")
                        .HasColumnType("float");

                    b.Property<string>("W_PinCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("W_SpaceAvailable")
                        .HasColumnType("float");

                    b.Property<double?>("W_TotalCapacity")
                        .IsRequired()
                        .HasColumnType("float");

                    b.HasKey("WarehouseInfoId");

                    b.ToTable("WarehouseInfo_DbData");
                });

            modelBuilder.Entity("MTM_Warehouse.Entities.WarehouseItems", b =>
                {
                    b.Property<int>("WarehouseItemsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarehouseItemsId"));

                    b.Property<double?>("Item_Capacity_Quant")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<string>("Item_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Item_SpaceAccuired")
                        .HasColumnType("float");

                    b.Property<double?>("Item_Unit_Quant")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<double?>("Item_price_per_unit")
                        .HasColumnType("float");

                    b.Property<double?>("Item_total_cost")
                        .HasColumnType("float");

                    b.Property<int?>("WarehouseInfoId")
                        .HasColumnType("int");

                    b.HasKey("WarehouseItemsId");

                    b.HasIndex("WarehouseInfoId");

                    b.ToTable("WarehouseItems_DbData");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
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

            modelBuilder.Entity("MTM_Warehouse.Entities.ApprovalJobs", b =>
                {
                    b.HasOne("MTM_Warehouse.Entities.JobProgress", "JobProgress")
                        .WithMany("ApprovalJobs")
                        .HasForeignKey("JobProgressId");

                    b.Navigation("JobProgress");
                });

            modelBuilder.Entity("MTM_Warehouse.Entities.Approvals", b =>
                {
                    b.HasOne("MTM_Warehouse.Entities.ApprovalJobs", "ApprovalJobs")
                        .WithOne("Approvals")
                        .HasForeignKey("MTM_Warehouse.Entities.Approvals", "ApprovalJobsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MTM_Warehouse.Entities.LoginEmp", "LoginEmp")
                        .WithMany("Approvals")
                        .HasForeignKey("LoginEmpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApprovalJobs");

                    b.Navigation("LoginEmp");
                });

            modelBuilder.Entity("MTM_Warehouse.Entities.EmpData", b =>
                {
                    b.HasOne("MTM_Warehouse.Entities.WarehouseInfo", "WarehouseInfo")
                        .WithMany("EmpDatas")
                        .HasForeignKey("WarehouseInfoId");

                    b.Navigation("WarehouseInfo");
                });

            modelBuilder.Entity("MTM_Warehouse.Entities.LoginEmp", b =>
                {
                    b.HasOne("MTM_Warehouse.Entities.WarehouseInfo", "WarehouseInfo")
                        .WithMany("loginEmps")
                        .HasForeignKey("WarehouseInfoId");

                    b.Navigation("WarehouseInfo");
                });

            modelBuilder.Entity("MTM_Warehouse.Entities.WarehouseItems", b =>
                {
                    b.HasOne("MTM_Warehouse.Entities.WarehouseInfo", "WarehouseInfo")
                        .WithMany("WarehouseItems")
                        .HasForeignKey("WarehouseInfoId");

                    b.Navigation("WarehouseInfo");
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
                    b.HasOne("MTM_Warehouse.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MTM_Warehouse.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MTM_Warehouse.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MTM_Warehouse.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MTM_Warehouse.Entities.ApprovalJobs", b =>
                {
                    b.Navigation("Approvals");
                });

            modelBuilder.Entity("MTM_Warehouse.Entities.JobProgress", b =>
                {
                    b.Navigation("ApprovalJobs");
                });

            modelBuilder.Entity("MTM_Warehouse.Entities.LoginEmp", b =>
                {
                    b.Navigation("Approvals");
                });

            modelBuilder.Entity("MTM_Warehouse.Entities.WarehouseInfo", b =>
                {
                    b.Navigation("EmpDatas");

                    b.Navigation("WarehouseItems");

                    b.Navigation("loginEmps");
                });
#pragma warning restore 612, 618
        }
    }
}