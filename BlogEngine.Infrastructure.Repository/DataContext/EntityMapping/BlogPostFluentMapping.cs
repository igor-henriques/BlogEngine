namespace BlogEngine.Infrastructure.Repository.DataContext.EntityMapping;

public sealed class BlogPostFluentMapping : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Title).IsRequired().HasMaxLength(Constants.FieldsDefinitions.MaxLengthPostTitle);
        builder.Property(b => b.Content).IsRequired();
        builder.Property(b => b.PublishDate);
        builder.Property(b => b.LastUpdateDateTime);
        builder.Property(b => b.Status).IsRequired().HasConversion<int>(); ;
        builder.HasOne(b => b.Author).WithMany().HasForeignKey(b => b.AuthorId).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(b => b.Comments).WithOne(c => c.BlogPost).HasForeignKey(c => c.BlogPostId).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(b => b.EditorComments).WithOne(c => c.BlogPost).HasForeignKey(c => c.BlogPostId).OnDelete(DeleteBehavior.NoAction);
    }
}