using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection.PortableExecutable;

namespace LibrarysystemLibrary
{
    /**
 * @file LibrarysystemLibrary.cs
 * @brief Contains the definitions of the Book and User classes for the Librarysystem library.
 */

    /**
     * @class Book
     * @brief Represents a book in the library system.
     */
    public class Book
    {
        /**
      * @brief Gets or sets the unique identifier of the book.
      */
        public int Id { get; set; }
        /**
      * @brief Gets or sets the name of the book.
      */
        public string Name { get; set; }
        /**
     * @brief Gets or sets a value indicating whether the book is marked.
     */
        public bool IsMarked { get; set; }
        /**
    * @brief Gets or sets a value indicating whether the book is in the wishlist.
    */
        public bool IsWishlist { get; set; }
        /**
     * @brief Gets or sets a value indicating whether the book is currently loaned.
     */
        public bool IsLoaned { get; set; }
    }
    /**
 * @file Librarysystem.cs
 * @brief Contains the definitions of the User and Librarysystem classes for the Librarysystem library.
 */

    /**
     * @class User
     * @brief Represents a user in the library system.
     */
    public class User
    {
        /**
      * @brief Gets or sets the email address of the user.
      */
        public string Email { get; set; }
        /**
     * @brief Gets or sets the password of the user.
     */
        public string Password { get; set; }
    }

    /**
 * @class Librarysystem
 * @brief Represents the main class for the Librarysystem library.
 */
    public class Librarysystem
    {
        /**
     * @brief Gets or sets a value indicating whether the library system is in test mode.
     * @details When in test mode, certain functions may behave differently.
     */
        public bool IsTestMode { get; set; } = false;
        /**
     * @brief Handles input errors by displaying an error message.
     * @return False to indicate that an error occurred during input handling.
     */
        public bool HandleInputError()
        {
            Console.WriteLine("Only enter numerical value");
            return false;
        }
        /**
     * @brief Clears the console screen, unless the library system is in test mode.
     */
        public void ClearScreen()
        {
            if (IsTestMode)
            {
                return; // Test modundayken konsolu temizleme
            }

            Console.Clear();
        }
        /**
     * @brief Waits for the user to press any key to continue.
     * @return True to indicate successful execution.
     */
        public bool EnterToContinue()
        {
            Console.WriteLine("Press any key to continue...");
            if (!IsTestMode)
            {
                Console.ReadKey();
            }
            return true;
        }
        /**
 * @brief Displays the menu for adding a new book and handles the user input.
 * @param pathFileBooks The file path for storing book information.
 * @return True to indicate successful execution.
 */
        public bool AddBookMenu(string pathFileBooks)
        {
            ClearScreen();
            Console.Write("Enter a book name: ");
            string bookName = Console.ReadLine();
            AddBook(bookName, pathFileBooks);
            return true;
        }
        /**
 * @brief Adds a new book to the library.
 * @param bookName The name of the book to be added.
 * @param pathFileBooks The file path for storing book information.
 * @return True to indicate successful execution.
 */
        public bool AddBook(string bookName, string pathFileBooks)
        {
            Book newBook = new Book
            {
                Id = GetNewId(pathFileBooks),
                Name = bookName,
                IsMarked = false,
                IsWishlist = false,
                IsLoaned = false
            };
            // Write book as binary 
            using (BinaryWriter writer = new BinaryWriter(File.Open(pathFileBooks, FileMode.Append)))
            {
                writer.Write(newBook.Id);
                writer.Write(newBook.Name);
                writer.Write(newBook.IsMarked);
                writer.Write(newBook.IsWishlist);
                writer.Write(newBook.IsLoaned);
            }

            return true;
        }
        /**
 * @brief Displays the menu for deleting a book and handles the user input.
 * @param pathFileBooks The file path for storing book information.
 * @return True to indicate successful execution.
 */
        public bool DeleteBookMenu(string pathFileBooks)
        {
            ClearScreen();
            WriteBooksToConsole(pathFileBooks);
            Console.Write("Enter a number to delete book: ");
            int bookId;

            // Check if the entered input is a valid integer.
            if (!int.TryParse(Console.ReadLine(), out bookId))
            {
                HandleInputError();
                EnterToContinue();
                return false;
            }

            // Delete the book with the specified ID.
            DeleteBook(bookId, pathFileBooks);
            return true;
        }
        /**
 * @brief Deletes a book from the library based on the provided book ID.
 * @param bookId The unique identifier of the book to be deleted.
 * @param pathFileBooks The file path for storing book information.
 * @return True to indicate successful execution.
 */
        public bool DeleteBook(int bookId, string pathFileBooks)
        {
            List<Book> books = LoadBooks(pathFileBooks);
            bool isFound = false;
            using (BinaryWriter writer = new BinaryWriter(File.Open(pathFileBooks, FileMode.Create)))
            {
                foreach (Book book in books)
                {
                    if (book.Id != bookId)
                    {
                        writer.Write(book.Id);
                        writer.Write(book.Name);
                        writer.Write(book.IsMarked);
                        writer.Write(book.IsWishlist);
                        writer.Write(book.IsLoaned);
                    }
                    else
                    {
                        isFound = true;
                    }
                }
            }

            if (isFound)
            {
                Console.WriteLine($"Book with ID '{bookId}' has been deleted successfully.");
                EnterToContinue();
                return true;
            }
            Console.WriteLine($"There is no book you want!");
            EnterToContinue();
            return false;
        }
        /**
* @brief Displays the menu for updating a book and handles the user input.
* @param pathFileBooks The file path for storing book information.
* @return True to indicate successful execution.
*/
        public bool UpdateBookMenu(string pathFileBooks)
        {
            ClearScreen();
            WriteBooksToConsole(pathFileBooks);
            Console.Write("Enter a number to update book: ");
            int bookId;

            if (!int.TryParse(Console.ReadLine(), out bookId))
            {
                HandleInputError();
                EnterToContinue();
                return false;
            }

            Console.Write("Enter the new name for the book: ");
            string newBookName = Console.ReadLine();


            UpdateBook(bookId, newBookName, pathFileBooks);
            return true;
        }
        /**
 * @brief Updates the name of a book in the library based on the provided book ID.
 * @param bookId The unique identifier of the book to be updated.
 * @param newBookName The new name for the book.
 * @param pathFileBooks The file path for storing book information.
 * @return True to indicate successful execution.
 */
        public bool UpdateBook(int bookId, string newBookName, string pathFileBooks)
        {
            List<Book> books = LoadBooks(pathFileBooks);
            bool isFound = false;
            using (BinaryWriter writer = new BinaryWriter(File.Open(pathFileBooks, FileMode.Create)))
            {
                foreach (Book book in books)
                {
                    if (book.Id != bookId)
                    {
                        writer.Write(book.Id);
                        writer.Write(book.Name);
                        writer.Write(book.IsMarked);
                        writer.Write(book.IsWishlist);
                        writer.Write(book.IsLoaned);
                    }
                    else
                    {
                        writer.Write(book.Id);
                        writer.Write(newBookName);
                        writer.Write(book.IsMarked);
                        writer.Write(book.IsWishlist);
                        writer.Write(book.IsLoaned);
                        isFound = true;
                    }
                }
            }

            if (isFound)
            {
                Console.WriteLine($"Book with ID '{newBookName}' has been updated successfully.");
                EnterToContinue();
                return true;
            }
            Console.WriteLine($"There is no book you want!");
            EnterToContinue();
            return false;
        }
        /**
         * @brief Gets a new unique identifier for a book based on the existing book count.
         * @param pathFileBooks The file path for storing book information.
         * @return The new unique identifier for a book.
         */
        public int GetNewId(string pathFileBooks)
        {
            List<Book> books = LoadBooks(pathFileBooks); // Dosya yolu parametresi eklenmiş LoadBooks çağrısı
            return books.Count + 1;
        }
    }
}