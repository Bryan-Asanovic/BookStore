﻿using Microsoft.EntityFrameworkCore;
using MyBookStore.Models;

namespace MyBookStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}