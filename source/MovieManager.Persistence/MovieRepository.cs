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

        public (string Title, int Duration) GetMovieWithLongestDuration()
        {
            var movie = _dbContext.Movies
                .OrderByDescending(movie => movie.Duration)
                .ThenBy(movie => movie.Title)
                .First();

            return Tuple.Create(movie.Title, movie.Duration).ToValueTuple();
        }
    }
}