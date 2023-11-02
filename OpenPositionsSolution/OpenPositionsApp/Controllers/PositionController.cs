using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OpenPositionsApp.ErrorMessages;
using OpenPositionsApp.Exceptions;
using OpenPositionsApp.Interfaces;
using OpenPositionsApp.Models.DTO;
using System.Diagnostics.CodeAnalysis;

namespace OpenPositionsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactCors")]
    [ExcludeFromCodeCoverage]
    public class PositionController : ControllerBase
    {
        #region PrivateReadonlyVariables
        private readonly IPositionService _positionService;
        private readonly ILogger<PositionController> _logger;
        #endregion

        #region Constructor
        public PositionController(IPositionService positionService, ILogger<PositionController> logger)
        {
            _positionService = positionService;
            _logger=logger;
        }
        #endregion

        #region AddPosition
        [Authorize(Roles ="HR")]
        [HttpPost("Add")]
        [ProducesResponseType(typeof(OpenPositionDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OpenPositionDTO>> AddPosition(OpenPositionDTO position)
        {
            try
            {
                var result = await _positionService.Add(position);
                return Created(new Messages().messages[8],result);
            }
            catch (NullResultException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Messages().messages[0]);
            }
            catch (DatabaseException ex) 
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (InvalidLocationException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }
        #endregion

        #region RemovePosition
        [Authorize(Roles = "HR")]
        [HttpDelete]
        [ProducesResponseType(typeof(OpenPositionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OpenPositionDTO>> RemovePosition(PositionDTO position)
        {
            try
            {
                var result = await _positionService.Delete(position.PositionId);
                return Ok(result);
            }
            catch (NullResultException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Messages().messages[2]);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Messages().messages[0]);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region UpdatePosition
        [Authorize(Roles = "HR")]
        [HttpPut]
        [ProducesResponseType(typeof(OpenPositionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OpenPositionDTO>> UpdatePosition(OpenPositionDTO position)
        {
            try
            {
                var result = await _positionService.Update(position);
                return Ok(result);
            }
            catch (NullResultException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Messages().messages[3]);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Messages().messages[0]);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (InvalidLocationException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }
        #endregion

        #region GetAllPositions
        [HttpGet("All")]
        [ProducesResponseType(typeof(ICollection<OpenPositionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<OpenPositionDTO>>> GetAllPosition()
        {
            try
            {
                var result = await _positionService.GetAll();
                if (result != null)
                    return Ok(result);
            }
            catch (NullResultException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Messages().messages[4]);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Messages().messages[0]);
            }
            return BadRequest(new Messages().messages[4]);
        }
        #endregion

        #region GetAllLocations
        [HttpGet("AllLocations")]
        [ProducesResponseType(typeof(ICollection<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<string>>> GetAllLocations()
        {
            try
            {
                var result = await _positionService.GetAllLocations();
                return Ok(result);
            }
            catch (NullResultException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Messages().messages[5]);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Messages().messages[0]);
            }
            catch (DatabaseException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
