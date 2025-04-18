﻿using Bookstore.Domain.Entities;

namespace Bookstore.Domain.IRepositories
{
    public interface IBookRepository
    {
        Task<Book> AddBookAsync(Book book);
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book?> GetBookByTitleAsync(string title);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task UpdateBookAsync(Book book);
    }
}
