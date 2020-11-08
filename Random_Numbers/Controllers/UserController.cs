using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Random_Numbers.Data;
using Random_Numbers.Models;

namespace Random_Numbers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly RandomNumberDbContext _context;

        public UserController(RandomNumberDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            var validation = new UserValidator().Validate(user);
            AddValidationResult(validation);

            if (!validation.IsValid)
                return CustomResponse();

            var userDb = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

            if (userDb != null)
            {
                AddErrorToList("Already exists an account using this username.");
                return CustomResponse();
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CustomResponse();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var validation = new UserValidator().Validate(user);
            AddValidationResult(validation);

            if (!validation.IsValid)
                return CustomResponse();

            var userDb = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);

            if (userDb == null)
            {
                AddErrorToList("User name and password doesn't match.");
                return CustomResponse();
            }

            return CustomResponse();
        }
    }
}
