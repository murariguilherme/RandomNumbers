using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Random_Numbers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Random_Numbers.Data.Mappings
{
    public class UserMappings : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnType("varchar(100)")
                .IsRequired();            

            builder.ToTable("Users");
        }
    }
}
