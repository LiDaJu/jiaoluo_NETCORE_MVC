using BooksApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BooksApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        //获取所有信息
        public List<Book> Get() =>
            _books.Find(book => true).ToList();

        //获取指定信息
        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        //新增
        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        //更新
        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        //删除根据指定类
        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        //删除根据指定id
        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);
    }
}