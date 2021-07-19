using System;
using System.IO;
using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;   

namespace DataAccess
{
    public class SaleContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=(local);database=MyStore2;uid=sa;pwd=123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>().HasKey(c => new {c.OrderId, c.ProductId});
            modelBuilder.Entity<Member>().HasData(
             new Member {
                 MemberId = 1,
                 Email = "admin@fstore.com",
                 CompanyName="FPT",
                 Password = "admin@@",
                 City = "HCM",
                 Country = "VietNam"
             },
             new Member
             {
                 MemberId = 2,
                 Email = "ngochc1@gmail.com",
                 CompanyName = "FPT",
                 Password = "ngoc123",
                 City = "HCM",
                 Country = "VietNam"
             },
             new Member
             {
                 MemberId = 3,
                 Email = "ngoc050401@gmail.com",
                 CompanyName = "FPT",
                 Password = "ngoc123",
                 City = "DaNang",
                 Country = "VietNam"
             }
             );
            modelBuilder.Entity<Product>().HasData(
            new Product
            {
                ProductId=1,
                CategoryId=1,
                ProductName="Product1",
                UnitInStock=20,
                UnitPrice=15,
                Weight="23kg"
            },
            new Product
            {
                ProductId = 2,
                CategoryId = 2,
                ProductName = "Product2",
                UnitInStock = 20,
                UnitPrice = 15,
                Weight = "23kg"
            },
            new Product
            {
                ProductId = 3,
                CategoryId = 2,
                ProductName = "Product3",
                UnitInStock = 22,
                UnitPrice = 13,
                Weight = "21kg"
            }
            );
        }
        public DbSet<Member> Members { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderDetail> OrderDetails { set; get; }
        public DbSet<Product> Products { set; get; }
    }
}
