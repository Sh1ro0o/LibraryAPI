using LibraryAPI.Common;
using LibraryAPI.Dto.Author;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Mapper;
using LibraryAPI.UnitOfWork;

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

        public async Task<OperationResult<AuthorDto?>> CreateAuthor(CreateAuthorDto model)
        {
            //User must provide at least a first name or last name
            if (string.IsNullOrWhiteSpace(model.FirstName) && string.IsNullOrWhiteSpace(model.LastName))
            {
                return OperationResult<AuthorDto?>.BadRequest(message: "Please provide a First name or Last name for Author!");
            }

            var author = await _unitOfWork.AuthorRepository.CreateAuthor(model.ToAuthorFromCreateDto());
            await _unitOfWork.Commit();

            return OperationResult<AuthorDto?>.Success(data: author.ToAuthorDto());
        }

        public async Task<OperationResult<AuthorDto?>> UpdateAuthor(SaveAuthorDto model)
        {
            //Check if exists
            var existingAuthor = await _unitOfWork.AuthorRepository.GetById(model.RecordId);

            if (existingAuthor == null)
            {
                return OperationResult<AuthorDto?>.NotFound(message: $"Author with Id: {model.RecordId}: not found!");
            }

            existingAuthor.FirstName = model.FirstName;
            existingAuthor.LastName = model.LastName;

            _unitOfWork.AuthorRepository.UpdateAuthor(existingAuthor);
            await _unitOfWork.Commit();

            return OperationResult<AuthorDto?>.Success(data: existingAuthor.ToAuthorDto());
        }

        public async Task<OperationResult<bool>> DeleteAuthor(int id)
        {
            //Check if exists
            var existingAuthor = await _unitOfWork.AuthorRepository.GetById(id);

            if (existingAuthor == null)
            {
                return OperationResult<bool>.NotFound(data: false, message: $"Author with Id: {id} not found!");
            }

            _unitOfWork.AuthorRepository.DeleteAuthor(existingAuthor);
            await _unitOfWork.Commit();

            return OperationResult<bool>.Success(data: true);
        }
    }
}
