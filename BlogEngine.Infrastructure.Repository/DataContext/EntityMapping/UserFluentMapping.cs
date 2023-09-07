namespace BlogEngine.Infrastructure.Repository.DataContext.EntityMapping;

public sealed class UserFluentMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.HashedPassword).IsRequired().HasMaxLength(Constants.FieldsDefinitions.MaxLengthHashedPassword);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(Constants.FieldsDefinitions.MaxLengthEmail);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.Role).IsRequired().HasConversion<int>().HasMaxLength(6);
        builder.Property(u => u.Username).IsRequired().HasMaxLength(Constants.FieldsDefinitions.MaxLengthName);
    }
}
