using Microsoft.EntityFrameworkCore;
using OpenPositionsApp.Exceptions;
using OpenPositionsApp.Interfaces;
using OpenPositionsApp.Models;
using OpenPositionsApp.Models.Context;
using System.Diagnostics.CodeAnalysis;


namespace OpenPositionsApp.Repositories
{
    [ExcludeFromCodeCoverage]
    public class PositionRepo : IPositionRepo<OpenPosition, int>
    {
        #region PrivateVariables
        private readonly OpenPositionContext _context;
        #endregion

        #region Constructor
        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="context"></param>
        public PositionRepo(OpenPositionContext context)
        {
            _context = context;
        }
        #endregion

        #region Add
        /// <summary>
        /// Adds an OpenPosition item to the data store.
        /// </summary>
        /// <param name="item">The OpenPosition object to be added.</param>
        /// <returns>The added OpenPosition object.</returns>
        /// <exception cref="DatabaseException">Thrown when the context or OpenPositions collection is not properly initialized.</exception>
        public async Task<OpenPosition> Add(OpenPosition item)
        {
            if(_context != null && _context.OpenPositions != null)
            {
                 _context.OpenPositions.Add(item);
                 await _context.SaveChangesAsync();
                 return item;
            }
            else
                throw new DatabaseException();
        }
        #endregion

        #region Delete
        /// <summary>
        /// Deletes an OpenPosition item from the data store.
        /// </summary>
        /// <param name="key">The key of the OpenPosition item to be deleted.</param>
        /// <returns>The deleted OpenPosition object.</returns>
        /// <exception cref="DatabaseException">Thrown when the context or OpenPositions collection is not properly initialized.</exception>
        /// <exception cref="NullResultException">Thrown when the requested OpenPosition item does not exist.</exception>
        public async Task<OpenPosition> Delete(int key)
        {
            if (_context != null && _context.OpenPositions != null)
            {
                var position = await Get(key);
                if (position != null)
                {
                    _context.OpenPositions.Remove(position);
                    await _context.SaveChangesAsync();
                    return position;
                }
            }
            else
                throw new DatabaseException();
            throw new NullResultException();
        }
        #endregion

        #region Get
        /// <summary>
        /// Retrieves an OpenPosition item from the data store based on the provided key.
        /// </summary>
        /// <param name="key">The key of the OpenPosition item to be retrieved.</param>
        /// <returns>The retrieved OpenPosition object.</returns>
        /// <exception cref="DatabaseException">Thrown when the context or OpenPositions collection is not properly initialized.</exception>
        /// <exception cref="NullResultException">Thrown when the requested OpenPosition item does not exist.</exception>
        public async Task<OpenPosition> Get(int key)
        {
            if (_context != null && _context.OpenPositions != null)
            {
                var position = await _context.OpenPositions.FirstOrDefaultAsync(u => u.PositionId == key);
                if (position != null)
                    return position;
            }
            else
                throw new DatabaseException();
            throw new NullResultException();
        }
        #endregion

        #region GetAll
        /// <summary>
        /// Retrieves a collection of all OpenPosition items from the data store.
        /// </summary>
        /// <returns>A collection of OpenPosition objects.</returns>
        /// <exception cref="DatabaseException">Thrown when the context or OpenPositions collection is not properly initialized.</exception>
        /// <exception cref="NullResultException">Thrown when no OpenPosition items are available.</exception>
        public async Task<ICollection<OpenPosition>> GetAll()
        {
            if (_context != null && _context.OpenPositions != null)
            {
                var positions = await _context.OpenPositions.ToListAsync();
                if (positions.Count > 0)
                    return positions;
            }
            else
                throw new DatabaseException();
            throw new NullResultException();
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates an OpenPosition item in the data store.
        /// </summary>
        /// <param name="item">The OpenPosition object to be updated.</param>
        /// <returns>The updated OpenPosition object.</returns>
        /// <exception cref="DatabaseException">Thrown when the context or OpenPositions collection is not properly initialized.</exception>
        /// <exception cref="NullResultException">Thrown when the specified OpenPosition item does not exist.</exception>
        public async Task<OpenPosition> Update(OpenPosition item)
        {
            if(_context!=null && _context.OpenPositions != null)
            {
                var position = await Get(item.PositionId);
                if (position != null)
                {
                    position.RoleName = item.RoleName;
                    position.Domain = item.Domain;
                    position.JobDescription = item.JobDescription;
                    position.LocationId = item.LocationId;
                    position.ReqSkills = item.ReqSkills;
                    position.EducationalQual = item.EducationalQual;
                    position.Experience = item.Experience;
                    await _context.SaveChangesAsync();
                    return position;
                }
            }
            else
                throw new DatabaseException();
            throw new NullResultException();
        }
        #endregion
    }
}
 