using LibraryAPI.Common;
using LibraryAPI.Dto.BorrowingTransaction;
using LibraryAPI.Filters;
using LibraryAPI.Interface.Service;
using LibraryAPI.Mapper;
using LibraryAPI.Model;
using LibraryAPI.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Service
{
    public class BorrowingTransactionService : IBorrowingTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public BorrowingTransactionService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<OperationResult<IEnumerable<BorrowingTransactionDto>>> GetAll(BorrowingTransactionFilter filter)
        {
            var borrowingTransactions = await _unitOfWork.BorrowingTransactionRepository.GetAll(filter);

            var borrowingTransactionsDto = borrowingTransactions.Select(x => x.ToBorrowingTransactionDto());

            return OperationResult<IEnumerable<BorrowingTransactionDto>>.Success(borrowingTransactionsDto);
        }

        public async Task<OperationResult<BorrowingTransactionDto?>> GetById(int id)
        {
            var borrowingTransaction = await _unitOfWork.BorrowingTransactionRepository.GetById(id);

            if (borrowingTransaction == null)
            {
                return OperationResult<BorrowingTransactionDto?>.NotFound(message: $"Borrowing Transaction with Id: {id} not Found!");
            }

            return OperationResult<BorrowingTransactionDto?>.Success(borrowingTransaction.ToBorrowingTransactionDto());
        }

        public async Task<OperationResult<BorrowingTransactionDto?>> CreateBorrowingTransaction(CreateBorrowingTransactionDto model)
        {
            //Check if User exists
            var existingUser = await _userManager.FindByIdAsync(model.UserId);
            if (existingUser == null)
            {
                return OperationResult<BorrowingTransactionDto?>.NotFound(message: $"User with Id: {model.UserId} Not Found!");
            }

            //Check if BookCopy exists
            var existingBookCopy = await _unitOfWork.BookCopyRepository.GetById(model.BookCopyId);
            if (existingBookCopy == null)
            {
                return OperationResult<BorrowingTransactionDto?>.NotFound(message: $"BookCopy with Id: {model.BookCopyId} Not Found!");
            }

            //Set remaining BorrowingTransaction properties
            var newBorrowingTransaction = new BorrowingTransaction
            {
                BorrowDate = DateTime.UtcNow,
                DueDate = model.DueDate,
                ReturnedDate = null,
                IsReturned = false,
                UserId = existingUser.Id,
                BookCopyId = existingBookCopy.RecordId,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = null
            };

            //Create and Commit
            await _unitOfWork.BorrowingTransactionRepository.Create(newBorrowingTransaction);
            await _unitOfWork.Commit();

            //Return
            return OperationResult<BorrowingTransactionDto?>.Success(newBorrowingTransaction.ToBorrowingTransactionDto());
        }

        public async Task<OperationResult<BorrowingTransactionDto?>> UpdateBorrowingTransaction(SaveBorrowingTransactionDto model)
        {
            //Find existing BorrowingTransaction
            var borrowingTransaction = await _unitOfWork.BorrowingTransactionRepository.GetById(model.RecordId);
            if (borrowingTransaction == null)
            {
                return OperationResult<BorrowingTransactionDto?>.NotFound(message: $"BorrowingTransaction with Id: {model.RecordId} Not Found!");
            }

            //Check if new UserId exists
            var existingUser = await _userManager.FindByIdAsync(model.UserId);
            if (existingUser == null)
            {
                return OperationResult<BorrowingTransactionDto?>.NotFound(message: $"User with Id: {model.UserId} Not Found!");
            }

            //Check if new BookCopyId exists
            var existingBookCopy = await _unitOfWork.BookCopyRepository.GetById(model.BookCopyId);
            if (existingBookCopy == null)
            {
                return OperationResult<BorrowingTransactionDto?>.NotFound(message: $"BookCopy with Id: {model.BookCopyId} Not Found!");
            }

            //Set BorrowingTransaction properties
            borrowingTransaction.UserId = model.UserId;
            borrowingTransaction.BookCopyId = model.BookCopyId;
            borrowingTransaction.ModifiedDate = DateTime.UtcNow;
            borrowingTransaction.DueDate = model.DueDate;
            borrowingTransaction.ReturnedDate = model?.ReturnedDate;

            //Check if book being returned
            if (borrowingTransaction.ReturnedDate is DateTime returnedDate)
            {
                borrowingTransaction.IsReturned = true;
            }
            else
            {
                borrowingTransaction.IsReturned = false;
            }

            //Update and Commit
            _unitOfWork.BorrowingTransactionRepository.Update(borrowingTransaction);
            await _unitOfWork.Commit();

            //Return
            return OperationResult<BorrowingTransactionDto?>.Success(borrowingTransaction.ToBorrowingTransactionDto());
        }

        public async Task<OperationResult<bool>> DeleteBorrowingTransaction(int id)
        {
            //Find existing BorrowingTransaction
            var borrowingTransaction = await _unitOfWork.BorrowingTransactionRepository.GetById(id);
            if (borrowingTransaction == null)
            {
                return OperationResult<bool>.NotFound(data: false, message: $"BorrowingTransaction with Id: {id} Not Found!");
            }

            //Delete and Commit
            _unitOfWork.BorrowingTransactionRepository.Delete(borrowingTransaction);
            await _unitOfWork.Commit();

            //Return
            return OperationResult<bool>.Success(data: true);
        }
    }
}
