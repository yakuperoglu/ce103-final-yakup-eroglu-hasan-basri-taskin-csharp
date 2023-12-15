
namespace LibrarysystemLibrary.Tests {
public class LibrarysystemTests {
  [Fact]
  public void TestAdd() {
    Librarysystem calc = new Librarysystem();
    Assert.Equal(4, calc.Add(2, 2));
  }

  [Fact]
  public void TestSubtract() {
    Librarysystem calc = new Librarysystem();
    Assert.Equal(2, calc.Subtract(4, 2));
  }

  [Fact]
  public void TestMultiply() {
    Librarysystem calc = new Librarysystem();
    Assert.Equal(8, calc.Multiply(2, 4));
  }

  [Fact]
  public void TestDivide() {
    Librarysystem calc = new Librarysystem();
    Assert.Equal(2, calc.Divide(4, 2));
  }

  [Fact]
  public void TestDivideByZero() {
    Librarysystem calc = new Librarysystem();
    Assert.Throws<DivideByZeroException>(() => calc.Divide(4, 0));
  }
}
}
