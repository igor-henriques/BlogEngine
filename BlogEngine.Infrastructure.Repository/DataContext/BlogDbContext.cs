namespace BlogEngine.Infrastructure.Repository.DataContext;

public sealed class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }
    public BlogDbContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<User> Users { get; init; }
    public DbSet<BlogPost> BlogPosts { get; init; }
    public DbSet<BlogComment> BlogComments { get; init; }
    public DbSet<BlogEditorComment> BlogEditorComments { get; init; }

    public const string USERS_TABLE_NAME = nameof(Users);
    public const string BLOG_POSTS_TABLE_NAME = nameof(BlogPosts);
    public const string BLOG_COMMENTS_TABLE_NAME = nameof(BlogComments);
    public const string BLOG_EDITORS_COMMENTS_TABLE_NAME = nameof(BlogEditorComments);
}
