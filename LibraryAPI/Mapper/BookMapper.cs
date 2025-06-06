﻿using LibraryAPI.Dto.Author;
using LibraryAPI.Dto.Book;
using LibraryAPI.Dto.Genre;
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
                PublishDate = book.PublishDate,
                ISBN = book.ISBN,
                Description = book.Description,
                Authors = book.BookAuthors?
                .Where(x => x.Author != null)
                .Select(x => new AuthorDto
                {
                    RecordId = x.Author!.RecordId,
                    FirstName = x.Author.FirstName,
                    LastName = x.Author.LastName
                })
                .ToList() ?? new List<AuthorDto>(),
                Genres = book.BookGenres?
                .Where(x => x.Genre != null)
                .Select(x => new GenreDto
                {
                    RecordId = x.Genre!.RecordId,
                    Name = x.Genre.Name,
                    Description = x.Genre.Description
                }).ToList() ?? new List<GenreDto>()
            };
        }

        public static Book ToBookFromCreateDto(this CreateBookDto data)
        {
            return new Book
            {
                Title = data.Title,
                PublishDate = data.PublishDate,
                ISBN = data.ISBN,
                Description = data.Description
            };
        }
    }
}
