using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Random_Numbers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Random_Numbers.Data.Mappings
{
    public class PlayerMappings : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Number)
                .IsRequired();

            builder.HasOne(p => p.User);

            builder.ToTable("Players");
        }
    }
}
