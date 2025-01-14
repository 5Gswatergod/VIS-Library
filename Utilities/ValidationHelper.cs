using System;
using System.Text.RegularExpressions;

namespace VISLMS.Utilities
{
    public static class ValidationHelper
    {
        // Validate if a string is not null or empty
        public static bool IsNotNullOrEmpty(string input)
        {
            return !string.IsNullOrEmpty(input);
        }

        // Validate if a string matches a given regular expression pattern
        public static bool IsValidPattern (string input, string pattern)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(pattern))
            {
                return false;
            }

            return Regex.IsMatch(input, pattern);
        }

        // Validate if an input string represents a valid integer
        public static bool IsValidInteger(string input)
        {
            return int.TryParse(input, out _);
        }

        // Validate if an input string represents a valid date
        public static bool IsValidDate(string input)
        {
            return DateTime.TryParse(input, out _);
        }

        // Validate if an integer falls within a given range
        public static bool IsWithinRange(int input, int min, int max)
        {
            return input >= min && input <= max;
        }

        // Validate if a string exceeds a specified length
        public static bool IsValidLength(string input, int maxLength)
        {
            return input?.Length <= maxLength;
        }

        // Validate if an email is in a valid format
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            const string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        // Validate if a phone number is in a valid format (e.g., 10-digit number)
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            const string phonePattern = @"^\d{10}$";
            return Regex.IsMatch(phoneNumber, phonePattern);
        }
    }
}