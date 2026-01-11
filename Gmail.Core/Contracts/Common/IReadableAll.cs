namespace Gmail.Core.Contracts.Common
{
    public interface IReadableAll<TModel> where TModel : class
    {
        List<TModel> GetAll();
        List<TModel> GetAllAsNoTracking();
    }
}
