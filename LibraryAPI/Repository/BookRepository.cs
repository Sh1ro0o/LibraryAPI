﻿using LibraryAPI.Common.Response;
using LibraryAPI.Data;
using LibraryAPI.Filters;
using LibraryAPI.Interface;
using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResponse<Book>> GetAll(BookFilter filter)
        {
            var query = GetBooksFilteredInternal(filter);

            //Total before pagination
            var totalItems = await query.CountAsync();

            //Pagination
            if (filter.PageNumber.HasValue && filter.PageSize.HasValue)
            {
                int skip = (filter.PageNumber.Value - 1) * filter.PageSize.Value;
                query = query.Skip(skip).Take(filter.PageSize.Value);
            }

            var books = await query.ToListAsync();

            return new PaginatedResponse<Book>
            {
                Data = books,
                TotalItems = totalItems
            };
        }

        public async Task<Book?> GetOne(BookFilter filter)
        {
            return await GetBooksFilteredInternal(filter).FirstOrDefaultAsync();
        }

        public async Task<Book?> GetById(int id)
        {
            return await _context.Book
                .Include(x => x.BookAuthors).ThenInclude(x => x.Author)
                .Include(x => x.BookGenres).ThenInclude(x => x.Genre)
                .FirstOrDefaultAsync(x => x.RecordId == id);
        }

        public async Task<Book> Create(Book model)
        {
            await _context.Book.AddAsync(model);

            return model;
        }

        public void Update(Book book)
        {
            _context.Book.Update(book);
        }

        public void Delete(Book book)
        {
            _context.Book.Remove(book);
        }


        //Internal Methods
        private IQueryable<Book> GetBooksFilteredInternal(BookFilter filter)
        {
            var query = _context.Book.AsQueryable();

            // Apply filters based on the filter object
            if (filter.RecordId != null)
            {
                query = query.Where(x => x.RecordId == filter.RecordId);
            }

            if (!string.IsNullOrWhiteSpace(filter.Title))
            {
                query = query.Where(x => x.Title == filter.Title);
            }

            if (filter.PublishDate != null)
            {
                query = query.Where(x => x.PublishDate == filter.PublishDate);
            }

            if (!string.IsNullOrWhiteSpace(filter.ISBN))
            {
                query = query.Where(x => x.ISBN == filter.ISBN);
            }

            // Include authors if requested
            if (filter.IncludeAuthors)
            {
                query = query.Include(x => x.BookAuthors).ThenInclude(x => x.Author);
            }

            // Include genres if requested
            if (filter.IncludeGenres)
            {
                query = query.Include(x => x.BookGenres).ThenInclude(x => x.Genre);
            }

            // Apply sorting
            query = query.OrderByDescending(x => x.RecordId);

            return query;
        }
    }
}
