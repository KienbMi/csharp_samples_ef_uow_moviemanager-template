using MovieManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace MovieManager.Core
{
    public class ImportController
    {
        const string Filename = "movies.csv";
        const int IDX_TITLE = 0;
        const int IDX_YEAR = 1;
        const int IDX_CATEGORIE = 2;
        const int IDX_DURATION = 3;

        /// <summary>
        /// Liefert die Movies mit den dazugehörigen Kategorien
        /// </summary>
        public static IEnumerable<Movie> ReadFromCsv()
        {
            //Title; Year; CategoryName; Duration
            //A Few Good Men; 1992; Crime; 80
            //Empire Records; 1995; Comedy; 98

            string[][] data = MyFile.ReadStringMatrixFromCsv(Filename, true);

            var categories = data.GroupBy(movie => movie[IDX_CATEGORIE])
                .Select(movie => new Category
                {
                    CategoryName = movie.Key
                })
                .ToDictionary(categorie => categorie.CategoryName);

            var movies = data
                .Select(movie => new Movie
                {
                    Title = movie[IDX_TITLE],
                    Year = Convert.ToInt32(movie[IDX_YEAR]),
                    Duration = Convert.ToInt32(movie[IDX_DURATION]),
                    Category = categories[movie[IDX_CATEGORIE]]
                });

            return movies;
        }
    }
}
