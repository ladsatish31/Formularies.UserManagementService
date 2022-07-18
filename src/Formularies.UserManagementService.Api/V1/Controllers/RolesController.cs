using Formularies.UserManagementService.Core.Interfaces.Services;
using Formularies.UserManagementService.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Api.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        private readonly ILogger<RolesController> _logger;
        public RolesController(IRoleService roleService, ILogger<RolesController> logger)
        {
            _roleService = roleService??throw new ArgumentNullException(nameof(roleService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get all the roles
        /// </summary>
        /// <returns>Roles</returns>
        /// <remarks>
        /// Tables used => Role
        /// </remarks>
        [HttpGet]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            try
            {
                var response = await _roleService.GetAllRoles().ConfigureAwait(false);
                if (response == null)
                {                    
                    return NoContent();
                }
                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while tyring to call GetRoles in RoleController class, Error message={ex.Message}");
                return null;
            }
        }
    }
}
