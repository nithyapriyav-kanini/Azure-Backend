using Microsoft.EntityFrameworkCore;
using OpenPositionsApp.Exceptions;
using OpenPositionsApp.Interfaces;
using OpenPositionsApp.Models;
using OpenPositionsApp.Models.Context;
using System.Diagnostics.CodeAnalysis;

namespace OpenPositionsApp.Repositories
{
    [ExcludeFromCodeCoverage]
    public class LocationRepo : IBaseRepo<Location>
    {
        #region Private Variables
        private readonly OpenPositionContext _context;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the LocationRepo class with a parameterized constructor.
        /// </summary>
        /// <param name="context">The OpenPositionContext used to interact with the data store.</param>
        public LocationRepo(OpenPositionContext context)
        {
            _context = context;
        }
        #endregion

        #region GetAll Locations
        /// <summary>
        /// Retrieves a list of all locations from the data store.
        /// </summary>
        /// <returns>A collection of Location objects.</returns>
        /// <exception cref="NullResultException">Thrown when the query for locations returns no results.</exception>
        /// <exception cref="DatabaseException">Thrown when the context is not properly initialized.</exception>
        public async Task<ICollection<Location>> GetAll()
        {
            if (_context.Locations != null)
            {
                var locations = await _context.Locations.ToListAsync();
                if (locations.Count > 0)
                    return locations;
                throw new NullResultException();
            }
            else
                throw new DatabaseException("Context not Initialized");
        }
        #endregion
    }
}