int firstNumber = 10;
int secondNumber = 5;

int result = CalculateTotal(firstNumber, secondNumber);

Console.WriteLine($"The total is: {result}");

static int CalculateTotal(int first, int second)
{
    int total = first + second;

    return total;
}