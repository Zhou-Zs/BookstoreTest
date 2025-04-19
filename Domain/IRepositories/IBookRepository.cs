using Bookstore.Domain.Entities;

namespace Bookstore.Domain.IRepositories
{
    public interface IBookRepository
    {
        /// <summary>
        /// 添加图书
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Task<Book> AddBookAsync(Book book);

        /// <summary>
        /// 根据图书ID获取图书
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Book?> GetBookByIdAsync(int id);

        /// <summary>
        /// 根据书名获取图书
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        Task<Book?> GetBookByTitleAsync(string title);

        /// <summary>
        /// 获取所有图书
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetAllBooksAsync();

        /// <summary>
        /// 更新图书
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        Task UpdateBookAsync(Book book);
    }
}
