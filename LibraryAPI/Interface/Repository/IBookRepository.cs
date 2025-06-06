﻿using LibraryAPI.Common.Response;
using LibraryAPI.Filters;
using LibraryAPI.Model;

namespace LibraryAPI.Interface
{
    public interface IBookRepository
    {
        Task<PaginatedResponse<Book>> GetAll(BookFilter filter);
        Task<Book?> GetOne(BookFilter filter);
        Task<Book?> GetById(int id);
        Task<Book> Create(Book model);
        void Update(Book book);
        void Delete(Book book);
    }
}
