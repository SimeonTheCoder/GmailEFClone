namespace Gmail.Core.Contracts.Common
{
    public interface IUpdatable<TEntity> where TEntity : class
    {
        void Update(TEntity entity);
    }
}
