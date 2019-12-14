﻿using System.Net;
using System.Threading.Tasks;
using Example.Library.DataStores;
using Example.Library.DataStores.EntityFrameworkCore;
using Example.Library.Models;
using Microsoft.AspNetCore.Mvc;
using XPike.Logging;

namespace XPikeDataStores.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController
        : ControllerBase
    {
        private readonly ILog<UserController> _logger;
        private readonly IExampleDataStore _exampleDataStore;
        private readonly IEntityFrameworkExampleDataStore _entityFramework;

        public UserController(ILog<UserController> logger,
            IExampleDataStore exampleDataStore,
            IEntityFrameworkExampleDataStore entityFramework)
        {
            _logger = logger;
            _exampleDataStore = exampleDataStore;
            _entityFramework = entityFramework;
        }

        [HttpGet("ef/{userId}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EfGetUserAsync([FromRoute] int userId)
        {
            var user = await _entityFramework.GetExampleAsync(userId).ConfigureAwait(false);
            return user == null ? (IActionResult) NotFound() : Ok(user);
        }

        [HttpPost("ef")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EfCreateUserAsync([FromBody] User user)
        {
            return Ok(await _entityFramework.CreateExampleAsync(user).ConfigureAwait(false));
        }

        [HttpPut("ef/{userId}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EfGetUserAsync([FromRoute] int userId, [FromBody] User user)
        {
            user.Id = userId;

            return Ok(await _entityFramework.UpdateExampleAsync(user).ConfigureAwait(false));
        }

        [HttpDelete("ef/{userId}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EfDeleteUserAsync([FromRoute] int userId)
        {
            return Ok(await _entityFramework.DeleteExampleAsync(userId).ConfigureAwait(false));
        }
        
        [HttpGet("mysql/{userId}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserAsync([FromRoute] int userId)
        {
            var user = await _exampleDataStore.GetExampleAsync(userId).ConfigureAwait(false);
            return user == null ? (IActionResult) NotFound() : Ok(user);
        }

        [HttpPost("mysql")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            return Ok(await _exampleDataStore.CreateExampleAsync(user).ConfigureAwait(false));
        }

        [HttpPut("mysql/{userId}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserAsync([FromRoute] int userId, [FromBody] User user)
        {
            user.Id = userId;

            return Ok(await _exampleDataStore.UpdateExampleAsync(user).ConfigureAwait(false));
        }

        [HttpDelete("mysql/{userId}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int userId)
        {
            return Ok(await _exampleDataStore.DeleteExampleAsync(userId).ConfigureAwait(false));
        }
    }
}