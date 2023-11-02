using Microsoft.EntityFrameworkCore;
using OpenPositionsApp.Exceptions;
using OpenPositionsApp.Interfaces;
using OpenPositionsApp.Models;
using OpenPositionsApp.Models.DTO;
using OpenPositionsApp.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace OpenPositionsApp.Adapter
{
    [ExcludeFromCodeCoverage]
    public class Adapter : IAdapter
    {
        #region PrivateVariables
        private readonly IBaseRepo<Location> _LocationRepo;
        #endregion

        #region Constructor
        public Adapter(IBaseRepo<Location> LocationRepo)
        {
            _LocationRepo = LocationRepo;
        }
        #endregion

        #region OpenPositionToOpenPositionDTO
        public async Task<OpenPositionDTO> Mapper(OpenPosition item)
        {
            OpenPositionDTO result = new()
            {
                PositionId = item.PositionId,
                RoleName = item.RoleName,
                Domain = item.Domain,
                JobDescription = item.JobDescription,
                Location = await Mapper(item.LocationId),
                ReqSkills = item.ReqSkills,
                EducationalQual = item.EducationalQual,
                Experience = item.Experience
            };
            return result;
        }
        #endregion

        #region OpenPositionDTOToOpenPosition
        public async Task<OpenPosition> Mapper(OpenPositionDTO item)
        {
            OpenPosition result = new()
            {
                PositionId = item.PositionId,
                RoleName = item.RoleName,
                Domain = item.Domain,
                JobDescription = item.JobDescription
            };
            if (item.Location != null)
            {
                result.LocationId = await Mapper(item.Location);
            }
            result.ReqSkills = item.ReqSkills;
            result.EducationalQual = item.EducationalQual;
            result.Experience = item.Experience;
            return result;
        }
        #endregion

        #region FetchLocationIdByName
        public async Task<int> Mapper(string name)
        {
            var locations = await _LocationRepo.GetAll();
            if (locations != null)
            {
                var location = locations.FirstOrDefault(u => u.LocationName!.ToLower() == name.ToLower());
                if (location != null)
                {
                    return location.LocationId;
                }
            }
            throw new InvalidLocationException();
        }
        #endregion

        #region FetchLocationNameById
        public async Task<string?> Mapper(int key)
        {
            var locations = await _LocationRepo.GetAll();
            if (locations != null)
            {
                var location = locations.FirstOrDefault(u => u.LocationId == key);
                if (location != null)
                {
                    return location.LocationName;
                }
            }
            throw new InvalidLocationException();
        }
        #endregion
    }
}
