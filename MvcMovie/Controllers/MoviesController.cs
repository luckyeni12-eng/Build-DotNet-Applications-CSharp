using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers;

public class MoviesController : Controller
{
    private readonly MvcMovieContext _context;

    public MoviesController(MvcMovieContext context)
    {
        _context = context;
    }

    // GET: Movies
    public async Task<IActionResult> Index(
        string? movieGenre,
        string? searchString,
        int? year)
    {
        if (_context.Movie == null)
        {
            return Problem(
                "Entity set 'MvcMovieContext.Movie' is null.");
        }

        // Get all available genres.
        IQueryable<string> genreQuery =
            from m in _context.Movie
            orderby m.Genre
            select m.Genre!;

        // Start with all movies.
        IQueryable<Movie> movies =
            from m in _context.Movie
            select m;

        // Search by movie title.
        if (!string.IsNullOrWhiteSpace(searchString))
        {
            movies = movies.Where(
                m => m.Title != null &&
                     m.Title.ToUpper().Contains(
                         searchString.ToUpper()));
        }

        // Filter by genre.
        if (!string.IsNullOrWhiteSpace(movieGenre))
        {
            movies = movies.Where(
                m => m.Genre == movieGenre);
        }

        // Rubric requirement:
        // Show movies released in the selected year or newer.
        if (year.HasValue)
        {
            var startDate = new DateTime(year.Value, 1, 1);

            movies = movies.Where(
                m => m.ReleaseDate >= startDate);
        }

        var movieGenreVM = new MovieGenreViewModel
        {
            Genres = new SelectList(
                await genreQuery
                    .Distinct()
                    .ToListAsync()),

            Movies = await movies
                .OrderBy(m => m.ReleaseDate)
                .ToListAsync(),

            MovieGenre = movieGenre,

            SearchString = searchString,

            Year = year
        };

        return View(movieGenreVM);
    }

    // GET: Movies/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // GET: Movies/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Movies/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind(
            "Id,Title,ReleaseDate,Genre,Price,Rating")]
        Movie movie)
    {
        if (ModelState.IsValid)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(movie);
    }

    // GET: Movies/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie.FindAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: Movies/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind(
            "Id,Title,ReleaseDate,Genre,Price,Rating")]
        Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(movie);
    }

    // GET: Movies/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var movie = await _context.Movie
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: Movies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await _context.Movie.FindAsync(id);

        if (movie != null)
        {
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int id)
    {
        return _context.Movie.Any(e => e.Id == id);
    }
}