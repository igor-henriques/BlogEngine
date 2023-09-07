namespace BlogEngine.Infrastructure.Repository.DataContext.EntityMapping;

public sealed class BlogEditorCommentFluentMapping : IEntityTypeConfiguration<BlogEditorComment>
{
    public void Configure(EntityTypeBuilder<BlogEditorComment> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Content).IsRequired();
        builder.Property(e => e.PublishDateTime).IsRequired();
        builder.HasOne(e => e.BlogPost).WithMany().HasForeignKey(e => e.BlogPostId).OnDelete(DeleteBehavior.NoAction);
    }
}
