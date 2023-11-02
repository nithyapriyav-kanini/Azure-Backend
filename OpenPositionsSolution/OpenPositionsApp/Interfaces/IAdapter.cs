using OpenPositionsApp.Models.DTO;
using OpenPositionsApp.Models;

namespace OpenPositionsApp.Interfaces
{
    public interface IAdapter
    {
        public Task<OpenPositionDTO> Mapper(OpenPosition item);
        public Task<OpenPosition> Mapper(OpenPositionDTO item);
        public Task<int> Mapper(string name);
        public Task<string?> Mapper(int key);
    }
}
