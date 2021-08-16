using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyManager.Data;
using PropertyManager.Models;

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

        [HttpGet("{Id}", Name = "GetPropertyById")]
        public async Task<ActionResult> GetPropertyById(int Id)
        {
            var property = await _db.Property.FirstOrDefaultAsync(x => x.Id == Id);
            return Ok(property);
        }

        // POST api/property
        [HttpPost]
        public async Task<ActionResult> CreatePropery(Property model)
        {
            model.CreatedDate = DateTime.Now;
            model.UpdatedDate = DateTime.Now;
            model.IsActive = true;
            model.IsDeleted = false;

            _db.Property.Add(model);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetPropertyById", model.Id, model);
        }
    }
}
