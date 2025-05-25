using LibraryAPI.Common;
using LibraryAPI.Dto.Genre;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Mapper;
using LibraryAPI.UnitOfWork;

namespace LibraryAPI.Service
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;
        public GenreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<OperationResult<IEnumerable<GenreDto>>> GetAll(GenreFilter filter)
        {
            var paginatedGenres = await _unitOfWork.GenreRepository.GetAll(filter);

            var genresDto = paginatedGenres.Data.Select(x => x.ToGenreDto());

            return OperationResult<IEnumerable<GenreDto>>.Success(genresDto, paginatedGenres.TotalItems);
        }

        public async Task<OperationResult<GenreDto?>> GetById(int id)
        {
            var genre = await _unitOfWork.GenreRepository.GetById(id);

            if (genre == null)
            {
                return OperationResult<GenreDto?>.NotFound(message: $"Genre with Id: {id} not found!");
            }

            return OperationResult<GenreDto?>.Success(genre.ToGenreDto());
        }

        public async Task<OperationResult<GenreDto?>> CreateGenre(CreateGenreDto model)
        {
            var newGenre = model.ToGenreFromCreateDto();

            newGenre.Name = model.Name;
            newGenre.Description = model.Description;

            var result = await _unitOfWork.GenreRepository.Create(newGenre);
            await _unitOfWork.Commit();

            return OperationResult<GenreDto?>.Success(newGenre.ToGenreDto());
        }

        public async Task<OperationResult<GenreDto?>> UpdateGenre(SaveGenreDto model)
        {
            //Check if genre exists
            var existingGenre = await _unitOfWork.GenreRepository.GetById(model.RecordId);

            if (existingGenre == null)
            {
                return OperationResult<GenreDto?>.NotFound(message: "Genre Not Found!");
            }

            existingGenre.Name = model.Name;
            existingGenre.Description = model.Description;

            _unitOfWork.GenreRepository.Update(existingGenre);
            await _unitOfWork.Commit();

            return OperationResult<GenreDto?>.Success(existingGenre.ToGenreDto());
        }

        public async Task<OperationResult<bool>> DeleteGenre(int id)
        {
            //Check if genre exists
            var existingGenre = await _unitOfWork.GenreRepository.GetById(id);

            if (existingGenre == null)
            {
                return OperationResult<bool>.NotFound(message: "Genre Not Found!");
            }

            _unitOfWork.GenreRepository.Delete(existingGenre);
            await _unitOfWork.Commit();

            return OperationResult<bool>.Success(data: true);
        }
    }
}
