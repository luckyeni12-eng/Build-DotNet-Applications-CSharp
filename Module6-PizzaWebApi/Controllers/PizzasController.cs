using Microsoft.AspNetCore.Mvc;
using Module6_PizzaWebApi.Models;

namespace Module6_PizzaWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzasController : ControllerBase
{
    private static readonly List<Pizza> Pizzas =
    [
        new Pizza
        {
            Id = 1,
            Name = "Margherita",
            Sauce = "Tomato",
            Toppings =
            [
                "Mozzarella",
                "Basil"
            ]
        },

        new Pizza
        {
            Id = 2,
            Name = "Pepperoni",
            Sauce = "Tomato",
            Toppings =
            [
                "Mozzarella",
                "Pepperoni"
            ]
        },

        new Pizza
        {
            Id = 3,
            Name = "Hawaiian",
            Sauce = "Tomato",
            Toppings =
            [
                "Mozzarella",
                "Ham",
                "Pineapple"
            ]
        },

        // Additional record required by the assignment
        new Pizza
        {
            Id = 4,
            Name = "BBQ Chicken",
            Sauce = "BBQ",
            Toppings =
            [
                "Mozzarella",
                "Chicken",
                "Red Onion"
            ]
        }
    ];

    [HttpGet]
    public ActionResult<IEnumerable<Pizza>> GetAll()
    {
        return Ok(Pizzas);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Pizza> GetById(int id)
    {
        Pizza? pizza = Pizzas.FirstOrDefault(
            p => p.Id == id
        );

        if (pizza == null)
        {
            return NotFound();
        }

        return Ok(pizza);
    }

    [HttpPost]
    public ActionResult<Pizza> Create(Pizza pizza)
    {
        int newId = Pizzas.Count == 0
            ? 1
            : Pizzas.Max(p => p.Id) + 1;

        pizza.Id = newId;

        Pizzas.Add(pizza);

        return CreatedAtAction(
            nameof(GetById),
            new { id = pizza.Id },
            pizza
        );
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(
        int id,
        Pizza updatedPizza)
    {
        Pizza? pizza = Pizzas.FirstOrDefault(
            p => p.Id == id
        );

        if (pizza == null)
        {
            return NotFound();
        }

        pizza.Name = updatedPizza.Name;
        pizza.Sauce = updatedPizza.Sauce;
        pizza.Toppings = updatedPizza.Toppings;

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        Pizza? pizza = Pizzas.FirstOrDefault(
            p => p.Id == id
        );

        if (pizza == null)
        {
            return NotFound();
        }

        Pizzas.Remove(pizza);

        return NoContent();
    }
}