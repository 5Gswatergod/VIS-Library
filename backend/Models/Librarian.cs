﻿using System.ComponentModel.DataAnnotations;

namespace VISLMS.Models
{
    public class Librarian
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }

        public bool VerifyLibrarian()
        {
            return true;
        }

        public string Search(string searchString)
        {
            return $"Searching for: {searchString}";
        }
    }
}
