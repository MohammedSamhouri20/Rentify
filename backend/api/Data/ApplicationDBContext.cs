using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {


        [DbFunction]
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double EarthRadiusKm = 6371.0;

            var dLat = (lat2 - lat1) * (Math.PI / 180.0);
            var dLon = (lon2 - lon1) * (Math.PI / 180.0);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1 * (Math.PI / 180.0)) * Math.Cos(lat2 * (Math.PI / 180.0)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c;
        }

        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CancellationPolicy> CancellationPolicies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<ServiceImage> ServiceImages { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Chat> Chats { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
            .HasOne(b => b.Owner)
            .WithMany()
            .HasForeignKey(b => b.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
            .HasOne(b => b.Renter)
            .WithMany()
            .HasForeignKey(b => b.RenterId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
            .HasOne(bs => bs.Service)
            .WithMany()
            .HasForeignKey(bs => bs.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
            .HasOne(bp => bp.Product)
            .WithMany()
            .HasForeignKey(bp => bp.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Favorite>()
                        .HasOne(f => f.User)
                        .WithMany(u => u.Favorites)
                        .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Review>()
                        .HasOne(r => r.Product)
                        .WithMany(p => p.Reviews)
                        .HasForeignKey(r => r.ProductId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Review>()
                        .HasOne(r => r.Service)
                        .WithMany(s => s.Reviews)
                        .HasForeignKey(r => r.ServiceId)
                        .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Chat>()
                .HasOne(c => c.UserOne)
                .WithMany()
                .HasForeignKey(c => c.UserOneId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Chat>()
                .HasOne(c => c.UserTwo)
                .WithMany()
                .HasForeignKey(c => c.UserTwoId)
                .OnDelete(DeleteBehavior.Restrict);

            var initialCategories = new List<Category>
            {
                new Category { Id = 1, CategoryName = "أدوات ومعدات", CategoryType = "product" },
                new Category { Id = 2, CategoryName = "سيارات ومركبات", CategoryType = "product" },
                new Category { Id = 3, CategoryName = "الإلكترونيات", CategoryType = "product" },
                new Category { Id = 4, CategoryName = "أجهزة إلكترونية", CategoryType = "product" },
                new Category { Id = 5, CategoryName = "المطبخ والمنزل", CategoryType = "product" },
                new Category { Id = 6, CategoryName = "معدات البستنة", CategoryType = "product" },
                new Category { Id = 7, CategoryName = "أدوات رياضية", CategoryType = "product" },
                new Category { Id = 8, CategoryName = "أدوات الحيوانات الأليفة", CategoryType = "product" },
                new Category { Id = 9, CategoryName = "الفنون والحرف", CategoryType = "product" },
                new Category { Id = 10, CategoryName = "السفر والأمتعة", CategoryType = "product" },
                new Category { Id = 11, CategoryName = "الكرفانات", CategoryType = "product" },
                new Category { Id = 12, CategoryName = "الآلات الموسيقية", CategoryType = "product" },
                new Category { Id = 13, CategoryName = "الحفلات والمناسبات", CategoryType = "product" },
                new Category { Id = 14, CategoryName = "الملابس والبدلات", CategoryType = "product" },
                new Category { Id = 15, CategoryName = "المعدات الطبية", CategoryType = "product" },
                new Category { Id = 16, CategoryName = "ألعاب الطاولة والألغاز", CategoryType = "product" },
                new Category { Id = 17, CategoryName = "مستلزمات التعلم", CategoryType = "product" },
                new Category { Id = 18, CategoryName = "الألعاب الإلكترونية", CategoryType = "product" },
                new Category { Id = 19, CategoryName = "المركبات المائية", CategoryType = "product" },
                new Category { Id = 20, CategoryName = "أثاث المنزل", CategoryType = "product" },
                new Category { Id = 21, CategoryName = "مستلزمات المكتب", CategoryType = "product" },
                new Category { Id = 22, CategoryName = "معدات البناء", CategoryType = "product" },
                new Category { Id = 23, CategoryName = "معدات الصيد", CategoryType = "product" },
                new Category { Id = 24, CategoryName = "مستلزمات التخييم", CategoryType = "product" },
                new Category { Id = 25, CategoryName = "مستلزمات الخياطة", CategoryType = "product" },
                new Category { Id = 26, CategoryName = "خدمات تنظيف", CategoryType = "service" },
                new Category { Id = 27, CategoryName = "مواسرجي", CategoryType = "service" },
                new Category { Id = 28, CategoryName = "عامل بناء", CategoryType = "service" },
                new Category { Id = 29, CategoryName = "فني كهرباء", CategoryType = "service" },
                new Category { Id = 30, CategoryName = "معلم", CategoryType = "service" }
            };

            modelBuilder.Entity<Category>().HasData(initialCategories);
        }
    }
}