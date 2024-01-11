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
