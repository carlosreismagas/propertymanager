using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyManager.Data;
using PropertyManager.Models;
using System.Collections.Generic;
using System.Linq;

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
        public async Task<ActionResult> CreateProperty([FromBody] Property model, [FromBody] List<Files> files)
        {
            try
            {  
                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;

                _db.Property.Add(model);
                await _db.SaveChangesAsync();

                files.ForEach(file => {
                    file.OriginID = model.Id;
                    file.Type = 1; //[Type 1 indicates property]
                    file.CreatedUser = model.CreatedUser;
                    file.CreatedDate = model.CreatedDate;
                    file.UpdatedUser = model.UpdatedUser;
                    file.UpdatedDate = model.UpdatedDate;
                });

                _db.Files.AddRange(files);
                await _db.SaveChangesAsync();
                
                return CreatedAtRoute(nameof(GetPropertyById), new { model.Id }, model);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        // PUT api/property/{id}
        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateProperty(int Id, [FromBody] Property model)
        {
            try
            {
                Property prop = await _db.Property.FirstOrDefaultAsync(x => x.Id == Id);
                if(prop == null) {
                    return NotFound();
                }

                prop.PropertyType = model.PropertyType;
                prop.Description = model.Description;
                prop.Address = model.Address;
                prop.UpdatedUser = model.UpdatedUser;
                prop.UpdatedDate = DateTime.Now;
                _db.Entry(prop).State = EntityState.Modified;

                byte type = 1;
                var filesToRemove = _db.Files.Where(x => x.OriginID == Id && x.Type == type).ToList();
                _db.Files.RemoveRange(filesToRemove);
                model.Files.ForEach(file => {
                    file.OriginID = Id;
                    file.Type = 1; //[Type 1 indicates property] // checkout how to bring files
                    file.CreatedUser = model.CreatedUser;
                    file.CreatedDate = model.CreatedDate;
                    file.UpdatedUser = model.UpdatedUser;
                    file.UpdatedDate = model.UpdatedDate;
                });
                _db.Files.AddRange(model.Files);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
    }
}
