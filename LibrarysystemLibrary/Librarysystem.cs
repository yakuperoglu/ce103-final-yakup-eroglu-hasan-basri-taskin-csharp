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
    }
}