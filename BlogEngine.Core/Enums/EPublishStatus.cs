namespace BlogEngine.Core.Enums;

[Flags]
public enum EPublishStatus : byte
{    
    Submmited = 2,
    Approved = 4,
    Reproved = 8,
    Published = 16
}
