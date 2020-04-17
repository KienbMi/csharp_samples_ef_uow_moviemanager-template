using Microsoft.EntityFrameworkCore;
using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieManager.Persistence
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MovieRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddRange(IEnumerable<Movie> range)
        {
            _dbContext.AddRange(range);
        }

        public (string Categorie, int CountMovies) GetCategorieWithMostMovies()
        {
            // KORREKTUR: Linq Abfrage von Categories und damit auf Datenbankebene
            var category = _dbContext.Categories
                .Select(_ => new
                {
                    Category = _.CategoryName,
                    Count = _.Movies.Count()
                })
                .OrderByDescending(_ => _.Count)
                .First();

            return Tuple.Create(category.Category, category.Count).ToValueTuple();          
        }

        public (string Title, int Duration) GetMovieWithLongestDuration()
        {
            var movie = _dbContext.Movies
                .OrderByDescending(movie => movie.Duration)
                .ThenBy(movie => movie.Title)
                .First();

            return Tuple.Create(movie.Title, movie.Duration).ToValueTuple();
        }

        public int GetYearWithMostActionMovies()
        {
            // KORREKTUR: Linq Abfrage Fehler behogen
            return _dbContext.Movies
                .Where(movie => movie.Category.CategoryName.Equals("Action"))
                .GroupBy(movie => movie.Year)
                .Select(_ => new
                {
                    _.Key,
                    Count = _.Count()
                })
                .OrderByDescending(_ => _.Count)
                .First().Key;
        }
    }
}