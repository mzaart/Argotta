﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Multilang.Db.Contexts;

namespace api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180408135740_GroupsMigration")]
    partial class GroupsMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("Multilang.Models.Db.Group", b =>
                {
                    b.Property<string>("Title");

                    b.Property<string>("AdminId")
                        .IsRequired();

                    b.Property<string>("members")
                        .IsRequired();

                    b.HasKey("Title");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Multilang.Models.Db.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("blobkedIdsJson")
                        .IsRequired();

                    b.Property<string>("displayName");

                    b.Property<string>("email");

                    b.Property<string>("firebaseToken")
                        .IsRequired();

                    b.Property<string>("fullName");

                    b.Property<string>("invitationsJson")
                        .IsRequired();

                    b.Property<string>("langCode");

                    b.Property<string>("language");

                    b.Property<string>("passwordHash")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
        }
    }
}
