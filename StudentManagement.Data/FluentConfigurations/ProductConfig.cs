using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagement.Data.Models;

namespace StudentManagement.Data.FluentConfigurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Set table name
            builder.ToTable("Products");
            // Configure primary key
            builder.HasKey(s => s.Id);

            // Configure other properties if needed
            builder.Property(s => s.Name)
                .IsRequired();

        }
    }
}
