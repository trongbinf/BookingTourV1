﻿// <auto-generated />
using System;
using BookingTour.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookingTour.Data.Migrations
{
    [DbContext(typeof(BookingTourDbContext))]
    [Migration("20241115191254_fixDataSeed")]
    partial class fixDataSeed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookingTour.Model.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActivityId"));

                    b.Property<string>("ActivityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ActivityType")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.HasKey("ActivityId");

                    b.HasIndex("TourId");

                    b.ToTable("Activities");

                    b.HasData(
                        new
                        {
                            ActivityId = 1,
                            ActivityName = "Ăn sáng tại khách sạn",
                            ActivityType = 0,
                            Description = "Thưởng thức bữa sáng buffet tại khách sạn trước khi khởi hành.",
                            Duration = new TimeSpan(0, 1, 0, 0, 0),
                            Location = "Khách sạn trung tâm Hà Nội",
                            TourId = 1
                        },
                        new
                        {
                            ActivityId = 2,
                            ActivityName = "Hướng dẫn tham quan",
                            ActivityType = 1,
                            Description = "Hướng dẫn viên cung cấp thông tin về các địa điểm nổi bật.",
                            Duration = new TimeSpan(0, 2, 0, 0, 0),
                            Location = "Điểm tham quan Hà Nội",
                            TourId = 1
                        },
                        new
                        {
                            ActivityId = 3,
                            ActivityName = "Tham quan Văn Miếu Quốc Tử Giám",
                            ActivityType = 2,
                            Description = "Khám phá di tích lịch sử với kiến trúc độc đáo.",
                            Duration = new TimeSpan(0, 3, 0, 0, 0),
                            Location = "Văn Miếu Quốc Tử Giám",
                            TourId = 1
                        },
                        new
                        {
                            ActivityId = 4,
                            ActivityName = "Dùng bữa trưa tại nhà hàng",
                            ActivityType = 0,
                            Description = "Thưởng thức ẩm thực truyền thống tại nhà hàng nổi tiếng.",
                            Duration = new TimeSpan(0, 1, 30, 0, 0),
                            Location = "Nhà hàng Phố Cổ Hà Nội",
                            TourId = 1
                        },
                        new
                        {
                            ActivityId = 5,
                            ActivityName = "Mua sắm tại chợ Đồng Xuân",
                            ActivityType = 2,
                            Description = "Tự do tham quan và mua sắm các đặc sản địa phương.",
                            Duration = new TimeSpan(0, 2, 0, 0, 0),
                            Location = "Chợ Đồng Xuân, Hà Nội",
                            TourId = 1
                        },
                        new
                        {
                            ActivityId = 6,
                            ActivityName = "Ăn sáng tại khu nghỉ dưỡng",
                            ActivityType = 0,
                            Description = "Bữa sáng với các món ăn đặc sản địa phương.",
                            Duration = new TimeSpan(0, 1, 0, 0, 0),
                            Location = "Khu nghỉ dưỡng Đà Lạt",
                            TourId = 2
                        },
                        new
                        {
                            ActivityId = 7,
                            ActivityName = "Khám phá thác Datanla",
                            ActivityType = 2,
                            Description = "Tham quan và chụp ảnh tại thác nước nổi tiếng.",
                            Duration = new TimeSpan(0, 3, 0, 0, 0),
                            Location = "Thác Datanla, Đà Lạt",
                            TourId = 2
                        },
                        new
                        {
                            ActivityId = 8,
                            ActivityName = "Dùng bữa trưa ngoài trời",
                            ActivityType = 0,
                            Description = "Bữa trưa BBQ tại khu vực gần thác nước.",
                            Duration = new TimeSpan(0, 2, 0, 0, 0),
                            Location = "Khu vực cắm trại, Đà Lạt",
                            TourId = 2
                        },
                        new
                        {
                            ActivityId = 9,
                            ActivityName = "Đi xe đạp quanh hồ Tuyền Lâm",
                            ActivityType = 2,
                            Description = "Thư giãn với chuyến đi xe đạp quanh hồ.",
                            Duration = new TimeSpan(0, 2, 0, 0, 0),
                            Location = "Hồ Tuyền Lâm, Đà Lạt",
                            TourId = 2
                        },
                        new
                        {
                            ActivityId = 10,
                            ActivityName = "Hướng dẫn bảo vệ môi trường",
                            ActivityType = 1,
                            Description = "Thực hiện các quy định bảo vệ môi trường khu tham quan.",
                            Duration = new TimeSpan(0, 0, 30, 0, 0),
                            Location = "Khu vực thiên nhiên, Đà Lạt",
                            TourId = 2
                        });
                });

            modelBuilder.Entity("BookingTour.Model.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BookingId");

                    b.HasIndex("TourId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BookingTour.Model.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Name = "Tour tham quan",
                            Status = true
                        },
                        new
                        {
                            CategoryId = 2,
                            Name = "Thiên nhiên",
                            Status = true
                        },
                        new
                        {
                            CategoryId = 3,
                            Name = "Biển",
                            Status = true
                        });
                });

            modelBuilder.Entity("BookingTour.Model.DateStart", b =>
                {
                    b.Property<int>("DateStartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DateStartId"));

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.Property<int>("TypeStatus")
                        .HasColumnType("int");

                    b.HasKey("DateStartId");

                    b.HasIndex("TourId");

                    b.ToTable("DateStarts");
                });

            modelBuilder.Entity("BookingTour.Model.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateExpire")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("bit");

                    b.Property<string>("JwtId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("BookingTour.Model.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ReviewId");

                    b.HasIndex("TourId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("BookingTour.Model.Tour", b =>
                {
                    b.Property<int>("TourId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TourId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFullDay")
                        .HasColumnType("bit");

                    b.Property<string>("MainImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("TourName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VehicleType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TourId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Tours");

                    b.HasData(
                        new
                        {
                            TourId = 1,
                            CategoryId = 1,
                            City = "Hà Nội",
                            Country = "Việt Nam",
                            Created = new DateTime(2024, 11, 16, 2, 12, 53, 395, DateTimeKind.Local).AddTicks(4086),
                            Description = "Tour tham quan các danh lam thắng cảnh trong thành phố.",
                            IsFullDay = true,
                            Price = 500000.0,
                            Status = true,
                            TourName = "Tham quan thành phố",
                            VehicleType = "Xe bus"
                        },
                        new
                        {
                            TourId = 2,
                            CategoryId = 2,
                            City = "Đà Lạt",
                            Country = "Việt Nam",
                            Created = new DateTime(2024, 11, 16, 2, 12, 53, 395, DateTimeKind.Local).AddTicks(4091),
                            Description = "Tour khám phá các khu rừng nguyên sinh.",
                            IsFullDay = false,
                            Price = 800000.0,
                            Status = true,
                            TourName = "Khám phá thiên nhiên",
                            VehicleType = "Xe du lịch"
                        },
                        new
                        {
                            TourId = 3,
                            CategoryId = 3,
                            City = "Nha Trang",
                            Country = "Việt Nam",
                            Created = new DateTime(2024, 11, 16, 2, 12, 53, 395, DateTimeKind.Local).AddTicks(4097),
                            Description = "Tour du lịch nghỉ dưỡng tại các bãi biển đẹp.",
                            IsFullDay = false,
                            Price = 1200000.0,
                            Status = true,
                            TourName = "Du lịch biển",
                            VehicleType = "Xe riêng"
                        },
                        new
                        {
                            TourId = 4,
                            CategoryId = 1,
                            City = "Hà Nội",
                            Country = "Việt Nam",
                            Created = new DateTime(2024, 11, 16, 2, 12, 53, 395, DateTimeKind.Local).AddTicks(4102),
                            Description = "Tham quan các bảo tàng nổi tiếng.",
                            IsFullDay = true,
                            Price = 300000.0,
                            Status = true,
                            TourName = "Tham quan bảo tàng",
                            VehicleType = "Xe điện"
                        },
                        new
                        {
                            TourId = 5,
                            CategoryId = 2,
                            City = "Sa Pa",
                            Country = "Việt Nam",
                            Created = new DateTime(2024, 11, 16, 2, 12, 53, 395, DateTimeKind.Local).AddTicks(4105),
                            Description = "Tour cắm trại qua đêm trong rừng.",
                            IsFullDay = false,
                            Price = 950000.0,
                            Status = true,
                            TourName = "Cắm trại rừng",
                            VehicleType = "Xe du lịch"
                        },
                        new
                        {
                            TourId = 6,
                            CategoryId = 3,
                            City = "Phú Quốc",
                            Country = "Việt Nam",
                            Created = new DateTime(2024, 11, 16, 2, 12, 53, 395, DateTimeKind.Local).AddTicks(4108),
                            Description = "Tour nghỉ dưỡng và tham quan vùng biển.",
                            IsFullDay = true,
                            Price = 1500000.0,
                            Status = true,
                            TourName = "Kỳ nghỉ biển",
                            VehicleType = "Thuyền"
                        });
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

                    b.HasData(
                        new
                        {
                            Id = "b4fe1717-8bcf-4e85-80c7-ecf029aff8e2",
                            ConcurrencyStamp = "1",
                            Name = "Admin",
                            NormalizedName = "Admin"
                        },
                        new
                        {
                            Id = "d82ab177-63bb-4206-9347-82ea9f89fef0",
                            ConcurrencyStamp = "2",
                            Name = "User",
                            NormalizedName = "User"
                        });
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

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

                    b.HasDiscriminator().HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
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

            modelBuilder.Entity("BookingTour.Model.AppUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("AppUser");
                });

            modelBuilder.Entity("BookingTour.Model.Activity", b =>
                {
                    b.HasOne("BookingTour.Model.Tour", null)
                        .WithMany("Activities")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookingTour.Model.Booking", b =>
                {
                    b.HasOne("BookingTour.Model.Tour", "Tour")
                        .WithMany("Bookings")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookingTour.Model.AppUser", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tour");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookingTour.Model.DateStart", b =>
                {
                    b.HasOne("BookingTour.Model.Tour", "Tour")
                        .WithMany("DateStarts")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("BookingTour.Model.RefreshToken", b =>
                {
                    b.HasOne("BookingTour.Model.AppUser", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookingTour.Model.Review", b =>
                {
                    b.HasOne("BookingTour.Model.Tour", "Tour")
                        .WithMany("Reviews")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookingTour.Model.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tour");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookingTour.Model.Tour", b =>
                {
                    b.HasOne("BookingTour.Model.Category", "Category")
                        .WithMany("Tours")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
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

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookingTour.Model.Category", b =>
                {
                    b.Navigation("Tours");
                });

            modelBuilder.Entity("BookingTour.Model.Tour", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Bookings");

                    b.Navigation("DateStarts");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("BookingTour.Model.AppUser", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}