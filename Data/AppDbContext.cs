using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetKubernetes.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NetKubernetes.Data {
  public class AppDbContext : IdentityDbContext<User> {
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt){
      
    }

    protected override void OnModelCreating(ModelBuilder builder){
      base.OnModelCreating(builder);
    }

    public DbSet<Property>? Properties { get; set;}

  }
}