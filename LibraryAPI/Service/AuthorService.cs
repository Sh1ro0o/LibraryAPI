using LibraryAPI.Common;
using LibraryAPI.Dto.Author;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Mapper;
using LibraryAPI.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<IEnumerable<AuthorDto>>> GetAll(AuthorFilter authorFilter)
        {
            var authors = await _unitOfWork.AuthorRepository.GetAll(authorFilter);

            var authorsDto = authors.Select(x => x.ToAuthorDto());

            return OperationResult<IEnumerable<AuthorDto>>.Success(data: authorsDto);
        }

        public Task<OperationResult<AuthorDto?>> CreateAuthor(CreateAuthorDto model)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<AuthorDto?>> UpdateAuthor(SaveAuthorDto model)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<bool>> DeleteAuthor(int id)
        {
            throw new NotImplementedException();
        }
    }
}
