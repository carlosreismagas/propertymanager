using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyManager.Data;
using PropertyManager.Models;

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

        [HttpPost]
        public async Task<ActionResult> Login(User model)
        {
            var user = await _db.User.FirstOrDefaultAsync(x => x.Username == model.Username);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Password == model.Password)
            {
                return Ok(new
                {
                    User = user,
                    Token = ""
                });
            }

            return NotFound();
        }

        // POST api/property/UserRegistration
        [HttpPost]
        public async Task<ActionResult> UserRegistration(User model)
        {
            model.CreatedUser = model.Name;
            model.UpdatedUser = model.Name;
            model.CreatedDate = DateTime.Now;
            model.UpdatedDate = DateTime.Now;
            model.IsActive = true;
            model.IsDeleted = false;

            _db.User.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetUserById", model.Id, model);
        }
    }
}
