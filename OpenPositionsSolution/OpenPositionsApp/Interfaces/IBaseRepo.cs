namespace OpenPositionsApp.Interfaces
{
    public interface IBaseRepo<P>
    {
        Task<ICollection<P>> GetAll();
    }
}
