using LibraryAPI.Common;
using LibraryAPI.Dto.BorrowingTransaction;
using LibraryAPI.Filters;

namespace LibraryAPI.Interface.Service
{
    public interface IBorrowingTransactionService
    {
        Task<OperationResult<IEnumerable<BorrowingTransactionDto>>> GetAll(BorrowingTransactionFilter filter);
        Task<OperationResult<BorrowingTransactionDto?>> GetById(int id);
        Task<OperationResult<BorrowingTransactionDto?>> CreateBorrowingTransaction(CreateBorrowingTransactionDto model);
        Task<OperationResult<BorrowingTransactionDto?>> UpdateBorrowingTransaction(SaveBorrowingTransactionDto model);
        Task<OperationResult<bool>> DeleteBorrowingTransaction(int id);
    }
}
