using System.ComponentModel.DataAnnotations;

namespace VISLMS.Models
{
    public class LibraryDatabase
    {
        public List<Book> ListofBooks { get; set; } = new List<Book>();

        public void Add(Book book)
        {
            ListofBooks.Add(book);
        }

        public void Delete(string isbn)
        {
            var book = ListofBooks.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null)
            {
                ListofBooks.Remove(book);
            }
        }

        public void Update(string isbn, book updatedBook)
        {
            var book = ListofBooks.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null)
            {
                book.ISBN = updatedBook.ISBN;
                book.Title = updatedBook.Title;
                book.Author = updatedBook.Author;
                book.PublishYear = updatedBook.PublishYear;
                book.Genre = updatedBook.Genre;
                book.Quantity = updatedBook.Quantity;
                book.OwnerName = updatedBook.OwnerName;
            }
        }

        public List<Book> Display()
        {
            return ListofBooks;
        }

        public List<Book> Search(string searchString)
        {
            return ListofBooks.Where(b => b.Title.Contains(searchString) || b.Author.Contains(searchString)).ToList();
        }
    }
}