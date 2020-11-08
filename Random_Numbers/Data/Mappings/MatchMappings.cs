using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Random_Numbers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Random_Numbers.Data.Mappings
{
    public class MatchMappings : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasOne(m => m.PlayerOne)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.PlayerTwo)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Matchs");
        }
    }
}
