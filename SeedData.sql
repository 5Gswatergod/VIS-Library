-- SeedData.sql: Sample data for Library Management System

USE VISLibraryDB;
GO

-- Insert sample books
INSERT INTO Books (BookID, Title, Author, Genre, PublicationYear, Availability)
VALUES
('9780131101630', 'The C Programming Language', 'Brian W. Kernighan and Dennis M. Ritchie', 'Programming', 1978, 5),
('9780596009205', 'Head First Design Patterns', 'Eric Freeman and Elisabeth Robson', 'Software Design', 2004, 3),
('9781449331818', 'Learning Python', 'Mark Lutz', 'Programming', 2013, 4),
('9780321125217', 'Refactoring', 'Martin Fowler', 'Software Engineering', 1999, 2),
('9780132350884', 'Clean Code', 'Robert C. Martin', 'Programming', 2008, 6);

-- Insert sample students
INSERT INTO Students (StudetID, Name, Email)
VALUES
('', 'John Doe', 'john.doe@example.com'),
('', 'Jane Smith', 'jane.smith@example.com'),
('', 'Alice Johnson', 'alice.johnson@example.com'),
('', 'Bob Brown', 'bob.brown@example.com'),
('', 'Eve Davis', 'eve.davis@example.com');

-- Insert sample borrowed books
INSERT INTO BorrowedBooks (BookID, StudentID, BorrowDate, ReturnDate, Status)
VALUES
('9780131101630', 1, '2025-01-01', NULL, 'Borrowed'),
('9780596009205', 2, '2025-01-03', '2025-01-10', 'Returned'),
('9781449331818', 3, '2025-01-05', NULL, 'Borrowed'),
('9780321125217', 4, '2025-01-07', NULL, 'Borrowed'),
('9780132350884', 5, '2025-01-09', '2025-01-15', 'Returned');
