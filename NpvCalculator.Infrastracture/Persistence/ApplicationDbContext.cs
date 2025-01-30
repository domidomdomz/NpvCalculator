﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NpvCalculator.Core.Entities;
using NpvCalculator.Infrastracture.Interfaces;

namespace NpvCalculator.Infrastracture.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<NpvCalculation> NpvCalculations => Set<NpvCalculation>();
        public DbSet<CashFlow> CashFlows => Set<CashFlow>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<NpvCalculation>().HasMany(c => c.CashFlows).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
