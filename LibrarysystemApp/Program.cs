internal class Program {
  private static void Main(string[] args) {
    Console.WriteLine("Librarysystem Application Running..");
    var librarysystemLibrary = new LibrarysystemLibrary.Librarysystem();
    librarysystemLibrary.Add(2, 2);
    librarysystemLibrary.Multiply(2, 2);
    librarysystemLibrary.Subtract(2, 2);
    librarysystemLibrary.Divide(2, 2);
  }
}
