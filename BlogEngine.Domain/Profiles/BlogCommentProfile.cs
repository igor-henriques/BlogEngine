namespace BlogEngine.Domain.Profiles;

public sealed class BlogCommentProfile : Profile
{
    public BlogCommentProfile()
    {
        CreateMap<BlogComment, BlogCommentDto>();
        CreateMap<BlogComment, BlogCommentForBlogPostDto>();
        CreateMap<CreateBlogCommentCommand, BlogComment>()
            .ForMember(dest => dest.BlogPost, opt => opt.Ignore());
    }
}
