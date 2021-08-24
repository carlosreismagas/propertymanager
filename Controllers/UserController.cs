using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyManager.Data;
using PropertyManager.Models;
using System.Linq;
using System.Collections.Generic;

namespace PropertyManager.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DBContext _db;
        public UserController(DBContext db)
        {
            _db = db;
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _db.User.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost, Route("Login")]
        public async Task<ActionResult> Login([FromBody] User model)
        {
            var user = await _db.User.FirstOrDefaultAsync(x => x.Username == model.Username);
            if (user == null) { return NotFound(); }
            if (user.Password == model.Password)
            {
                return Ok(new
                {
                    User = user,
                    Token = "123"
                });
            }

            return NotFound();
        }

        // POST api/property/Register
        [HttpPost, Route("Register")]
        public async Task<ActionResult> Register([FromBody] User model)
        {
            model.CreatedUser = model.Name;
            model.UpdatedUser = model.Name;
            model.CreatedDate = DateTime.Now;
            model.UpdatedDate = DateTime.Now;
            model.IsActive = true;
            model.IsDeleted = false;

            _db.User.Add(model);
            await _db.SaveChangesAsync();

            return Ok(new
                {
                    User = model,
                    Token = "123"
                });
        }
    }
}
