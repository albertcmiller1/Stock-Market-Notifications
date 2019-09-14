using EmailAlert.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailAlert.Data
{
    public class EmailAlertDbContext : DbContext
    {
        public EmailAlertDbContext(DbContextOptions<EmailAlertDbContext> options)
            : base(options)
        {
        }

        public DbSet<Email> Emails { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //this allows us to enter some info into the db on dbcreation
            modelBuilder.Entity<Administrator>().HasData(
                new Administrator { Email = "albertcmiller1@gmail.com", Password = "22732235Fly", Id = 1 },
                new Administrator { Email = "alflyboymiller@yahoo.com", Password = "Password", Id = 2 });
        }
    }
}

//dotnet ef dbcontext info -s ..\EmailAlert.App\EmailAlert.App.csproj