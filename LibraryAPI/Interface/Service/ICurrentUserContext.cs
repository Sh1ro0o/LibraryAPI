namespace LibraryAPI.Interface.Service
{
    public interface ICurrentUserContext
    {
        string? UserId { get; }
        IReadOnlyList<string> Roles { get; }
    }
}
