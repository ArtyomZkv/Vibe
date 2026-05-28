using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Like> Likes => Set<Like>();
        public DbSet<Match> Matches => Set<Match>();
        public DbSet<Dialog> Dialogs => Set<Dialog>();
        public DbSet<Message> Messages => Set<Message>();
    }
}
