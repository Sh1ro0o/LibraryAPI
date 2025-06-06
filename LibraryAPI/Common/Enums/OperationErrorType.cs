namespace LibraryAPI.Common.Enums
{
    public enum OperationErrorType
    {
        NotFound = 0,
        Conflict = 1,
        BadRequest = 2,
        UnAuthorized = 3,
        BadGateway = 4,
        InternalServerError = 99,
    }
}
