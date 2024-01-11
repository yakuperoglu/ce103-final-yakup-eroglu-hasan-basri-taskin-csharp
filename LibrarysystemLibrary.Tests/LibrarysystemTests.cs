using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace LibrarysystemLibrary.Tests
{
    public class LibrarysystemTests
    {
        private string testFilePathBooks = "test_books.bin";
        private string testFilePathUsers = "test_users.bin";

        [Fact]
        public void TestAddBookMenu_SuccessfulAddition()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var input = new StringReader("NewBook\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            bool result = library.AddBookMenu(testFilePathBooks);

            string expectedOutput = "Enter a book name: ";
            Assert.Contains(expectedOutput, output.ToString());
            Assert.True(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void AddBook_ShouldAddBookToList()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            Librarysystem library = new Librarysystem();
            library.IsTestMode = true;

            bool result = library.AddBook("Test Book", testFilePathBooks);

            Assert.True(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void GetNewId_ShouldReturnCorrectId()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            Librarysystem library = new Librarysystem();
            library.IsTestMode = true;
            CreateTestFile(); // Test dosyasýný oluþtur

            int newId = library.GetNewId(testFilePathBooks);

            Assert.Equal(5, newId);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void LoadBooks_ShouldLoadBooksFromFile()
        {

            CleanupTestDataBook();
            CleanupTestDataUser();

            Librarysystem library = new Librarysystem();
            library.IsTestMode = true;
            CreateTestFile();

            List<Book> books = library.LoadBooks(testFilePathBooks);

            Assert.NotNull(books);
            Assert.Equal(4, books.Count());

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void HandleInputError_ShouldReturnFalse()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();


            Librarysystem library = new Librarysystem();
            library.IsTestMode = true;

            bool result = library.HandleInputError();

            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestMain_Menu_ShouldVisitAllFunctionsAndExit()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var input = new StringReader("2\nexample\nexample\n3\n2\n1\nexample\nexample\n5\n4");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var library = new Librarysystem();
            library.IsTestMode = true;

            int result = library.Main_Menu(testFilePathUsers, testFilePathBooks);
            string expectedOutput =
                "Welcome To Personal Library System\n\n\r\n1. Login\r\n2. Register\r\n3. Guest Mode\r\n4. Exit Program\r\nPlease enter a number to select:\r\nEnter email: Enter password: User registered successfully.\r\nPress any key to continue...\r\nWelcome To Personal Library System\n\n\r\n1. Login\r\n2. Register\r\n3. Guest Mode\r\n4. Exit Program\r\nPlease enter a number to select:\r\nGuest Operations\n\n\r\n1. View Catalog\r\n2. Return to Main Menu\r\nPlease enter a number to select:\r\nWelcome To Personal Library System\n\n\r\n1. Login\r\n2. Register\r\n3. Guest Mode\r\n4. Exit Program\r\nPlease enter a number to select:\r\nEnter email: Enter password: Login successful.\r\nPress any key to continue...\r\nWelcome to User Operations\n\n\r\n1. Book Cataloging\r\n2. Loan Management\r\n3. WishList Management\r\n4. Reading Tracker\r\n5. Return to Main Menu\r\nPlease enter a number to select:\r\nWelcome To Personal Library System\n\n\r\n1. Login\r\n2. Register\r\n3. Guest Mode\r\n4. Exit Program\r\nPlease enter a number to select:\r\nExit Program\r\n";

            Assert.Equal(expectedOutput, output.ToString());
            Assert.Equal(0, result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }


        [Fact]
        public void TestMain_Menu_DefaultChoice_ShouldPrintInvalidChoiceAndExit()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var input = new StringReader("abc\n1231231234\n4");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var library = new Librarysystem();
            library.IsTestMode = true;

            int result = library.Main_Menu(testFilePathUsers, testFilePathBooks);



            // Assert
            string expectedOutput =
                "Welcome To Personal Library System\n\n\r\n1. Login\r\n2. Register\r\n3. Guest Mode\r\n4. Exit Program\r\nPlease enter a number to select:\r\nOnly enter numerical value\r\nWelcome To Personal Library System\n\n\r\n1. Login\r\n2. Register\r\n3. Guest Mode\r\n4. Exit Program\r\nPlease enter a number to select:\r\nInvalid choice. Please try again.\r\nPress any key to continue...\r\nWelcome To Personal Library System\n\n\r\n1. Login\r\n2. Register\r\n3. Guest Mode\r\n4. Exit Program\r\nPlease enter a number to select:\r\nExit Program\r\n";

            Assert.Equal(expectedOutput, output.ToString());
            Assert.Equal(0, result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestRegisterUser_ShouldRegisterUser()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            // Arrange
            var library = new Librarysystem();
            library.IsTestMode = true;

            // Act
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            using (var inputStream = new StringReader("test@example.com\ntestpassword"))
            {
                Console.SetIn(inputStream);

                library.RegisterMenu(testFilePathUsers);
            }

            // Assert
            string expectedOutput =
                "Enter email: Enter password: User registered successfully.\r\nPress any key to continue...\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestLoginUser_ShouldLoginUserSuccessfully()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var testUser = new User { Email = "test@example.com", Password = "testpassword" };
            library.RegisterUser(testUser, testFilePathUsers);

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            using (var inputStream = new StringReader("test@example.com\ntestpassword"))
            {
                Console.SetIn(inputStream);

                library.LoginUserMenu(testFilePathUsers);
            }

            // Assert
            string expectedOutput =
                "Enter email: Enter password: Login successful.\r\nPress any key to continue...\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestLoginUser_InvalidInput_ShouldPrintErrorMessage()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var testUser = new User { Email = "test@example.com", Password = "testpassword" };
            library.RegisterUser(testUser, testFilePathUsers);

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            using (var inputStream = new StringReader("invalidemail\ninvalidpassword"))
            {
                Console.SetIn(inputStream);

                library.LoginUserMenu(testFilePathUsers);
            }

            string expectedOutput =
                "Enter email: Enter password: Invalid email or password. Please try again.\r\nPress any key to continue...\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestViewCatalog_ShouldDisplayBooks()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Act
            library.ViewCatalog(testFilePathBooks);

            // Assert
            string expectedOutput =
                "1. Book1 (Unread: UnWishlisted)\r\n2. Book2 (Read: Wishlist)\r\n3. Book3 (Read: Wishlist)\r\n4. Book4 (Unread: UnWishlisted)\r\nPress any key to continue...\r\n";
            Assert.Equal(expectedOutput, consoleOutput.ToString());

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }


        [Fact]
        public void TestViewCatalog_ShouldntDisplayBooks()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            library.ViewCatalog(testFilePathBooks);

            // Assert
            string expectedOutput =
                "There are no books.\r\nPress any key to continue...\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestViewCatalog_ShouldDisplayNoBooks()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            // Delete the existing file if it exists
            if (File.Exists(testFilePathBooks))
            {
                File.Delete(testFilePathBooks);
            }

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            library.ViewCatalog(testFilePathBooks);

            string expectedOutput = "There are no books.\r\nPress any key to continue...\r\n";
            Assert.Equal(expectedOutput, consoleOutput.ToString());

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestGuestOperation_ShouldDisplayMenuAndExit()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var input = new StringReader("abc\n321312\n1\n4\n2");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            bool result = library.GuestOperation(testFilePathUsers);

            string expectedOutput =
                "Guest Operations\n\n\r\n1. View Catalog\r\n2. Return to Main Menu\r\nPlease enter a number to select:\r\nOnly enter numerical value\r\nGuest Operations\n\n\r\n1. View Catalog\r\n2. Return to Main Menu\r\nPlease enter a number to select:\r\nInvalid choice. Please try again.\r\nPress any key to continue...\r\nGuest Operations\n\n\r\n1. View Catalog\r\n2. Return to Main Menu\r\nPlease enter a number to select:\r\nThere are no books.\r\nPress any key to continue...\r\nGuest Operations\n\n\r\n1. View Catalog\r\n2. Return to Main Menu\r\nPlease enter a number to select:\r\nInvalid choice. Please try again.\r\nPress any key to continue...\r\nGuest Operations\n\n\r\n1. View Catalog\r\n2. Return to Main Menu\r\nPlease enter a number to select:\r\n";

            Assert.Equal(expectedOutput, output.ToString());
            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestDeleteBookMenu_SuccessfulDeletion()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            var input = new StringReader("2");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            bool result = library.DeleteBookMenu(testFilePathBooks);

            string expectedOutput =
                "1. Book1 (Unread: UnWishlisted)\r\n2. Book2 (Read: Wishlist)\r\n3. Book3 (Read: Wishlist)\r\n4. Book4 (Unread: UnWishlisted)\r\nEnter a number to delete book: Book with ID '2' has been deleted successfully.\r\nPress any key to continue...\r\n";

            Assert.Equal(expectedOutput, output.ToString());
            Assert.True(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestDeleteBookMenu_ShouldInvalidInput()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            var input = new StringReader("qwe");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            bool result = library.DeleteBookMenu(testFilePathBooks);

            string expectedOutput =
                "1. Book1 (Unread: UnWishlisted)\r\n2. Book2 (Read: Wishlist)\r\n3. Book3 (Read: Wishlist)\r\n4. Book4 (Unread: UnWishlisted)\r\nEnter a number to delete book: Only enter numerical value\r\nPress any key to continue...\r\n";

            Assert.Equal(expectedOutput, output.ToString());
            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestDeleteBook_BookNotFound()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            // Act
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            var result = library.DeleteBook(5, testFilePathBooks);

            // Assert
            Assert.False(result);

            string expectedOutput =
                "There is no book you want!\r\nPress any key to continue...\r\n";
            Assert.Equal(expectedOutput, consoleOutput.ToString());

            //Cleanup
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestUserOperations_ReturnsFalseOnExit()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var input = new StringReader("qwe\n1\n5\n2\n4\n3\n4\n4\n4\n321\n5");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            bool result = library.UserOperations(testFilePathBooks);

            // Assert
            string expectedOutput =
                "Welcome to User Operations\n\n\r\n1. Book Cataloging\r\n2. Loan Management\r\n3. WishList Management\r\n4. Reading Tracker\r\n5. Return to Main Menu\r\nPlease enter a number to select:\r\nOnly enter numerical value\r\nPress any key to continue...\r\nWelcome to User Operations\n\n\r\n1. Book Cataloging\r\n2. Loan Management\r\n3. WishList Management\r\n4. Reading Tracker\r\n5. Return to Main Menu\r\nPlease enter a number to select:\r\nWelcome to Book Operations\n\n\r\n1. Add Book\r\n2. Delete Book\r\n3. Update Book\r\n4. View Catalog\r\n5. Return User Operations\r\nPlease enter a number to select:\r\nWelcome to User Operations\n\n\r\n1. Book Cataloging\r\n2. Loan Management\r\n3. WishList Management\r\n4. Reading Tracker\r\n5. Return to Main Menu\r\nPlease enter a number to select:\r\nLoan Management Menu\n\n\r\n1. Give Book\r\n2. Borrow Book\r\n3. View Borrowed Books\r\n4. Return to Main Menu\r\nPlease enter a number to select:\r\nWelcome to User Operations\n\n\r\n1. Book Cataloging\r\n2. Loan Management\r\n3. WishList Management\r\n4. Reading Tracker\r\n5. Return to Main Menu\r\nPlease enter a number to select:\r\nWelcome to WishlistManageMenu\n\n\r\n1. View Wishlist\r\n2. Add To Wishlist\r\n3. Remove From Wishlist\r\n4. Return User Operations\r\nPlease enter a number to select:\r\nWelcome to User Operations\n\n\r\n1. Book Cataloging\r\n2. Loan Management\r\n3. WishList Management\r\n4. Reading Tracker\r\n5. Return to Main Menu\r\nPlease enter a number to select:\r\nWelcome to ReadingTracker\n\n\r\n1. Log Progress\r\n2. Mark As Read\r\n3. View History\r\n4. Return User Operations\r\nPlease enter a number to select:\r\nWelcome to User Operations\n\n\r\n1. Book Cataloging\r\n2. Loan Management\r\n3. WishList Management\r\n4. Reading Tracker\r\n5. Return to Main Menu\r\nPlease enter a number to select:\r\nInvalid choice. Please try again.\r\nPress any key to continue...\r\nWelcome to User Operations\n\n\r\n1. Book Cataloging\r\n2. Loan Management\r\n3. WishList Management\r\n4. Reading Tracker\r\n5. Return to Main Menu\r\nPlease enter a number to select:\r\n";
            Assert.Equal(expectedOutput, output.ToString());
            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestBookCataloging_ReturnsFalseOnReturnToUserOperations()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var input = new StringReader("qwe\n1\nqwe\n2\n1\n1\nqwe\n3\n1\nqwe1\n4\n321\n5");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            bool result = library.BookCataloging(testFilePathBooks);

            // Assert
            string expectedOutput =
                "Welcome to Book Operations\n\n\r\n1. Add Book\r\n2. Delete Book\r\n3. Update Book\r\n4. View Catalog\r\n5. Return User Operations\r\nPlease enter a number to select:\r\nOnly enter numerical value\r\nWelcome to Book Operations\n\n\r\n1. Add Book\r\n2. Delete Book\r\n3. Update Book\r\n4. View Catalog\r\n5. Return User Operations\r\nPlease enter a number to select:\r\nEnter a book name: Welcome to Book Operations\n\n\r\n1. Add Book\r\n2. Delete Book\r\n3. Update Book\r\n4. View Catalog\r\n5. Return User Operations\r\nPlease enter a number to select:\r\n1. qwe (Unread: UnWishlisted)\r\nEnter a number to delete book: Book with ID '1' has been deleted successfully.\r\nPress any key to continue...\r\nWelcome to Book Operations\n\n\r\n1. Add Book\r\n2. Delete Book\r\n3. Update Book\r\n4. View Catalog\r\n5. Return User Operations\r\nPlease enter a number to select:\r\nEnter a book name: Welcome to Book Operations\n\n\r\n1. Add Book\r\n2. Delete Book\r\n3. Update Book\r\n4. View Catalog\r\n5. Return User Operations\r\nPlease enter a number to select:\r\n1. qwe (Unread: UnWishlisted)\r\nEnter a number to update book: Enter the new name for the book: Book with ID 'qwe1' has been updated successfully.\r\nPress any key to continue...\r\nWelcome to Book Operations\n\n\r\n1. Add Book\r\n2. Delete Book\r\n3. Update Book\r\n4. View Catalog\r\n5. Return User Operations\r\nPlease enter a number to select:\r\n1. qwe1 (Unread: UnWishlisted)\r\nPress any key to continue...\r\nWelcome to Book Operations\n\n\r\n1. Add Book\r\n2. Delete Book\r\n3. Update Book\r\n4. View Catalog\r\n5. Return User Operations\r\nPlease enter a number to select:\r\nInvalid choice. Please try again.\r\nPress any key to continue...\r\nWelcome to Book Operations\n\n\r\n1. Add Book\r\n2. Delete Book\r\n3. Update Book\r\n4. View Catalog\r\n5. Return User Operations\r\nPlease enter a number to select:\r\n";
            Assert.Equal(expectedOutput, output.ToString());
            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestLoanManagement_ReturnsFalseOnReturnToUserOperations()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            // Arrange
            var library = new Librarysystem();
            library.IsTestMode = true;
            CreateTestFile();

            var input = new StringReader("qwe\n2\n1\n1\n1\n3\n321\n4");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            bool result = library.LoanManagement(testFilePathBooks);

            // Assert
            string expectedOutput =
                "Loan Management Menu\n\n\r\n1. Give Book\r\n2. Borrow Book\r\n3. View Borrowed Books\r\n4. Return to Main Menu\r\nPlease enter a number to select:\r\nOnly enter numerical value\r\nLoan Management Menu\n\n\r\n1. Give Book\r\n2. Borrow Book\r\n3. View Borrowed Books\r\n4. Return to Main Menu\r\nPlease enter a number to select:\r\n1. Book1 (Unread : UnWishlisted)\r\n3. Book3 (Read : Wishlist)\r\nEnter the ID of the book you want to borrow: Book with ID '1' has been borrowed successfully.\r\nPress any key to continue...\r\nLoan Management Menu\n\n\r\n1. Give Book\r\n2. Borrow Book\r\n3. View Borrowed Books\r\n4. Return to Main Menu\r\nPlease enter a number to select:\r\n1. Book1 (Unread : UnWishlisted)\r\n2. Book2 (Read : Wishlist)\r\n4. Book4 (Unread : UnWishlisted)\r\nEnter the ID of the book you want to give back: Book with ID '1' returned successfully.\r\nPress any key to continue...\r\nLoan Management Menu\n\n\r\n1. Give Book\r\n2. Borrow Book\r\n3. View Borrowed Books\r\n4. Return to Main Menu\r\nPlease enter a number to select:\r\n2. Book2 (Read : Wishlist)\r\n4. Book4 (Unread : UnWishlisted)\r\nPress any key to continue...\r\nLoan Management Menu\n\n\r\n1. Give Book\r\n2. Borrow Book\r\n3. View Borrowed Books\r\n4. Return to Main Menu\r\nPlease enter a number to select:\r\nInvalid choice. Please try again.\r\nPress any key to continue...\r\nLoan Management Menu\n\n\r\n1. Give Book\r\n2. Borrow Book\r\n3. View Borrowed Books\r\n4. Return to Main Menu\r\nPlease enter a number to select:\r\n";

            Assert.Equal(expectedOutput, output.ToString());
            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestUpdateBookMenu_ShouldUpdateBookSuccessfully()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            var input = new StringReader("2\nUpdatedBook\n");
            Console.SetIn(input);

            // Act
            bool result = library.UpdateBookMenu(testFilePathBooks);

            // Assert
            string expectedOutput =
                "1. Book1 (Unread: UnWishlisted)\r\n2. Book2 (Read: Wishlist)\r\n3. Book3 (Read: Wishlist)\r\n4. Book4 (Unread: UnWishlisted)\r\nEnter a number to update book: Enter the new name for the book: Book with ID 'UpdatedBook' has been updated successfully.\r\nPress any key to continue...\r\n";
            Assert.Equal(expectedOutput, consoleOutput.ToString());
            Assert.True(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestUpdateBookMenu_InvalidInput_ShouldPrintErrorMessage()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();
            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            var input = new StringReader("qwe\n");
            Console.SetIn(input);

            bool result = library.UpdateBookMenu(testFilePathBooks);

            string expectedOutput =
                "1. Book1 (Unread: UnWishlisted)\r\n2. Book2 (Read: Wishlist)\r\n3. Book3 (Read: Wishlist)\r\n4. Book4 (Unread: UnWishlisted)\r\nEnter a number to update book: Only enter numerical value\r\nPress any key to continue...\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());
            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestUpdateBookMenu_InvalidBookId_ShouldPrintErrorMessage()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            var input = new StringReader("123132");
            Console.SetIn(input);

            library.UpdateBookMenu(testFilePathBooks);

            string expectedOutput =
                "1. Book1 (Unread: UnWishlisted)\r\n2. Book2 (Read: Wishlist)\r\n3. Book3 (Read: Wishlist)\r\n4. Book4 (Unread: UnWishlisted)\r\nEnter a number to update book: Enter the new name for the book: There is no book you want!\r\nPress any key to continue...\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestWriteUnBorrowedBooksToConsole_ShouldWriteBooksAndReturnTrue()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();
            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            bool result = library.WriteUnBorrowedBooksToConsole(testFilePathBooks);

            string expectedOutput =
                "1. Book1 (Unread : UnWishlisted)\r\n3. Book3 (Read : Wishlist)\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());
            Assert.True(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestWriteUnBorrowedBooksToConsole_ShouldntFindBooks()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            bool result = library.WriteUnBorrowedBooksToConsole(testFilePathBooks);

            string expectedOutput =
                "There are no books to borrow.\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());
            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestWriteBorrowedBooksToConsole_ShouldWriteBooksAndReturnTrue()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            bool result = library.WriteBorrowedBooksToConsole(testFilePathBooks);

            string expectedOutput =
                "2. Book2 (Read : Wishlist)\r\n4. Book4 (Unread : UnWishlisted)\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());
            Assert.True(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestWriteBorrowedBooksToConsole_ShouldntFindBooks()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            bool result = library.WriteBorrowedBooksToConsole(testFilePathBooks);

            string expectedOutput =
                "There are no books to give back.\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());
            Assert.False(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestViewBorrowedBooks_ShouldDisplayBooksAndReturnTrue()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            bool result = library.ViewBorrowedBooks(testFilePathBooks);

            string expectedOutput =
                "2. Book2 (Read : Wishlist)\r\n4. Book4 (Unread : UnWishlisted)\r\nPress any key to continue...\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());
            Assert.True(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestBorrowBookMenu_ShouldBorrowBookAndReturnTrue()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            var input = new StringReader("1");
            Console.SetIn(input);

            bool result = library.BorrowBookMenu(testFilePathBooks);

            Assert.True(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestBorrowBookMenu_ShouldNotBorrowBookAndReturnFalse()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            var input = new StringReader("2");
            Console.SetIn(input);

            bool result = library.BorrowBookMenu(testFilePathBooks);

            Assert.False(result);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestBorrowBookMenu_InvalidInputShouldReturnFalse()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var input = new StringReader("qwe");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var library = new Librarysystem();
            library.IsTestMode = true;

            bool result = library.BorrowBookMenu(testFilePathBooks);
            string expectedOutput =
                "There are no books to borrow.\r\nEnter the ID of the book you want to borrow: Only enter numerical value\r\nPress any key to continue...\r\n";

            Assert.Equal(expectedOutput, output.ToString());
            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestGiveBookMenu_InvalidInput()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var input = new StringReader("qwe");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var library = new Librarysystem();
            library.IsTestMode = true;

            bool result = library.GiveBookMenu(testFilePathBooks);
            string expectedOutput =
                "There are no books to give back.\r\nEnter the ID of the book you want to give back: Only enter numerical value\r\nPress any key to continue...\r\n";

            Assert.Equal(expectedOutput, output.ToString());
            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestGiveBookMenu_ShouldGiveBookBackAndReturnTrue()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            var input = new StringReader("2");
            Console.SetIn(input);

            bool result = library.GiveBookMenu(testFilePathBooks);

            Assert.True(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestGiveBookMenu_ShouldNotGiveBookBackAndReturnFalse()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            var input = new StringReader("1");
            Console.SetIn(input);

            bool result = library.GiveBookMenu(testFilePathBooks);

            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestGiveBook_ShouldGiveBookBackAndReturnTrue()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            bool result = library.GiveBook(2, testFilePathBooks);

            Assert.True(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestGiveBook_ShouldNotGiveBookBackAndReturnFalse()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            bool result = library.GiveBook(1, testFilePathBooks);

            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestWishList_InvalidOption()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var input = new StringReader("312\nqwe\n4");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            bool result = library.WishList(testFilePathBooks);

            string expectedOutput =
                "Welcome to WishlistManageMenu\n\n\r\n1. View Wishlist\r\n2. Add To Wishlist\r\n3. Remove From Wishlist\r\n4. Return User Operations\r\nPlease enter a number to select:\r\nInvalid choice. Please try again.\r\nPress any key to continue...\r\nWelcome to WishlistManageMenu\n\n\r\n1. View Wishlist\r\n2. Add To Wishlist\r\n3. Remove From Wishlist\r\n4. Return User Operations\r\nPlease enter a number to select:\r\nOnly enter numerical value\r\nWelcome to WishlistManageMenu\n\n\r\n1. View Wishlist\r\n2. Add To Wishlist\r\n3. Remove From Wishlist\r\n4. Return User Operations\r\nPlease enter a number to select:\r\n";

            Assert.Equal(expectedOutput, output.ToString());
            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestListWishList_ShouldWriteBooksAndReturnTrue()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            bool result = library.ListWishList(testFilePathBooks);

            string expectedOutput =
                "2. Book2 (Read : Wishlist)\r\n3. Book3 (Read : Wishlist)\r\nPress any key to continue...\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());
            Assert.True(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestRemoveFromWishListMenu_InvalidBookId()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var input = new StringReader("8448\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);


            CreateTestFile();

            bool result = library.RemoveFromWishListMenu(testFilePathBooks);

            string expectedOutput =
                "2. Book2 (Read : Wishlist)\r\n3. Book3 (Read : Wishlist)\r\nEnter the ID of the book you want to remove from your wishlist: There is no wishlisted book with ID '8448'.\r\nPress any key to continue...\r\n";

            Assert.Equal(expectedOutput, output.ToString());
            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestAddToWishListMenu_InputError()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            var input = new StringReader("qwe");
            Console.SetIn(input);

            bool result = library.AddToWishListMenu(testFilePathBooks);

            string expectedOutput =
                "All books are on the wish list.\r\nEnter the ID of the book you want to add to your wishlist: Only enter numerical value\r\nPress any key to continue...\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());
            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestAddToWishListMenu_ShouldAddBookAndReturnTrue()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var input = new StringReader("4\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            CreateTestFile();

            bool result = library.AddToWishListMenu(testFilePathBooks);

            string expectedOutput =
                "1. Book1 (Unread : UnWishlisted)\r\n4. Book4 (Unread : UnWishlisted)\r\nEnter the ID of the book you want to add to your wishlist: Book with ID '4' has been added to your wishlist.\r\nPress any key to continue...\r\n";

            Assert.Equal(expectedOutput, output.ToString());
            Assert.True(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestAddToWishListMenu_InvalidBookId()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var input = new StringReader("89\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            CreateTestFile();

            bool result = library.AddToWishListMenu(testFilePathBooks);

            string expectedOutput =
                "1. Book1 (Unread : UnWishlisted)\r\n4. Book4 (Unread : UnWishlisted)\r\nEnter the ID of the book you want to add to your wishlist: There is no book with ID '89'.\r\nPress any key to continue...\r\n";

            Assert.Equal(expectedOutput, output.ToString());
            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestWishList_ReturnsFalseOnReturnToUserOperations()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var input = new StringReader("qwe\n1\n2\n1\n3\n1\n4");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            bool result = library.WishList(testFilePathBooks);

            string expectedOutput =
                "Welcome to WishlistManageMenu\n\n\r\n1. View Wishlist\r\n2. Add To Wishlist\r\n3. Remove From Wishlist\r\n4. Return User Operations\r\nPlease enter a number to select:\r\nOnly enter numerical value\r\nWelcome to WishlistManageMenu\n\n\r\n1. View Wishlist\r\n2. Add To Wishlist\r\n3. Remove From Wishlist\r\n4. Return User Operations\r\nPlease enter a number to select:\r\nYou bought all the books on your wish list.\r\nPress any key to continue...\r\nWelcome to WishlistManageMenu\n\n\r\n1. View Wishlist\r\n2. Add To Wishlist\r\n3. Remove From Wishlist\r\n4. Return User Operations\r\nPlease enter a number to select:\r\nAll books are on the wish list.\r\nEnter the ID of the book you want to add to your wishlist: There is no book with ID '1'.\r\nPress any key to continue...\r\nWelcome to WishlistManageMenu\n\n\r\n1. View Wishlist\r\n2. Add To Wishlist\r\n3. Remove From Wishlist\r\n4. Return User Operations\r\nPlease enter a number to select:\r\nYou bought all the books on your wish list.\r\nEnter the ID of the book you want to remove from your wishlist: There is no wishlisted book with ID '1'.\r\nPress any key to continue...\r\nWelcome to WishlistManageMenu\n\n\r\n1. View Wishlist\r\n2. Add To Wishlist\r\n3. Remove From Wishlist\r\n4. Return User Operations\r\nPlease enter a number to select:\r\n";

            Assert.Equal(expectedOutput, output.ToString());
            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestWriteBooksToConsole_NoBooks()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            bool result = library.WriteBooksToConsole(testFilePathBooks);

            string expectedOutput = "There are no books.\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());
            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }
        [Fact]
        public void TestWriteBooksToConsole_ShouldWriteBooks()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            bool result = library.WriteBooksToConsole(testFilePathBooks);

            string expectedOutput =
                "1. Book1 (Unread: UnWishlisted)\r\n2. Book2 (Read: Wishlist)\r\n3. Book3 (Read: Wishlist)\r\n4. Book4 (Unread: UnWishlisted)\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());
            Assert.True(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestWriteMarkedBooksToConsole_NoMarkedBooks()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            bool result = library.WriteMarkedBooksToConsole(testFilePathBooks);

            string expectedOutput = "There are no marked books.\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());
            Assert.False(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }

        [Fact]
        public void TestWriteMarkedBooksToConsole_ShouldWriteMarkedBooks()
        {
            CleanupTestDataBook();
            CleanupTestDataUser();

            var library = new Librarysystem();
            library.IsTestMode = true;

            CreateTestFile();

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            bool result = library.WriteMarkedBooksToConsole(testFilePathBooks);

            string expectedOutput =
                "2. Book2 (Read : Wishlist)\r\n3. Book3 (Read : Wishlist)\r\n";

            Assert.Equal(expectedOutput, consoleOutput.ToString());
            Assert.True(result);

            // Clean up
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
        }


        private void CreateTestFile()
        {
            //Books
            var testBooks = new List<Book>
            {
                new Book { Id = 1, Name = "Book1", IsMarked = false, IsWishlist = false, IsLoaned = false },
                new Book { Id = 2, Name = "Book2", IsMarked = true, IsWishlist = true, IsLoaned = true },
                new Book { Id = 3, Name = "Book3", IsMarked = true, IsWishlist = true, IsLoaned = false },
                new Book { Id = 4, Name = "Book4", IsMarked = false, IsWishlist = false, IsLoaned = true },
            };

            // Create a test file with books
            using (BinaryWriter writer = new BinaryWriter(File.Open(testFilePathBooks, FileMode.Create)))
            {
                foreach (var book in testBooks)
                {
                    writer.Write(book.Id);
                    writer.Write(book.Name);
                    writer.Write(book.IsMarked);
                    writer.Write(book.IsWishlist);
                    writer.Write(book.IsLoaned);
                }
            }
        }

        private void CleanupTestDataBook()
        {
            if (File.Exists(testFilePathBooks))
            {
                File.Delete(testFilePathBooks);
            }
        }
        private void CleanupTestDataUser()
        {
            if (File.Exists(testFilePathUsers))
            {
                File.Delete(testFilePathUsers);
            }
        }
    }
}
