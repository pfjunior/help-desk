﻿// <auto-generated />
using System;
using HD.Tickets.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HD.Tickets.Infra.Data.Migrations
{
    [DbContext(typeof(TicketContext))]
    [Migration("20240510175811_Ticket")]
    partial class Ticket
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence<int>("TicketNumberSequence")
                .StartsAt(1000L);

            modelBuilder.Entity("HD.Tickets.Domain.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<Guid>("TicketId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("HD.Tickets.Domain.Entities.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Solution")
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("TicketNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR TicketNumberSequence");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Tickets", (string)null);
                });

            modelBuilder.Entity("HD.Tickets.Domain.Entities.Comment", b =>
                {
                    b.HasOne("HD.Tickets.Domain.Entities.Ticket", "Ticket")
                        .WithMany("Comments")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("HD.Tickets.Domain.Entities.Ticket", b =>
                {
                    b.OwnsOne("HD.Tickets.Domain.Entities.User", "User", b1 =>
                        {
                            b1.Property<Guid>("TicketId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Department")
                                .IsRequired()
                                .HasColumnType("varchar(150)")
                                .HasColumnName("Department");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("varchar(150)")
                                .HasColumnName("Email");

                            b1.Property<string>("UserName")
                                .IsRequired()
                                .HasColumnType("varchar(150)")
                                .HasColumnName("UserName");

                            b1.HasKey("TicketId");

                            b1.ToTable("Tickets");

                            b1.WithOwner()
                                .HasForeignKey("TicketId");
                        });

                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("HD.Tickets.Domain.Entities.Ticket", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}