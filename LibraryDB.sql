create database VISLibraryDB;
GO

use VISLibraryDB;
GO

--Tables
create table Books (
    BookId INT PRIMARY KEY IDENTITY(1, 1),          -- ISBN system
    Title VARCHAR (255) not null,
    Author VARCHAR (255) not null,
    PublicationYear INT,
    Genre VARCHAR (50),
    Availability int DEFAULT 0,
    OwnerName VARCHAR (255)                         --Books that have a owner
)

create table Students (
    StudentId INT PRIMARY KEY IDENTITY(1, 1),       -- Auto-increment ID
    Name varchar (255) NOT NULL,
    Email varchar (255) unique NOT NULL,
    Join_Date Date
)

create table BorrowedBooks (
    BorrowId INT PRIMARY KEY IDENTITY(1,1),         -- Auto-increment ID
    BookId VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES Books(BookId),
    StudentId INT NOT NULL FOREIGN KEY REFERENCES Students(StudentId),
    BorrowDate DATE NOT NULL DEFAULT GETDATE(),
    DueDate DATE NOT NULL,
    ReturnDate DATE,
    Status NVARCHAR(20) DEFAULT 'Borrowed'
)

CREATE TABLE MissingBooks (
    MissingId INT IDENTITY(1,1) PRIMARY KEY,        -- Auto-increment ID
    MissingBookId VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES Books(BookId),
    ReportDate DATE DEFAULT GETDATE(),
    Status VARCHAR(20) DEFAULT 'Missing',           -- Values: 'Missing' or 'Found'
);

CREATE TABLE Staff (
    StaffID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Role NVARCHAR(50),                              -- Values: 'Admin', 'Librarian'
    Username NVARCHAR(50) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL             -- Hashed for security
);

-- Ensure ISBN is unique for each book
ALTER TABLE Books ADD CONSTRAINT UQ_ISBN UNIQUE (ISBN);

-- Ensure Email is unique for each member
ALTER TABLE Students ADD CONSTRAINT UQ_Email UNIQUE (Email);

--Insert into
-- Books (Book_Id, Title, Author, Published_Year, Genre, Available_Copies) values 
-- (00551, 'The_Great_Gatsby', 'F_Scott_Fitzgerald', '19250410', 'Tragedy', '10000')




