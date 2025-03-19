using LibraryAPI.Interface;

namespace LibraryAPI.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }

        Task<int> Commit();
    }
}
