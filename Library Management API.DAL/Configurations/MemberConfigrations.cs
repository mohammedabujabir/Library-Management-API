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
    internal class MemberConfigrations : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(m => m.Name).HasColumnType("varchar(20)");
            builder.Property(m => m.MemberShipType).HasColumnName("ShipType");
        }
    }
}
