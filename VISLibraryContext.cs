using Microsoft.EntityFrameworkCore;
using VISLibraryManagementSystem.Models;

namespace VISLibraryManagementSystem.Data
{
    public class VISLibraryContext : DbContext
    {
        public VISLibraryContext(DbContextOptions<VISLibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<BorrowedBooks> BorrowedBooks { get; set; }
        public DbSet<MissingBook> MissingBooks { get; set; }
        public DbSet<Staff> Staffs { get; set; }
    }
}
