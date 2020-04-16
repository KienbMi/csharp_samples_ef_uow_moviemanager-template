using MovieManager.Core.Contracts;
using System.Linq;

namespace MovieManager.Persistence
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public (string Categories, int Count, int Duration)[] GetListOfCategories()
        {
            return _dbContext.Categories
                .Select(_ => new
                {
                    CategoryName = _.CategoryName,
                    Count = _.Movies.Count(),
                    SumDuration = _.Movies.Sum(_ => _.Duration)
                })
                .OrderBy(_ => _.CategoryName)
                .AsEnumerable()
                .Select(_ => (_.CategoryName, _.Count, _.SumDuration))
                .ToArray();
        }

        public (string Categories, double Duration)[] GetListOfCategoriesWithAvgDuration()
        {
            return _dbContext.Categories
                .Select(_ => new
                {
                    CategoryName = _.CategoryName,
                    AvgDuration = _.Movies.Average(_ => _.Duration)
                })
                .OrderByDescending(_ => _.AvgDuration)
                .AsEnumerable()
                .Select(_ => (_.CategoryName, _.AvgDuration))
                .ToArray();
        }
    }
}