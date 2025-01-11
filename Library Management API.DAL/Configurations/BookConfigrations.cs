using Library_Management_API.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_API.DAL.Configurations
{
    internal class BookConfigrations : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Title).HasColumnType("varchar(20)");
            builder.Property(b => b.Author).HasColumnType("varchar(15)");
            builder.Property(b => b.Quantity).HasDefaultValue(0);
        }
    }
}
