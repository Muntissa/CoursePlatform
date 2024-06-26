﻿using CoursePlatform.Common.Entities;
using CoursePlatform.Common.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoursePlatform.Common
{
    public class CoursePlatformContext : IdentityDbContext<User, Role, long>
    {
        protected IServiceProvider _services;

        public CoursePlatformContext(IServiceProvider services)
        {
            _services = services;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=CoursePlatform;Username=postgres;Password=admin");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (Type type in _services.GetServices<IDataEntity>().Select(s => s.GetType()))
                modelBuilder.Entity(type);

            base.OnModelCreating(modelBuilder);
        }
    }
}
