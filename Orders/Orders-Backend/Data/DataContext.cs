﻿using Microsoft.EntityFrameworkCore;
using Orders_Shared.Entities;

namespace Orders_Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        

        public DbSet<Country> Countries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(i => i.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(i => i.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex(i => new {i.CountryId, i.Name}).IsUnique();
            modelBuilder.Entity<City>().HasIndex(i => new {i.StateId, i.Name}).IsUnique();
            DisableCascadingDelete(modelBuilder);
        }

        private void DisableCascadingDelete(ModelBuilder modelBuilder)
        {
            var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(r => r.GetForeignKeys());
            foreach (var relation in relationships)
            {
                relation.DeleteBehavior = DeleteBehavior.Restrict;   
            }
        }
    }
}
