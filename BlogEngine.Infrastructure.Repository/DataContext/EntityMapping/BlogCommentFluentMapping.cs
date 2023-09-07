namespace BlogEngine.Infrastructure.Repository.DataContext.EntityMapping;

public sealed class BlogCommentFluentMapping : IEntityTypeConfiguration<BlogComment>
{
    public void Configure(EntityTypeBuilder<BlogComment> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Content).IsRequired();
        builder.Property(e => e.PublishDateTime).IsRequired();        
        builder.Property(e => e.Username).HasMaxLength(Constants.FieldsDefinitions.MaxLengthName);
        builder.Property(e => e.Email).HasMaxLength(Constants.FieldsDefinitions.MaxLengthEmail);
        builder.HasOne(c => c.BlogPost).WithMany(p => p.Comments).HasForeignKey(c => c.BlogPostId).OnDelete(DeleteBehavior.NoAction);
    }
}
