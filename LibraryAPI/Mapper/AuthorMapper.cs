using LibraryAPI.Dto.Author;
using LibraryAPI.Model;

namespace LibraryAPI.Mapper
{
    public static class AuthorMapper
    {
        public static AuthorDto ToAuthorDto(this Author author)
        {
            return new AuthorDto
            {
                RecordId = author.RecordId,
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }

        public static Author ToAuthorFromCreateDto(this CreateAuthorDto data)
        {
            return new Author
            {
                FirstName = data.FirstName,
                LastName = data.LastName
            };
        }
    }
}
