/*

@file Program.cs
@brief The main entry point for the Librarysystem application.
*/

/*
 
@class Program
@brief Contains the main method to run the Librarysystem application.
@details This class initializes the Librarysystem library and launches the main menu.
*/
internal class Program
{
    /**
     
@brief The main entry point for the application.
@param args Command-line arguments passed to the program.*/
    private static void Main(string[] args)
    {// Define file paths for storing user and book information.
        string pathFileUsers = "users.bin";
        string pathFileBooks = "books.bin";

        // Display application startup message.
        Console.WriteLine("Librarysystem Application Running..");
        // Initialize the Librarysystem library.
        var librarysystemLibrary = new LibrarysystemLibrary.Librarysystem();
        // Launch the main menu of the Librarysystem.
        librarysystemLibrary.Main_Menu(pathFileUsers, pathFileBooks);
    }
}