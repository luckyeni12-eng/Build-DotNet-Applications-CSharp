using Humanizer;

string message = "build dotnet applications with csharp";

Console.WriteLine("Original text:");
Console.WriteLine(message);

Console.WriteLine();

Console.WriteLine("Using the Humanizer NuGet package:");
Console.WriteLine(message.Titleize());