namespace BlogEngine.Core.Shared;

public static class Constants
{
    public readonly struct Routes
    {
        public readonly struct BlogPost
        {
            public const string BaseBlogPost = "api/v1/blog-post";

            public const string GetByStatus = $"{BaseBlogPost}/get-by-status";
            public const string GetByAuthor = $"{BaseBlogPost}/get-by-author";
            public const string GetPublishedPostsPaginated = $"{BaseBlogPost}/get-published-posts-paginated";
            public const string Create = $"{BaseBlogPost}/create";             
            public const string Put = $"{BaseBlogPost}/put";
            public const string PatchStatus = $"{BaseBlogPost}/update-status";
            public const string AddComment = $"{BaseBlogPost}/add-comment";
            public const string AddEditorComment = $"{BaseBlogPost}/add-editor-comment";
            public const string ReproveBlogPost = $"{BaseBlogPost}/reprove";
        }

        public readonly struct User
        {
            public const string BaseUser = "api/v1/user";
            public const string Create = $"{BaseUser}/create";
            public const string PatchPassword = $"{BaseUser}/update-password";
            public const string PatchRole = $"{BaseUser}/update-role";
            public const string Authenticate = $"{BaseUser}/authenticate";
        }

        public const string Health = "/health";
        public const string Swagger = "/swagger";
    }

    public readonly struct SwaggerTags
    {
        public const string BlogComment = nameof(BlogComment);
        public const string BlogEditorComment = nameof(BlogEditorComment);
        public const string BlogPost = nameof(BlogPost);        
        public const string User = nameof(User);
    }

    public readonly struct FieldsDefinitions
    {
        public const int MaxLengthName = 100;
        public const int MaxLengthHashedPassword = 256;
        public const int MaxLengthEmail = 255;
        public const int MaxLengthPostTitle = 255;
    }

    public readonly struct RoleNames
    {
        public const string Editor = nameof(EUserRole.Editor);
        public const string Writer = nameof(EUserRole.Writer);
    }
}