using OpenPositionsApp.Models.DTO;

namespace OpenPositionsApp.Interfaces
{
    public interface IPositionService
    {
        Task<OpenPositionDTO> Add(OpenPositionDTO item);
        Task<OpenPositionDTO> Update(OpenPositionDTO item);
        Task<OpenPositionDTO> Delete(int key);
        Task<OpenPositionDTO> Get(int key);
        Task<ICollection<OpenPositionDTO>> GetAll();
        Task<ICollection<string?>> GetAllLocations();
    }
}
