using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    internal class URLTokenConfiguration : IEntityTypeConfiguration<URLToken>
    {
        public void Configure(EntityTypeBuilder<URLToken> builder)
        {
            builder.HasIndex(u => u.Token).IsUnique();
        }
    }
}
