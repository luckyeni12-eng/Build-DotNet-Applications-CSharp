Console.WriteLine("Hello, World!");

Console.WriteLine("My name is a C# student.");
Console.WriteLine("I am learning how to build .NET applications.");

string firstName = "Lucky";
int age = 25;
bool isLearningCSharp = true;

Console.WriteLine($"Name: {firstName}");
Console.WriteLine($"Age: {age}");
Console.WriteLine($"Learning C#: {isLearningCSharp}");

int number1 = 10;
int number2 = 5;

int sum = number1 + number2;
int difference = number1 - number2;
int product = number1 * number2;
int quotient = number1 / number2;

Console.WriteLine($"Sum: {sum}");
Console.WriteLine($"Difference: {difference}");
Console.WriteLine($"Product: {product}");
Console.WriteLine($"Quotient: {quotient}");

if (age >= 18)
{
    Console.WriteLine("The person is an adult.");
}
else
{
    Console.WriteLine("The person is a minor.");
}

string[] programmingLanguages =
{
    "C#",
    "JavaScript",
    "Python"
};

foreach (string language in programmingLanguages)
{
    Console.WriteLine($"Language: {language}");
}

int result = AddNumbers(20, 30);

Console.WriteLine($"Method result: {result}");

static int AddNumbers(int firstNumber, int secondNumber)
{
    return firstNumber + secondNumber;
}
