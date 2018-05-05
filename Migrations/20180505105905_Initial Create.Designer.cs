using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Multilang.Db.Contexts;

namespace MultiLang.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180505105905_Initial Create")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<int>("translationEngine");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
        }
    }
}
