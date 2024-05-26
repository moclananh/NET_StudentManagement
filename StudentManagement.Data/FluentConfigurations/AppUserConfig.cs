using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data.Models;

namespace StudentManagement.Data.FluentConfigurations
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // Set table name
            builder.ToTable("AppUsers");
            // Configure primary key
            builder.HasKey(s => s.Id);

            // Configure other properties if needed
            builder.Property(s => s.UserName)
                .IsRequired();

            builder.Property(s => s.Password)
                .IsRequired();

            builder.Property(s => s.Email)
                .IsRequired();

        }
    }
}
