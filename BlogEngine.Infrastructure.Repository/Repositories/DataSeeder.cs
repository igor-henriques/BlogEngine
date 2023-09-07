using BlogEngine.Domain.Commands.User.Create;

namespace BlogEngine.Infrastructure.Repository.Repositories;

public sealed class DataSeeder : IDataSeeder
{
    private readonly BlogDbContext _context;
    private readonly IOptions<InitialSeedDataOptions> _initDataOptions;
    private readonly IValidator<CreateUserCommand> _userDtoValidator;
    private readonly ILogger<DataSeeder> _logger;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly IMapper _mapper;

    public DataSeeder(BlogDbContext context,
                      IOptions<InitialSeedDataOptions> initDataOptions,
                      IValidator<CreateUserCommand> userDtoValidator,
                      ILogger<DataSeeder> logger,
                      IPasswordHashingService passwordHashingService,
                      IMapper mapper)
    {
        _context = context;
        _initDataOptions = initDataOptions;
        _userDtoValidator = userDtoValidator;
        _logger = logger;
        _passwordHashingService = passwordHashingService;
        _mapper = mapper;
    }

    public async Task SeedFakeDataAsync()
    {
        await SeedInitialProductionDataAsync();

        if (await AreAllTablesEmpty(_context))
        {
            Log.Logger.Information("{sourceName} already has data. No need to seed.", nameof(BlogDbContext));
            return;
        }

        Log.Logger.Information("{DataSeeder} already has no data. Start seeding.", nameof(DataSeeder));

        var userFaker = new Faker<User>()
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.HashedPassword, f => f.Internet.Password())
            .RuleFor(u => u.Role, f => f.PickRandom<EUserRole>());

        var users = userFaker.Generate(500);

        _context.Users.AddRange(users);
        await _context.SaveChangesAsync();

        var blogPostFaker = new Faker<BlogPost>()
            .RuleFor(bp => bp.Title, f => f.Lorem.Sentence())
            .RuleFor(bp => bp.Content, f => f.Lorem.Paragraph())
            .RuleFor(bp => bp.PublishDate, f => f.Date.Past(2))
            .RuleFor(bp => bp.LastUpdateDateTime, f => f.Date.Past(1))
            .RuleFor(bp => bp.Status, f => f.PickRandom<EPublishStatus>())
            .RuleFor(bp => bp.AuthorId, f => f.PickRandom(users).Id);

        var blogPosts = blogPostFaker.Generate(500);
        _context.BlogPosts.AddRange(blogPosts);

        await _context.SaveChangesAsync();

        var commentFaker = new Faker<BlogComment>()
            .RuleFor(c => c.Content, f => f.Lorem.Sentence())
            .RuleFor(c => c.PublishDateTime, f => f.Date.Past(2))
            .RuleFor(c => c.Username, f => f.Internet.UserName())
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.BlogPostId, f => f.PickRandom(blogPosts.Where(bp => bp.Status.HasFlag(EPublishStatus.Published))).Id);

        var editorCommentFaker = new Faker<BlogEditorComment>()
            .RuleFor(c => c.Content, f => f.Lorem.Sentence())
            .RuleFor(c => c.PublishDateTime, f => f.Date.Past(2))
            .RuleFor(c => c.EditorId, f => f.PickRandom(users.Where(u => u.Role.HasFlag(EUserRole.Editor)).ToList()).Id)
            .RuleFor(c => c.BlogPostId, f => f.PickRandom(blogPosts.Where(bp => bp.Status.HasFlag(EPublishStatus.Reproved)).ToList()).Id);

        var editorComments = editorCommentFaker.Generate(500);
        var comments = commentFaker.Generate(2000);
        
        _context.BlogComments.AddRange(comments);
        _context.BlogEditorComments.AddRange(editorCommentFaker);

        await _context.SaveChangesAsync();        
    }

    public async Task SeedInitialProductionDataAsync()
    {
        await AddDefaultUser(_initDataOptions.Value.FirstEditorUser);
        await AddDefaultUser(_initDataOptions.Value.FirstWriterUser);        

        async Task AddDefaultUser(CreateUserCommand user)
        {
            var validationResult = await _userDtoValidator.ValidateAsync(user);

            if (!validationResult.IsValid)
            {
                var errors = string.Join("\n", validationResult.Errors);
                _logger.LogError("Error while seeding production initial data: {errors}", errors);
                throw new Exception(errors);
            }

            var mappedUser = _mapper.Map<User>(user) with
            {
                HashedPassword = _passwordHashingService.HashPassword(user.Password)
            };

            if (await _context.Users.AnyAsync(x => x.Username == mappedUser.Username
                                                && x.Email == mappedUser.Email
                                                && x.Role == mappedUser.Role))
            {
                Log.Logger.Information("Initial user already exists. Skipping.");
                return;
            }

            _context.Users.Add(mappedUser);

            var wasUserAdded = await _context.SaveChangesAsync() >= 1;

            if (!wasUserAdded)
            {
                throw new Exception("Error while seeding production initial data. Initial user couldn't be added.");
            }

        }
    }

    private static async Task<bool> AreAllTablesEmpty(DbContext context)
    {
        int totalRecords = 0;

        using var command = context.Database.GetDbConnection().CreateCommand();

        command.CommandText = @$"
                SELECT COUNT(*) FROM {BlogDbContext.USERS_TABLE_NAME}
                UNION ALL
                SELECT COUNT(*) FROM {BlogDbContext.BLOG_POSTS_TABLE_NAME}
                UNION ALL
                SELECT COUNT(*) FROM {BlogDbContext.BLOG_COMMENTS_TABLE_NAME}
                UNION ALL
                SELECT COUNT(*) FROM {BlogDbContext.BLOG_EDITORS_COMMENTS_TABLE_NAME}
                ";

        context.Database.OpenConnection();

        using var result = await command.ExecuteReaderAsync();

        while (result.Read())
        {
            totalRecords += result.GetInt32(0);
        }

        return totalRecords != 1;
    }
}
