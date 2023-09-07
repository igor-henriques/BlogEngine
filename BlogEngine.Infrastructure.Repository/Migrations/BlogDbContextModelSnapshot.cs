﻿// <auto-generated />
using System;
using BlogEngine.Infrastructure.Repository.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlogEngine.Infrastructure.Repository.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    partial class BlogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BlogEngine.Domain.Entities.BlogComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BlogPostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("PublishDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("BlogPostId");

                    b.ToTable("BlogComments");
                });

            modelBuilder.Entity("BlogEngine.Domain.Entities.BlogEditorComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BlogPostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EditorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PublishDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BlogPostId");

                    b.HasIndex("EditorId");

                    b.ToTable("BlogEditorComments");
                });

            modelBuilder.Entity("BlogEngine.Domain.Entities.BlogPost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastUpdateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("BlogEngine.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Role")
                        .HasMaxLength(6)
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BlogEngine.Domain.Entities.BlogComment", b =>
                {
                    b.HasOne("BlogEngine.Domain.Entities.BlogPost", "BlogPost")
                        .WithMany("Comments")
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BlogPost");
                });

            modelBuilder.Entity("BlogEngine.Domain.Entities.BlogEditorComment", b =>
                {
                    b.HasOne("BlogEngine.Domain.Entities.BlogPost", "BlogPost")
                        .WithMany("EditorComments")
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BlogEngine.Domain.Entities.User", "Editor")
                        .WithMany()
                        .HasForeignKey("EditorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BlogPost");

                    b.Navigation("Editor");
                });

            modelBuilder.Entity("BlogEngine.Domain.Entities.BlogPost", b =>
                {
                    b.HasOne("BlogEngine.Domain.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("BlogEngine.Domain.Entities.BlogPost", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("EditorComments");
                });
#pragma warning restore 612, 618
        }
    }
}
