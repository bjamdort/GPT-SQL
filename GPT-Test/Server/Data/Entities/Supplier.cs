using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPT_Test.Server.Data.Entities
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? ContactName { get; set; }
        public string? ContactTitle { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? HomePage { get; set; }

        public ICollection<Product> Products { get; set; } = null!;
    }

    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(s => s.SupplierID);

            builder.Property(s => s.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(s => s.ContactName)
                .HasMaxLength(30);

            builder.Property(s => s.ContactTitle)
                .HasMaxLength(30);

            builder.Property(s => s.Address)
                .HasMaxLength(60);

            builder.Property(s => s.City)
                .HasMaxLength(15);

            builder.Property(s => s.Region)
                .HasMaxLength(15);

            builder.Property(s => s.PostalCode)
                .HasMaxLength(10);

            builder.Property(s => s.Country)
                .HasMaxLength(15);

            builder.Property(s => s.Phone)
                .HasMaxLength(24);

            builder.Property(s => s.Fax)
                .HasMaxLength(24);

            builder.Property(s => s.HomePage)
                .HasColumnType("ntext");
        }
    }
}