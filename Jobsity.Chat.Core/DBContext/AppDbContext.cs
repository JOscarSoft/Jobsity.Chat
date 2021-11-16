using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Jobsity.Chat.Core.Models;

namespace Jobsity.Chat.Core.DBContext
{

    public class AppDbContext : IdentityDbContext, IDataProtectionKeyContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        #region Tables
        public DbSet<UserChat> UsersChat { get; set; }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        public DbSet<Message> Messages { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Relationships
            builder
                .Entity<Message>()
                .HasOne<UserChat>(message => message.Writter)
                .WithMany(user => user.MessagesList)
                .HasForeignKey(user => user.UserId);
            #endregion
        }
    }
}