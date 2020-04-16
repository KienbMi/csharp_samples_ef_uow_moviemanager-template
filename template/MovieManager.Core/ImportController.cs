using MovieManager.Core.Entities;
using System;
using System.Collections.Generic;

namespace MovieManager.Core
{
    public class ImportController
    {
        const string Filename = "movies.csv";

        /// <summary>
        /// Liefert die Movies mit den dazugehörigen Kategorien
        /// </summary>
        public static IEnumerable<Movie> ReadFromCsv()
        {
            throw new NotImplementedException();
        }

    }
}
