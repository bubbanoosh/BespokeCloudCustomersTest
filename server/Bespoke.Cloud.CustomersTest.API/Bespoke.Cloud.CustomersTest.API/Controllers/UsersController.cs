using AutoMapper;
using Bespoke.Cloud.CustomersTest.API.Helpers;
using Bespoke.Cloud.CustomersTest.API.Dtos;
using Bespoke.Cloud.CustomersTest.Business.Interfaces;
using Bespoke.Cloud.CustomersTest.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Bespoke.Cloud.CustomersTest.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private ILogger<UsersController> _logger;
        private IUserManager _usersManager;
        private IMapper _mapper;
        private AppSettings _appSettings;

        public UsersController(IUserManager userManger,
            ILogger<UsersController> logger,
            IMapper mapper,
            IConfiguration config,
            IOptions<AppSettings> appSettings
            )
        {
            _usersManager = userManger;
            _logger = logger;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// POST: api/Users
        /// Registration
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost(Name = "Register")]
        public async Task<IActionResult> Register([FromBody]UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }

            // map dto to entity
            var user = _mapper.Map<User>(userDto);

            try
            {
                var registeredUser = await _usersManager.Register(user, userDto.Password);

                return CreatedAtRoute("Register",
                    new { registeredUser.Id },
                    registeredUser);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// POST: api/Users/login
        /// Login authentication
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login/", Name = "authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]UserDto userDto)
        {
            var user = await _usersManager.Authenticate(userDto.Email, userDto.Password);

            if (user == null)
                return BadRequest("Username or password is incorrect");

            var tokenString = CreateToken(user.Id);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                Token = tokenString
            });
        }

        /// <summary>
        /// GET: api/Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _usersManager.GetUsers("");
            var usersToReturn = _mapper.Map<IEnumerable<UserDisplayDto>>(users);

            return Ok(usersToReturn);
        }

        /// <summary>
        /// GET: api/Users/list/searchText?
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpGet("list/{searchText?}", Name = "SearchUsers")]
        public IActionResult GetUsers(string searchText = "")
        {
            var users = _usersManager.GetUsers(searchText);
            var usersToReturn = _mapper.Map<IEnumerable<UserDisplayDto>>(users);

            return Ok(usersToReturn);
        }

        /// <summary>
        /// GET: api/Users/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(int id)
        {
            var user = _usersManager.GetUserById(id);

            if (user == null)
            {
                return NotFound("No User found");
            }
            else
            {
                var userDto = _mapper.Map<UserDisplayDto>(user);
                return Ok(userDto);
            }
        }

        /// <summary>
        /// PUT: api/Users/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = id;

            if (user == null)
            {
                return BadRequest();
            }

            var userToUpdate = _usersManager.GetUserById(user.Id);

            if (userToUpdate == null)
            {
                return NotFound($"No User found to 'Update' with id: {id}");
            }
            else
            {
                var updated = _usersManager.UpdateUser(userToUpdate);
                if (!updated)
                {
                    throw new Exception($"User {id} could not be updated on save.");
                }

                return NoContent(); // 204 No Content
            }
        }

        /// <summary>
        /// DELETE: api/Users/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _usersManager.GetUserById(id);

            if (user == null)
            {
                return NotFound($"No User found to 'Delete' with id: {id}");
            }
            else
            {
                if (!_usersManager.RemoveUser(id))
                {
                    throw new Exception($"Deleting user {id} failed on save.");
                }

                _logger.LogInformation(100, $"User {id} was DELETED!!");

                return NoContent(); // 204 No Content
            }
        }

        #region Private methods
        private string CreateToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtKey);
            var authenticationDays = _appSettings.AuthenticationDays;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(authenticationDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
        #endregion

    }
}
