using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MedicineManagement.Models;

namespace MedicineManagement.Data
{
    public class MedicineContext : DbContext
    {
        public MedicineContext(DbContextOptions<MedicineContext> options)
            : base(options)
        {
        }

        public DbSet<Medicine> Medicine { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Telling EF to use Id as identity
            modelBuilder.Entity<Medicine>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        }
    }
}
