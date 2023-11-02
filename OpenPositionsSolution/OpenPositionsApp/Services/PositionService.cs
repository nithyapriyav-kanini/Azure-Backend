using OpenPositionsApp.Exceptions;
using OpenPositionsApp.Interfaces;
using OpenPositionsApp.Models;
using OpenPositionsApp.Models.DTO;
using System.Data;

namespace OpenPositionsApp.Services
{
    public class PositionService : IPositionService
    {
        #region PrivateVariables
        private readonly IPositionRepo<OpenPosition, int> _positionRepo;
        private readonly IBaseRepo<Location> _locationRepo;
        private readonly IAdapter _adapter;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the PositionService class.
        /// </summary>
        /// <param name="PRepo">The repository for OpenPosition objects.</param>
        /// <param name="LRepo">The repository for Location objects.</param>
        /// <param name="adapter">The adapter used for data conversion.</param>
        public PositionService(IPositionRepo<OpenPosition,int> positionRepo,IBaseRepo<Location> locationRepo,IAdapter adapter)
        {
            _positionRepo = positionRepo;
            _locationRepo = locationRepo;
            _adapter = adapter;
        }
        #endregion

        #region AddPosition
        /// <summary>
        /// Adds an OpenPositionDTO item to the data store.
        /// </summary>
        /// <param name="item">The OpenPositionDTO object to be added.</param>
        /// <returns>The added OpenPositionDTO object.</returns>
        /// <exception cref="NullResultException">Thrown when the Location property of the input item is null.</exception>
        public async Task<OpenPositionDTO> Add(OpenPositionDTO item)
        {
            if (item.Location == null)
                throw new NullResultException();
            var position = await _adapter.Mapper(item);
            var result = await _positionRepo.Add(position);
            var pos = await _adapter.Mapper(result);
            return pos;
        }
        #endregion

        #region DeletePosition
        /// <summary>
        /// Deletes an OpenPositionDTO item from the data store based on the provided key.
        /// </summary>
        /// <param name="key">The key of the OpenPositionDTO item to be deleted.</param>
        /// <returns>The deleted OpenPositionDTO object.</returns>
        /// <exception cref="DatabaseException">Thrown when the context or OpenPositions collection is not properly initialized.</exception>
        /// <exception cref="NullResultException">Thrown when the requested OpenPositionDTO item does not exist.</exception>
        public async Task<OpenPositionDTO> Delete(int key)
        {
            var result=await _positionRepo.Delete(key);
            var position = await _adapter.Mapper(result);
            return position;
        }
        #endregion

        #region GetPosition
        /// <summary>
        /// Retrieves an OpenPositionDTO item from the data store based on the provided key.
        /// </summary>
        /// <param name="key">The key of the OpenPositionDTO item to be retrieved.</param>
        /// <returns>The retrieved OpenPositionDTO object.</returns>
        /// <exception cref="DatabaseException">Thrown when the context or OpenPositions collection is not properly initialized.</exception>
        /// <exception cref="NullResultException">Thrown when the requested OpenPositionDTO item does not exist.</exception>
        public async Task<OpenPositionDTO> Get(int key)
        {
            var result=await _positionRepo.Get(key);
            var position = await _adapter.Mapper(result);
            return position;
        }
        #endregion

        #region GetAllPositions
        /// <summary>
        /// Retrieves a collection of all OpenPositionDTO items from the data store.
        /// </summary>
        /// <returns>A collection of OpenPositionDTO objects.</returns>
        /// <exception cref="DatabaseException">Thrown when the context or OpenPositions collection is not properly initialized.</exception>
        /// <exception cref="NullResultException">Thrown when no OpenPositionDTO items are available.</exception>
        public async Task<ICollection<OpenPositionDTO>> GetAll()
        {
            var result = await _positionRepo.GetAll();
            List<OpenPositionDTO> list = new();
            foreach (var item in result)
            {
                var position = await _adapter.Mapper(item);
                list.Add(position);
            }
            if (list.Count > 0)
                return list;
            throw new NullResultException();
        }
        #endregion

        #region UpdatePosition
        /// <summary>
        /// Updates an OpenPositionDTO item in the data store.
        /// </summary>
        /// <param name="item">The updated OpenPositionDTO object.</param>
        /// <returns>The updated OpenPositionDTO object.</returns>
        /// <exception cref="NullResultException">Thrown when the Location property of the input item is null.</exception>
        /// <exception cref="DatabaseException">Thrown when the context or OpenPositions collection is not properly initialized.</exception>
        public async Task<OpenPositionDTO> Update(OpenPositionDTO item)
        {
            if (item.Location == null)
                throw new NullResultException();
            var position = await _adapter.Mapper(item);
            var result = await _positionRepo.Update(position);
            item = await _adapter.Mapper(result);
            return item;
        }
        #endregion

        #region GetAllLocations
        /// <summary>
        /// Retrieves a collection of all location names from the data store.
        /// </summary>
        /// <returns>A collection of location names.</returns>
        /// <exception cref="DatabaseException">Thrown when the context or Locations collection is not properly initialized.</exception>
        /// <exception cref="NullResultException">Thrown when no location names are available.</exception>
        public async Task<ICollection<string?>> GetAllLocations()
        {
            var locations=await _locationRepo.GetAll();
            List<string?> result = locations
                   .Where(location => location!.LocationName != null)
                   .Select(location => location!.LocationName)
                   .ToList();
            if (result.Count>0)
                return result;
            throw new NullResultException();
        }
        #endregion
    }
}
