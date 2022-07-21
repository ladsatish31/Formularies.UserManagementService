using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Formularies.UserManagementService.Api.Helper;
using Formularies.UserManagementService.Core.Helpers;
using Formularies.UserManagementService.Core.Interfaces.Services;
using Formularies.UserManagementService.Core.Models;
using Formularies.UserManagementService.Core.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Api.V2.Controllers
{
    [Produces("application/json")]
    [Route("api/v{api-version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public new User User => (User)HttpContext.Items["User"];
        private readonly IUriService _uriService;

        public RolesController(IRoleService roleService, IUriService uriService)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _uriService = uriService;
        }

        /// <summary>
        /// Get all the roles
        /// </summary>
        /// <returns>Roles</returns>
        /// <remarks>
        /// Tables used => Roles
        /// </remarks>
        //[HttpGet(Name = "GetRoles")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        //{
        //    var response = await _roleService.GetAllRoles().ConfigureAwait(false);
        //    return response != null ? Ok(response) : NotFound();
        //}

        [HttpGet(Name = "GetAllRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Role>> GetAllRoles([FromQuery] PaginationFilter pageFilter, [FromQuery] SearchFilter reportFilter)
        {
            var route = Request.Path.Value;
            var response = _roleService.GetAllRoles(reportFilter);
            var validFilter = new PaginationFilter(pageFilter.PageNumber, pageFilter.PageSize);
            var pagedData = response
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToList();
            var totalRecords = response.Count();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Role>(pagedData, validFilter, totalRecords, _uriService, route);
            return pagedReponse != null ? Ok(pagedReponse) : NotFound();
        }

        /// <summary>
        /// Get role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Role</returns>
        /// <remarks>
        /// Tables used => Roles
        /// </remarks>
        [HttpGet("{id}", Name = "GetRoleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Role>> GetRoleById(int id)
        {
            if (id <= 0)
            {
                throw new ApiException(ModelState.AllErrors());
            }
            var response = await _roleService.GetRoleById(id).ConfigureAwait(false);
            return response != null ? Ok(new ApiResponse(response)) : NotFound();

        }

        /// <summary>
        /// Create role
        /// </summary>
        /// <param name="role"></param>
        /// <returns>Role</returns>
        /// <remarks>
        /// Tables used => Roles
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost(Name = "CreateRole")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleRequest>> CreateRole(RoleRequest role)
        {
            if (!ModelState.IsValid)
            {
                throw new ApiException(ModelState.AllErrors());
            }
            var response = await _roleService.CreateRole(role).ConfigureAwait(false);
            return CreatedAtRoute(nameof(GetRoleById), new { id = response.RoleId }, response);
        }

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true/false</returns>
        /// <remarks>
        /// Tables used => Roles
        /// </remarks>
        [HttpDelete("{id}", Name = "DeleteRole")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteRole(int id)
        {
            if (id <= 0)
            {
                throw new ApiException(ModelState.AllErrors());
            }
            var response = await _roleService.DeleteRole(id).ConfigureAwait(false);
            return response ? NoContent() : NotFound();
        }

        /// <summary>
        /// Update role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns>true/false</returns>
        /// <remarks>
        /// Tables used => Roles
        /// </remarks>
        [HttpPut("{id}", Name = "UpdateRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UpdateRole(int id, RoleRequest role)
        {
            if (!ModelState.IsValid || id <= 0)
            {
                throw new ApiException(ModelState.AllErrors());
            }
            var response = await _roleService.UpdateRole(id, role).ConfigureAwait(false);
            return response ? Ok(response) : NotFound();
        }
    }
}
