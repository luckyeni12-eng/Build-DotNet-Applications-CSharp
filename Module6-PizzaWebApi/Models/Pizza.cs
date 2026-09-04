namespace Module6_PizzaWebApi.Models;

public class Pizza
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Sauce { get; set; } = string.Empty;

    public List<string> Toppings { get; set; } = new();
}