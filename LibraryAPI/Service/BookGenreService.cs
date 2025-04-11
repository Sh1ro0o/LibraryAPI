using LibraryAPI.Common;
using LibraryAPI.Dto.BookAuthor;
using LibraryAPI.Dto.BookGenre;
using LibraryAPI.Interface.Service;
using LibraryAPI.Mapper;
using LibraryAPI.Model;
using LibraryAPI.UnitOfWork;

namespace LibraryAPI.Service
{
    public class BookGenreService : IBookGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookGenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<IEnumerable<BookGenreDto>?>> UpdateBookGenre(SaveBookGenreDto model)
        {
            //Check if Book exists
            var existingBook = await _unitOfWork.BookRepository.GetById(model.BookId);
            if (existingBook == null)
            {
                return OperationResult<IEnumerable<BookGenreDto>?>.NotFound(message: $"Book with Id: {model.BookId} not found!");
            }

            var existingGenres = await _unitOfWork.GenreRepository.CheckIfGenresExist(model.GenreIds);
            bool genresExist = existingGenres.Count == model.GenreIds.Count;
            if (!genresExist)
            {
                return OperationResult<IEnumerable<BookGenreDto>?>.NotFound(message: $"Genre Not Found!");
            }

            //Get existing connections
            var existingBookGenres = await _unitOfWork.BookGenreRepository.GetByBookId(model.BookId);
            var existingBookGenreIds = existingBookGenres.Select(x => x.GenreId).ToList();

            //BookGenres to add
            var bookGenresToAdd = model.GenreIds
                .Where(x => !existingBookGenreIds.Contains(x))
                .Select(x => new BookGenre
                {
                    BookId = model.BookId,
                    GenreId = x
                }).ToList();

            //BookGenres to delete
            var bookGenresToDelete = existingBookGenres
                .Where(x => !model.GenreIds.Contains(x.GenreId))
                .ToList();

            await _unitOfWork.BookGenreRepository.AddRange(bookGenresToAdd);
            _unitOfWork.BookGenreRepository.DeleteRange(bookGenresToDelete);

            await _unitOfWork.Commit();

            //Return new connections
            var result = await _unitOfWork.BookGenreRepository.GetByBookId(model.BookId);
            var bookGenreDtos = result.Select(x => x.ToBookGenreDto());
            return OperationResult<IEnumerable<BookGenreDto>?>.Success(bookGenreDtos);
        }
    }
}
