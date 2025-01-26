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
    internal class BorrowingConfigrations : IEntityTypeConfiguration<Borrowing>
    {
        public void Configure(EntityTypeBuilder<Borrowing> builder)
        {
      
            builder.Property(b => b.ReturnDate).HasDefaultValue(null);
        }
    }
}
