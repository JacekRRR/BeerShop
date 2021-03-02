using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Piwo.Models;

namespace Piwo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Categories> Kategorie { get; set; }

        public DbSet<SpecialTags> Tags { get; set; }

        public DbSet<Produkty> Piwa { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrdedDetails> OrderDetails { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
    }
}
