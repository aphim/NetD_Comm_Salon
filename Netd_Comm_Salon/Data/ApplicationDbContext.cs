using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Netd_Comm_Salon.Models;

namespace Netd_Comm_Salon.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<appointment> appointment { get; set; }
        public DbSet<client> client { get; set; }
        public DbSet<Stylist> stylist { get; set; }

    }
}
