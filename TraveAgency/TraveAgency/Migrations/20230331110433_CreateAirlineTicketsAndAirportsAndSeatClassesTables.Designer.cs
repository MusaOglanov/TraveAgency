﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TraveAgency.DAL;

namespace TraveAgency.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230331110433_CreateAirlineTicketsAndAirportsAndSeatClassesTables")]
    partial class CreateAirlineTicketsAndAirportsAndSeatClassesTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TraveAgency.Models.AirlineTicket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AirlineCompany")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AirlineTicketDetailId")
                        .HasColumnType("int");

                    b.Property<int?>("AirportId")
                        .HasColumnType("int");

                    b.Property<int>("ArrivalAirportId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ArrivalDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartureAirportId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DepartureDateTime")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("FlightDuration")
                        .HasColumnType("time");

                    b.Property<string>("FlightNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReturnAirportId")
                        .HasColumnType("int");

                    b.Property<int>("TicketPrice")
                        .HasColumnType("int");

                    b.Property<int>("TransferAirportId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AirlineTicketDetailId");

                    b.HasIndex("AirportId");

                    b.HasIndex("ArrivalAirportId");

                    b.HasIndex("DepartureAirportId");

                    b.HasIndex("ReturnAirportId");

                    b.HasIndex("TransferAirportId");

                    b.ToTable("AirlineTickets");
                });

            modelBuilder.Entity("TraveAgency.Models.AirlineTicketDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BaggageAllowance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BaggagePrice")
                        .HasColumnType("int");

                    b.Property<string>("FlightDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("FlightDuration")
                        .HasColumnType("time");

                    b.Property<string>("Handluggage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HassBaggage")
                        .HasColumnType("bit");

                    b.Property<bool>("HassMealService")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeactive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReturn")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTransfer")
                        .HasColumnType("bit");

                    b.Property<string>("MealDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MealPrice")
                        .HasColumnType("int");

                    b.Property<int>("ReturnPrice")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReturnTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransferPrice")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransferTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AirlineTicketDetails");
                });

            modelBuilder.Entity("TraveAgency.Models.Airport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeactive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("TraveAgency.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Azərbaycan");

                    b.Property<bool>("IsDeactive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Star")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("TraveAgency.Models.HotelCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeactive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HotelCategories");
                });

            modelBuilder.Entity("TraveAgency.Models.HotelDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CheckInTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CheckOutTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("Info")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDomestic")
                        .HasColumnType("bit");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<bool>("RoomAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("WebSite")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId")
                        .IsUnique();

                    b.ToTable("HotelDetails");
                });

            modelBuilder.Entity("TraveAgency.Models.HotelHotelCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HotelCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HotelCategoryId");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelHotelCategory");
                });

            modelBuilder.Entity("TraveAgency.Models.HotelImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelImages");
                });

            modelBuilder.Entity("TraveAgency.Models.HotelRoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeactive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("HotelRoomTypes");
                });

            modelBuilder.Entity("TraveAgency.Models.SeatClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AirlineTicketId")
                        .HasColumnType("int");

                    b.Property<string>("Info")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsDeactive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SeatPrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AirlineTicketId");

                    b.ToTable("SeatClasses");
                });

            modelBuilder.Entity("TraveAgency.Models.AirlineTicket", b =>
                {
                    b.HasOne("TraveAgency.Models.AirlineTicketDetail", "AirlineTicketDetail")
                        .WithMany()
                        .HasForeignKey("AirlineTicketDetailId");

                    b.HasOne("TraveAgency.Models.Airport", null)
                        .WithMany("AirlineTickets")
                        .HasForeignKey("AirportId");

                    b.HasOne("TraveAgency.Models.Airport", "ArrivalAirport")
                        .WithMany()
                        .HasForeignKey("ArrivalAirportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TraveAgency.Models.Airport", "DepartureAirport")
                        .WithMany()
                        .HasForeignKey("DepartureAirportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TraveAgency.Models.Airport", "ReturnAirport")
                        .WithMany()
                        .HasForeignKey("ReturnAirportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TraveAgency.Models.Airport", "TransferAirport")
                        .WithMany()
                        .HasForeignKey("TransferAirportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AirlineTicketDetail");

                    b.Navigation("ArrivalAirport");

                    b.Navigation("DepartureAirport");

                    b.Navigation("ReturnAirport");

                    b.Navigation("TransferAirport");
                });

            modelBuilder.Entity("TraveAgency.Models.HotelDetail", b =>
                {
                    b.HasOne("TraveAgency.Models.Hotel", "Hotel")
                        .WithOne("HotelDetail")
                        .HasForeignKey("TraveAgency.Models.HotelDetail", "HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("TraveAgency.Models.HotelHotelCategory", b =>
                {
                    b.HasOne("TraveAgency.Models.HotelCategory", "HotelCategory")
                        .WithMany("HotelHotelCategories")
                        .HasForeignKey("HotelCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TraveAgency.Models.Hotel", "Hotel")
                        .WithMany("HotelHotelCategories")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("HotelCategory");
                });

            modelBuilder.Entity("TraveAgency.Models.HotelImage", b =>
                {
                    b.HasOne("TraveAgency.Models.Hotel", "Hotel")
                        .WithMany("HotelImages")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("TraveAgency.Models.HotelRoomType", b =>
                {
                    b.HasOne("TraveAgency.Models.Hotel", "Hotel")
                        .WithMany("HotelRoomTypes")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("TraveAgency.Models.SeatClass", b =>
                {
                    b.HasOne("TraveAgency.Models.AirlineTicket", "AirlineTicket")
                        .WithMany("SeatClasses")
                        .HasForeignKey("AirlineTicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AirlineTicket");
                });

            modelBuilder.Entity("TraveAgency.Models.AirlineTicket", b =>
                {
                    b.Navigation("SeatClasses");
                });

            modelBuilder.Entity("TraveAgency.Models.Airport", b =>
                {
                    b.Navigation("AirlineTickets");
                });

            modelBuilder.Entity("TraveAgency.Models.Hotel", b =>
                {
                    b.Navigation("HotelDetail");

                    b.Navigation("HotelHotelCategories");

                    b.Navigation("HotelImages");

                    b.Navigation("HotelRoomTypes");
                });

            modelBuilder.Entity("TraveAgency.Models.HotelCategory", b =>
                {
                    b.Navigation("HotelHotelCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
