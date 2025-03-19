using LibraryAPI.Dto.Book;
using LibraryAPI.Model;

namespace LibraryAPI.Mapper
{
    public static class BookMapper
    {
        public static BookDto ToBookDto(this Book book)
        {
            return new BookDto
            {
                RecordId = book.RecordId,
                Title = book.Title,
                PublishDate = book?.PublishDate,
                ISBN = book?.ISBN,
                Description = book?.Description
            };
        }

        public static Book ToBookFromCreateDto(this CreateBookDto data)
        {
            return new Book
            {
                Title = data.Title,
                PublishDate = data?.PublishDate,
                ISBN = data?.ISBN,
                Description = data?.Description
            };
        }
    }
}
