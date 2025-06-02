using LibraryAPI.Dto.BookCopy;
using LibraryAPI.Model;

namespace LibraryAPI.Mapper
{
    public static class BookCopyMapper
    {
        public static BookCopyDto ToBookCopyDto(this BookCopy bookCopy)
        {
            return new BookCopyDto
            {
                RecordId = bookCopy.RecordId,
                SerialNumber = bookCopy.SerialNumber,
                IsAvailable = bookCopy.IsAvailable,
                CreateDate = bookCopy.CreateDate,
                ModifiedDate = bookCopy.ModifiedDate,
                BookId = bookCopy.BookId,
                BookTitle = bookCopy.Book?.Title
            };
        }

        public static BookCopy ToBookCopyFromCreateDto(this CreateBookCopyDto model)
        {
            return new BookCopy
            {
                BookId = model.BookId
            };
        }
    }
}
