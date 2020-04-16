namespace MovieManager.Core.Contracts
{
    public interface ICategoryRepository
    {
        (string Categories, int Count, int Duration)[] GetListOfCategories();
        (string Categories, double Duration)[] GetListOfCategoriesWithAvgDuration();
    }
}
