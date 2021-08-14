using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyManager.Data;

namespace PropertyManager.Controllers
{
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly DBContext _db;

        public PropertyController(DBContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProperties()
        {
            var properties = await _db.Property.ToListAsync();
            return Ok(properties);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetPropertyById(int Id)
        {
            var property = await _db.Property.FirstOrDefaultAsync(x => x.Id == Id);
            return Ok(property);
        }
    }
}
