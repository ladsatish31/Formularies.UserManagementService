using Formularies.UserManagementService.Core.Interfaces.Services;
using Formularies.UserManagementService.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService??throw new ArgumentNullException(nameof(roleService));
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
            var response = await _roleService.GetAllRoles().ConfigureAwait(false);
            if(response == null)
            {
                return NoContent();
            }
            return Ok(response);
        }
    }
}
