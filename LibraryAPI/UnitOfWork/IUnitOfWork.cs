﻿using LibraryAPI.Interface;
using LibraryAPI.Interface.Repository;

namespace LibraryAPI.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        IBookAuthorRepository BookAuthorRepository { get; }
        IBookCopyRepository BookCopyRepository { get; }
        IGenreRepository GenreRepository { get; }
        IBookGenreRepository BookGenreRepository { get; }

        Task<int> Commit();
    }
}
