namespace OpenPositionsApp.Interfaces
{
    public interface IPositionRepo<P,I> : IBaseRepo<P>
    {
        Task<P> Add(P item);
        Task<P> Update(P item);
        Task<P> Delete(I key);
        Task<P> Get(I key);
    }
}
