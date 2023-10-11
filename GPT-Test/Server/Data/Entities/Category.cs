using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPT_Test.Server.Data.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }

        public ICollection<Product> Products { get; set; } = null!;
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryID);

            builder.Property(c => c.CategoryName)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(c => c.Description)
                .HasMaxLength(4000);

            builder.Property(c => c.Picture)
                .HasColumnType("image");
        }
    }
}