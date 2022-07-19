using Formularies.UserManagementService.Core.Interfaces.Services;
using Formularies.UserManagementService.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Formularies.UserManagementService.Api.V1.Controllers
{
    [Produces("application/json")]
    [Route("api/" + ApiConstants.ServiceName + "/v{api-version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Get all the users
        /// </summary>
        /// <returns>Users</returns>
        /// <remarks>
        /// Tables used => Users
        /// </remarks>
        [HttpGet(Name = "GetUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var response = await _userService.GetAllUsers().ConfigureAwait(false);
            return response != null ? Ok(response) : NotFound();
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User</returns>
        /// <remarks>
        /// Tables used => Users
        /// </remarks>
        [HttpGet("{id}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {            
            var response = await _userService.GetUserById(id).ConfigureAwait(false);
            return response != null ? Ok(response) : NotFound();

        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="User"></param>
        /// <returns>User</returns>
        /// <remarks>
        /// Tables used => Users
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost(Name = "CreateUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> CreateUser(User User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var response = await _userService.CreateUser(User).ConfigureAwait(false);
            return CreatedAtRoute(nameof(GetUserById), new { id = response.UserId }, response);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true/false</returns>
        /// <remarks>
        /// Tables used => Users
        /// </remarks>
        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteUser(Guid id)
        {           
            var response = await _userService.DeleteUser(id).ConfigureAwait(false);
            return response ? NoContent() : NotFound();
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="User"></param>
        /// <returns>true/false</returns>
        /// <remarks>
        /// Tables used => Users
        /// </remarks>
        [HttpPut("{id}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UpdateUser(Guid id, User User)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var response = await _userService.UpdateUser(id, User).ConfigureAwait(false);
            return response ? Ok(response) : NotFound();
        }
    }
}
