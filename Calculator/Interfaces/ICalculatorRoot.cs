using Calculator.Models;
using System.Numerics;

namespace Calculator.Interfaces;

// მშობელი კლასის ინტერფეისი
public interface ICalculatorRoot
{
    // დამატება
    Complex Add(Complex a, Complex b);

    // გამოკლება
    Complex Subtract(Complex a, Complex b);

    // გამრავლება
    Complex Multiply(Complex a, Complex b);

    // გაყოფა
    Complex Divide(Complex a, Complex b);

    // ოპერატორის არჩევა
    Func<Complex, Complex, Complex> GetOperationMethod(string operation);

    // გადაკასტვა
    Complex? ComplexParse(string text);

    // ისტორიის დამატება
    void AddHistory(OperationModel model);
}
