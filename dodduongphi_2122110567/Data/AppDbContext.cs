﻿using dodduongphi_2122110567.Model;
using Microsoft.EntityFrameworkCore;

namespace dodduongphi_2122110567.Data
{
    public class AppDbContext : DbContext
    {   

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
