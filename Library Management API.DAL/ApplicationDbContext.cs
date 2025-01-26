using Library_Management_API.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.DAL
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
    
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) { }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Book>().HasMany(b => b.Borrowings).WithOne(B => B.Book).HasForeignKey(B => B.BookId);
            modelBuilder.Entity<Member>().HasMany(m => m.Borrowings).WithOne(B => B.Member).HasForeignKey(B => B.MemberId);
            modelBuilder.Entity<Borrowing>().HasKey(b => new { b.MemberId, b.BookId });
           
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
    }
}
