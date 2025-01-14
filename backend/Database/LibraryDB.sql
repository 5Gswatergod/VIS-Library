create database VISLibraryDB;
GO

use VISLibraryDB;
GO

--Tables
CREATE TABLE Users(
    UserID INT PRIMARY KEY IDENTITY(1, 1),          -- Auto-increment ID
    Name NVARCHAR(100) NOT NULL,
    UserType NVARCHAR(50) NOT NULL,                 -- e.g., 'Student', 'Staff', or 'Librarian'
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL
);

create table Books (
    BookID INT PRIMARY KEY IDENTITY(1, 1),          -- ISBN system
    Title VARCHAR (255) not null,
    Author VARCHAR (255) not null,
    PublishedYear INT,
    Genre VARCHAR (50),
    Quantity int DEFAULT 0,
    OwnerName VARCHAR (255)                         --Books that have a owner
)

CREATE TABLE Accounts (
    AccountID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    no_borrowed_books INT DEFAULT 0,
    no_reserved_books INT DEFAULT 0,
    no_returned_books INT DEFAULT 0,
    no_lost_books INT DEFAULT 0,
    fine_amount DECIMAL(10, 2) DEFAULT 0.00,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE BorrowedBooks (
    BorrowID INT PRIMARY KEY IDENTITY(1,1),         -- Auto-increment ID
    BookID VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES Books(BookId),
    StudentID INT NOT NULL FOREIGN KEY REFERENCES Students(StudentId),
    BorrowDate DATE NOT NULL DEFAULT GETDATE(),
    DueDate DATE NOT NULL,
    ReturnDate DATE,
    Status NVARCHAR(20) DEFAULT 'Borrowed'
)

CREATE TABLE LibraryDatabase (
    ListID INT PRIMARY KEY IDENTITY(1,1),
    BookID NVARCHAR(13) NOT NULL,
    FOREIGN KEY (BookID) REFERENCES Books(BookID)
);

CREATE TABLE Librarians (
    LibrarianID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    SearchString NVARCHAR(200),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Ensure ISBN is unique for each book
ALTER TABLE Books ADD CONSTRAINT UQ_ISBN UNIQUE (ISBN);

-- Ensure Email is unique for each member
ALTER TABLE Students ADD CONSTRAINT UQ_Email UNIQUE (Email);

--Insert into
-- Books (Book_Id, Title, Author, Published_Year, Genre, Available_Copies) values 
-- (00551, 'The_Great_Gatsby', 'F_Scott_Fitzgerald', '19250410', 'Tragedy', '10000')




