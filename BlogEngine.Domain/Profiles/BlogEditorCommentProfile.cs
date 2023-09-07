namespace BlogEngine.Domain.Profiles;

public sealed class BlogEditorCommentProfile : Profile
{
    public BlogEditorCommentProfile()
    {
        CreateMap<BlogEditorComment, BlogEditorCommentDto>();
        CreateMap<BlogEditorComment, BlogEditorCommentForBlogPostDto>();

        CreateMap<CreateBlogEditorCommentCommand, BlogEditorComment>()
            .ForMember(src => src.Editor, opt => opt.Ignore())
            .ForMember(src => src.BlogPost, opt => opt.Ignore())
            .ForMember(src => src.EditorId, opt => opt.Ignore());
    }
}
