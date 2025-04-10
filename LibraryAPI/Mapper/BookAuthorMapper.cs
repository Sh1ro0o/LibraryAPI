using LibraryAPI.Dto.BookAuthor;
using LibraryAPI.Model;

namespace LibraryAPI.Mapper
{
    public static class BookAuthorMapper
    {
        public static BookAuthorDto ToBookAuthorDto(this BookAuthor bookAuthor)
        {
            return new BookAuthorDto
            {
                BookId = bookAuthor.BookId,
                AuthorId = bookAuthor.AuthorId,
            };
        }
    }
}
