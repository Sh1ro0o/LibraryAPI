using LibraryAPI.Dto.BorrowingTransaction;
using LibraryAPI.Filters;
using LibraryAPI.Model;

namespace LibraryAPI.Interface.Repository
{
    public interface IBorrowingTransactionRepository
    {
        Task<ICollection<BorrowingTransaction>> GetAll(BorrowingTransactionFilter filter);
        Task<BorrowingTransaction?> GetOne(BorrowingTransactionFilter filter);
        Task<SeperateTransactionsCountDto> GetSeperateTransactionsCount(SeperateTransactionsCountFilter filter);
        Task<BorrowingTransaction?> GetById(int id);
        Task<BorrowingTransaction> Create(BorrowingTransaction model);
        void Update(BorrowingTransaction model);
        void Delete(BorrowingTransaction model);
    }
}
