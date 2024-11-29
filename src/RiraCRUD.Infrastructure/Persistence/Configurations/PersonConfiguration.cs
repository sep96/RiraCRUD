
namespace RiraCRUD.Infrastructure.Persistence.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {   
            // Define table name
            builder.ToTable("Persons");

            // Primary key
            builder.HasKey(p => p.Id);

            // Columns configuration
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.NationalId)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false); // Equivalent to CHAR(10)

            builder.Property(p => p.DateOfBirth)
                .IsRequired();

            // Unique constraint on NationalId
            builder.HasIndex(p => p.NationalId)
                .IsUnique();
             
        }
    }
}
