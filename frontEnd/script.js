const sortBySelect = document.getElementById('sortBy');
const titleInput = document.getElementById('title');
const authorInput = document.getElementById('author');

const bookGrid = document.querySelector('.book-grid');

// Sample book data (replace with your actual data)
const books = [
    { title: "The Hitchhiker's Guide to the Galaxy", author: "Douglas Adams", cover: "https://images-na.ssl-images-amazon.com/images/I/5113Q43N2SL._SX328_BO1,204,203,200_.jpg" },
    { title: "Pride and Prejudice", author: "Jane Austen", cover: "https://images-na.ssl-images-amazon.com/images/I/51d644T5zJL._SX328_BO1,204,203,200_.jpg" },
    // ... add more books
];

function displayBooks(filteredBooks) {
    bookGrid.innerHTML = ''; // Clear previous results

    filteredBooks.forEach(book => {
        const bookCard = document.createElement('div');
        bookCard.classList.add('book-card');
        bookCard.innerHTML = `
            <img src="${book.cover}" alt="${book.title}">
            <h3>${book.title}</h3>
            <p>By: ${book.author}</p>
        `;
        bookGrid.appendChild(bookCard);
    });
}

function filterBooks() {
    let filteredBooks = [...books]; // Copy the original book array

    // Filter by title
    const titleFilter = titleInput.value.toLowerCase();
    if (titleFilter) {
        filteredBooks = filteredBooks.filter(book => book.title.toLowerCase().includes(titleFilter));
    }

    // Filter by author
    const authorFilter = authorInput.value.toLowerCase();
    if (authorFilter) {
        filteredBooks = filteredBooks.filter(book => book.author.toLowerCase().includes(authorFilter));
    }

    // Sort books
    const sortBy = sortBySelect.value;
    if (sortBy === 'date') {
        // Implement your date sorting logic
        filteredBooks.sort((a, b) => {
            // Example: sort by release date
            return new Date(a.releaseDate) - new Date(b.releaseDate);
        });
    } else if (sortBy === 'alphabet') {
        // Implement your alphabetical sorting logic
        filteredBooks.sort((a, b) => a.title.localeCompare(b.title));
    }else if (sortBy === 'availability') {
        // Implement your alphabetical sorting logic
        filteredBooks.sort((a, b) => a.title.localeCompare(b.status));
    }

    displayBooks(filteredBooks);
}

// Event listeners
sortBySelect.addEventListener('change', filterBooks);
titleInput.addEventListener('input', filterBooks);
authorInput.addEventListener('input', filterBooks);

// Initial display
displayBooks(books);

// ... (your existing code)

function displayBooks(filteredBooks) {
    bookGrid.innerHTML = ''; // Clear previous results

    filteredBooks.forEach(book => {
        const bookCard = document.createElement('div');
        bookCard.classList.add('book-card');
        bookCard.innerHTML = `
            <img src="${book.cover}" alt="${book.title}">
            <h3>${book.title}</h3>
            <p>By: ${book.author}</p>
            <p>Available: ${book.available ? 'Yes' : 'No'}</p>
            <a href="/book/${book.title.replace(/\s+/g, '-').toLowerCase()}">View Details</a> 
        `;
        bookGrid.appendChild(bookCard);
    });
}

// ... (your existing code)