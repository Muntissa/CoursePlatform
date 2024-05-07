using CoursePlatform.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursePlatform.Common
{
    public class CoursePlatformContext : DbContext
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
