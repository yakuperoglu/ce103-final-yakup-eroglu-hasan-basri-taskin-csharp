using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection.PortableExecutable;

namespace LibrarysystemLibrary
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMarked { get; set; }
        public bool IsWishlist { get; set; }
        public bool IsLoaned { get; set; }
    }

    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Librarysystem
    {
        public bool IsTestMode { get; set; } = false;

        public bool HandleInputError()
        {
            Console.WriteLine("Only enter numerical value");
            return false;
        }

        public void ClearScreen()
        {
            if (IsTestMode)
            {
                return; // Test modundayken konsolu temizleme
            }

            Console.Clear();
        }

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