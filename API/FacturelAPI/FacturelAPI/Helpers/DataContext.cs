using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FacturelAPI.Entities;
using FacturelAPI.Models.Bills;
using Microsoft.EntityFrameworkCore;

namespace FacturelAPI.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Bill> Bills { get; set; }
    }
}
