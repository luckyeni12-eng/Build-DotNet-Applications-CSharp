using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<DbContextOptions<MvcMovieContext>>());

        if (context.Movie.Any())
        {
            return;
        }

        context.Movie.AddRange(
            new Movie
            {
                Title = "Black Panther",
                ReleaseDate = DateTime.Parse("2018-02-16"),
                Genre = "Action",
                Price = 12.99M,
                Rating = "PG-13"
            },

            new Movie
            {
                Title = "The Lion King",
                ReleaseDate = DateTime.Parse("1994-06-24"),
                Genre = "Animation",
                Price = 10.99M,
                Rating = "G"
            },

            new Movie
            {
                Title = "Inception",
                ReleaseDate = DateTime.Parse("2010-07-16"),
                Genre = "Science Fiction",
                Price = 11.99M,
                Rating = "PG-13"
            },

            new Movie
            {
                Title = "When Harry Met Sally",
                ReleaseDate = DateTime.Parse("1989-07-21"),
                Genre = "Romantic Comedy",
                Price = 7.99M,
                Rating = "R"
            },

            new Movie
            {
                Title = "Ghostbusters",
                ReleaseDate = DateTime.Parse("1984-06-08"),
                Genre = "Comedy",
                Price = 8.99M,
                Rating = "PG"
            },

            new Movie
            {
                Title = "Ghostbusters 2",
                ReleaseDate = DateTime.Parse("1989-06-16"),
                Genre = "Comedy",
                Price = 9.99M,
                Rating = "PG"
            },

            new Movie
            {
                Title = "The Dark Knight",
                ReleaseDate = DateTime.Parse("2008-07-18"),
                Genre = "Action",
                Price = 13.99M,
                Rating = "PG-13"
            },

            new Movie
            {
                Title = "Avatar",
                ReleaseDate = DateTime.Parse("2009-12-18"),
                Genre = "Science Fiction",
                Price = 14.99M,
                Rating = "PG-13"
            }
        );

        context.SaveChanges();
    }
}