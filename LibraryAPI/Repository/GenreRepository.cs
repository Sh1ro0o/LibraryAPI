﻿using LibraryAPI.Data;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Repository;
using LibraryAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly LibraryDbContext _context;
        public GenreRepository(LibraryDbContext context)
        {
            _context = context;
        }

        #region GET Methods

        public async Task<List<Genre>> GetAll(GenreFilter filter)
        {
            return await GetGenresFilteredInternal(filter).ToListAsync();
        }

        public async Task<Genre?> GetOne(GenreFilter filter)
        {
            return await GetGenresFilteredInternal(filter).FirstOrDefaultAsync();
        }

        public async Task<Genre?> GetById(int id)
        {
            return await _context.Genre.FirstOrDefaultAsync(x => x.RecordId == id);
        }

        public async Task<List<Genre>> CheckIfGenresExist(List<int> genreIds)
        {
            var existingGenres = await _context.Genre
                .Where(x => genreIds.Contains(x.RecordId))
                .Distinct()
                .ToListAsync();

            return existingGenres;
        }

        #endregion

        #region CREATE Methods

        public async Task<Genre> Create(Genre model)
        {
            await _context.Genre.AddAsync(model);

            return model;
        }

        #endregion

        #region UPDATE Methods

        public void Update(Genre book)
        {
            _context.Genre.Update(book);
        }

        #endregion

        #region DELETE Methods

        public void Delete(Genre book)
        {
            _context.Genre.Remove(book);
        }

        #endregion

        #region PRIVATE Methods

        //Internal Methods
        private IQueryable<Genre> GetGenresFilteredInternal(GenreFilter filter)
        {
            var query = _context.Genre.AsQueryable();

            if (filter.RecordId != null)
            {
                query = query.Where(x => x.RecordId == filter.RecordId);
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(x => x.Name == filter.Name);
            }

            if (!string.IsNullOrWhiteSpace(filter.Description))
            {
                query = query.Where(x => x.Description == filter.Description);
            }

            if (filter.PageNumber != null && filter.PageSize != null)
            {
                query = query.Skip(filter.PageSize.Value * (filter.PageNumber.Value - 1));
                query = query.Take(filter.PageSize.Value);
            }

            return query;
        }

        #endregion
    }
}
