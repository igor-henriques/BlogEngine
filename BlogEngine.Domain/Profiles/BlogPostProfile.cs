namespace BlogEngine.Domain.Profiles;

public sealed class BlogPostProfile  : Profile
{
    public BlogPostProfile()
    {
        CreateMap<CreateBlogPostCommand, BlogPost>()
           .ForMember(dest => dest.LastUpdateDateTime, opt => opt.Ignore())
           .ForMember(dest => dest.Author, opt => opt.Ignore())
           .ForMember(dest => dest.Status, opt => opt.Ignore())
           .ForMember(dest => dest.AuthorId, opt => opt.Ignore());

        CreateMap<BlogPost, BlogPostDto>();        
    }
}