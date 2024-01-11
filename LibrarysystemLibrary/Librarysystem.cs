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
        /**
 * @brief Writes information about unread and unwishlist books to the console.
 * @param pathFileBooks The file path for storing book information.
 * @return True if there are unread and unwishlist books; otherwise, false.
 */
        public bool WriteUnWhislistedBooksToConsole(string pathFileBooks)
        {
            List<Book> books = LoadBooks(pathFileBooks);
            bool isFound = false;
            foreach (Book book in books)
            {
                if (!book.IsWishlist)
                {
                    isFound = true;
                    string readStatus = book.IsMarked ? "Read" : "Unread";
                    string wishlistStatus = book.IsWishlist ? "Wishlist" : "UnWishlisted";

                    Console.WriteLine($"{book.Id}. {book.Name} ({readStatus} : {wishlistStatus})");
                }
            }

            if (!isFound)
            {
                Console.WriteLine("All books are on the wish list.");
                return false;
            }
            return true;
        }
        /**
 * @brief Writes information about wishlist books to the console.
 * @param pathFileBooks The file path for storing book information.
 * @return True if there are wishlist books; otherwise, false.
 */
        public bool WriteWhislistedBooksToConsole(string pathFileBooks)
        {
            List<Book> books = LoadBooks(pathFileBooks);
            bool isFound = false;
            foreach (Book book in books)
            {
                if (book.IsWishlist)
                {
                    isFound = true;
                    string readStatus = book.IsMarked ? "Read" : "Unread";
                    string wishlistStatus = book.IsWishlist ? "Wishlist" : "UnWishlisted";

                    Console.WriteLine($"{book.Id}. {book.Name} ({readStatus} : {wishlistStatus})");
                }
            }

            if (!isFound)
            {
                Console.WriteLine("You bought all the books on your wish list.");
                return false;
            }
            return true;
        }
        /**
 * @brief Writes information about all books to the console.
 * @param pathFileBooks The file path for storing book information.
 * @return True if there are books; otherwise, false.
 */
        public bool WriteBooksToConsole(string pathFileBooks)
        {
            List<Book> books = LoadBooks(pathFileBooks);
            bool isFound = false;
            foreach (Book book in books)
            {
                isFound = true;
                string readStatus = book.IsMarked ? "Read" : "Unread";
                string wishlistStatus = book.IsWishlist ? "Wishlist" : "UnWishlisted";

                Console.WriteLine($"{book.Id}. {book.Name} ({readStatus}: {wishlistStatus})");
            }

            if (!isFound)
            {
                Console.WriteLine("There are no books.");
                return false;
            }
            return true;
        }
        /**
 * @brief Writes information about unborrowed books to the console.
 * @param pathFileBooks The file path for storing book information.
 * @return True if there are unborrowed books; otherwise, false.
 */
        public bool WriteUnBorrowedBooksToConsole(string pathFileBooks)
        {
            List<Book> books = LoadBooks(pathFileBooks);
            bool isFound = false;
            foreach (Book book in books)
            {
                if (!book.IsLoaned)
                {
                    isFound = true;
                    string readStatus = book.IsMarked ? "Read" : "Unread";
                    string wishlistStatus = book.IsWishlist ? "Wishlist" : "UnWishlisted";

                    Console.WriteLine($"{book.Id}. {book.Name} ({readStatus} : {wishlistStatus})");
                }
            }

            if (!isFound)
            {
                Console.WriteLine("There are no books to borrow.");
                return false;
            }
            return true;
        }
        /**
 * @brief Writes information about borrowed books to the console.
 * @param pathFileBooks The file path for storing book information.
 * @return True if there are borrowed books; otherwise, false.
 */
        public bool WriteBorrowedBooksToConsole(string pathFileBooks)
        {
            List<Book> books = LoadBooks(pathFileBooks);
            bool isFound = false;
            foreach (Book book in books)
            {
                if (book.IsLoaned)
                {
                    isFound = true;
                    string readStatus = book.IsMarked ? "Read" : "Unread";
                    string wishlistStatus = book.IsWishlist ? "Wishlist" : "UnWishlisted";

                    Console.WriteLine($"{book.Id}. {book.Name} ({readStatus} : {wishlistStatus})");
                }
            }
            if (!isFound)
            {
                Console.WriteLine("There are no books to give back.");
                return false;
            }
            return true;
        }
        /**
 * @brief Writes information about marked books to the console.
 * @param pathFileBooks The file path for storing book information.
 * @return True if there are marked books; otherwise, false.
 */
        public bool WriteMarkedBooksToConsole(string pathFileBooks)
        {
            List<Book> books = LoadBooks(pathFileBooks);
            bool isFound = false;
            foreach (Book book in books)
            {
                if (book.IsMarked)
                {
                    isFound = true;
                    string readStatus = book.IsMarked ? "Read" : "Unread";
                    string wishlistStatus = book.IsWishlist ? "Wishlist" : "UnWishlisted";

                    Console.WriteLine($"{book.Id}. {book.Name} ({readStatus} : {wishlistStatus})");
                }
            }
            if (!isFound)
            {
                Console.WriteLine("There are no marked books.");
                return false;
            }
            return true;
        }
        /**
 * @brief Writes information about unmarked books to the console.
 * @param pathFileBooks The file path for storing book information.
 * @return True if there are unmarked books; otherwise, false.
 */
        public bool WriteUnMarkedBooksToConsole(string pathFileBooks)
        {
            List<Book> books = LoadBooks(pathFileBooks);
            bool isFound = false;
            foreach (Book book in books)
            {
                if (!book.IsMarked)
                {
                    isFound = true;
                    string readStatus = book.IsMarked ? "Read" : "Unread";
                    string wishlistStatus = book.IsWishlist ? "Wishlist" : "UnWishlisted";

                    Console.WriteLine($"{book.Id}. {book.Name} ({readStatus} : {wishlistStatus})");
                }
            }
            if (!isFound)
            {
                Console.WriteLine("There are no unmarked books.");
                return false;
            }
            return true;
        }
        /**
 * @brief Writes information about unmarked books to the console.
 * @param pathFileBooks The file path for storing book information.
 * @return True if there are unmarked books; otherwise, false.
 */
        public bool WriteUnMarkedBooksToConsole(string pathFileBooks)
        {
            List<Book> books = LoadBooks(pathFileBooks);
            bool isFound = false;
            foreach (Book book in books)
            {
                if (!book.IsMarked)
                {
                    isFound = true;
                    string readStatus = book.IsMarked ? "Read" : "Unread";
                    string wishlistStatus = book.IsWishlist ? "Wishlist" : "UnWishlisted";

                    Console.WriteLine($"{book.Id}. {book.Name} ({readStatus} : {wishlistStatus})");
                }
            }
            if (!isFound)
            {
                Console.WriteLine("There are no unmarked books.");
                return false;
            }
            return true;
        }
        /**
 * @brief Loads books from the specified file path.
 * @param pathFileBooks The file path for storing book information.
 * @return A list of Book objects loaded from the file. If the file doesn't exist, an empty list is returned.
 */
        public List<Book> LoadBooks(string pathFileBooks)
        {
            List<Book> books = new List<Book>();

            // Checks if file path exists
            if (File.Exists(pathFileBooks))
            {
                // Read books from file
                using (BinaryReader reader = new BinaryReader(File.Open(pathFileBooks, FileMode.Open)))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        Book book = new Book
                        {
                            Id = reader.ReadInt32(),
                            Name = reader.ReadString(),
                            IsMarked = reader.ReadBoolean(),
                            IsWishlist = reader.ReadBoolean(),
                            IsLoaned = reader.ReadBoolean()
                        };

                        books.Add(book);
                    }
                }
            }

            return books;
        }
        /**
 * @brief Main menu of the Personal Library System.
 * @param pathFileUsers The file path for storing user information.
 * @param pathFileBooks The file path for storing book information.
 * @return 0 to indicate successful program completion.
 */
        public int Main_Menu(string pathFileUsers, string pathFileBooks)
        {
            int choice;

            while (true)
            {
                ClearScreen();
                Print_Main_Menu();
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    HandleInputError();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ClearScreen();
                        if (LoginUserMenu(pathFileUsers))
                            UserOperations(pathFileBooks);
                        break;

                    case 2:
                        ClearScreen();
                        RegisterMenu(pathFileUsers);
                        break;

                    case 3:
                        ClearScreen();
                        GuestOperation(pathFileBooks);
                        break;

                    case 4:
                        Console.WriteLine("Exit Program");
                        return 0;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        EnterToContinue();
                        break;
                }
            }
        }
        /**
 * @brief Prints the main menu options on the console.
 * @return True to indicate successful execution.
 */
        public bool Print_Main_Menu()
        {
            Console.WriteLine("Welcome To Personal Library System\n\n");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Guest Mode");
            Console.WriteLine("4. Exit Program");
            Console.WriteLine("Please enter a number to select:");
            return true;
        }

        /**
 * @brief Displays the login menu and handles user input.
 * @param pathFile The file path for storing user information.
 * @return True if the login is successful; otherwise, false.
 */
        public bool LoginUserMenu(string pathFile)
        {
            ClearScreen();
            User loginUser = new User();
            Console.Write("Enter email: ");
            loginUser.Email = Console.ReadLine();

            Console.Write("Enter password: ");
            loginUser.Password = Console.ReadLine();
            return LoginUser(loginUser, pathFile);
        }
        /**
 * @brief Attempts to log in a user with the provided credentials.
 * @param user The User object containing the login credentials.
 * @param pathFile The file path for storing user information.
 * @return True if the login is successful; otherwise, false.
 */
        public bool LoginUser(User user, string pathFile)
        {
            if (File.Exists(pathFile))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(pathFile, FileMode.Open)))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        User existingUser = new User
                        {
                            Email = reader.ReadString(),
                            Password = reader.ReadString()
                        };

                        if (existingUser.Email == user.Email && existingUser.Password == user.Password)
                        {
                            Console.WriteLine("Login successful.");
                            EnterToContinue();
                            //UserOperations'u çağır
                            return true;
                        }
                    }
                }
            }
            Console.WriteLine("Invalid email or password. Please try again.");
            EnterToContinue();
            return false;
        }
        /**
 * @brief Displays the registration menu and handles user input for registration.
 * @param pathFileUsers The file path for storing user information.
 */
        public void RegisterMenu(string pathFileUsers)
        {
            ClearScreen();
            User newUser = new User();
            Console.Write("Enter email: ");
            newUser.Email = Console.ReadLine();

            Console.Write("Enter password: ");
            newUser.Password = Console.ReadLine();
            RegisterUser(newUser, pathFileUsers);
        }
        /**
 * @brief Registers a new user by writing their information to the user file.
 * @param user The User object containing the registration information.
 * @param pathFileUsers The file path for storing user information.
 * @return True to indicate successful user registration.
 */
        public bool RegisterUser(User user, string pathFileUsers)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(pathFileUsers, FileMode.Append)))
            {
                writer.Write(user.Email);
                writer.Write(user.Password);
            }

            Console.WriteLine("User registered successfully.");
            EnterToContinue();
            return true;
        }
    }
}