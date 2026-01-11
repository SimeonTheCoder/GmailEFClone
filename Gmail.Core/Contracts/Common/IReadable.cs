namespace Gmail.Core.Contracts.Common
{
    public interface IReadable<TModel> where TModel : class
    {
        TModel GetById(string id);
    }
}
