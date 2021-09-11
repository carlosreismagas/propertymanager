using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyManager.Data;
using PropertyManager.Models;
using System.Collections.Generic;
using System.Linq;
using PropertyManager.DTOs;

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
            return Ok(new PropertyDTO
            {
                Id = property.Id,
                Description = property.Description,
                PropertyType = property.PropertyType,
                ImageUrl = property.ImageUrl,
                Address = property.Address,
                IsActive = property.IsActive,
                CreatedDate = property.CreatedDate,
                CreatedUser = property.CreatedUser,
                UpdatedDate = property.UpdatedDate,
                UpdatedUser = property.UpdatedUser,
                IsDeleted = property.IsDeleted,
                Files = _db.Files.Where(x => x.OriginID == property.Id && x.Type == 1).ToList()
            });
        }

        // POST api/property
        [HttpPost]
        public async Task<ActionResult> CreateProperty([FromBody] PropertyDTO modelDTO)
        {
            try
            {
                var model = new Property()
                {
                    Description = modelDTO.Description,
                    PropertyType = modelDTO.PropertyType,
                    ImageUrl = modelDTO.ImageUrl,
                    Address = modelDTO.Address
                };

                model.CreatedDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;
                _db.Property.Add(model);

                modelDTO.Files.ForEach(file =>
                {
                    file.OriginID = modelDTO.Id;
                    file.Type = 1; //[Type 1 indicates property]
                    file.CreatedUser = modelDTO.CreatedUser;
                    file.CreatedDate = modelDTO.CreatedDate;
                    file.UpdatedUser = modelDTO.UpdatedUser;
                    file.UpdatedDate = modelDTO.UpdatedDate;
                });

                var files = modelDTO.Files;
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
        public async Task<ActionResult> UpdateProperty(int Id, [FromBody] PropertyDTO modelDTO)
        {
            try
            {
                Property prop = await _db.Property.FirstOrDefaultAsync(x => x.Id == Id);
                if (prop == null)
                {
                    return NotFound();
                }

                prop.PropertyType = modelDTO.PropertyType;
                prop.Description = modelDTO.Description;
                prop.Address = modelDTO.Address;
                prop.UpdatedUser = modelDTO.UpdatedUser;
                prop.UpdatedDate = DateTime.Now;
                _db.Entry(prop).State = EntityState.Modified;

                byte type = 1;
                var filesToRemove = _db.Files.Where(x => x.OriginID == Id && x.Type == type).ToList();
                if (filesToRemove.Count() > 0)
                {
                    _db.Files.RemoveRange(filesToRemove);
                }
                modelDTO.Files.ForEach(file =>
                {
                    file.OriginID = Id;
                    file.Type = type; //[Type 1 indicates property] // checkout how to bring files
                    file.CreatedUser = modelDTO.CreatedUser;
                    file.CreatedDate = modelDTO.CreatedDate;
                    file.UpdatedUser = modelDTO.UpdatedUser;
                    file.UpdatedDate = modelDTO.UpdatedDate;
                });

                var files = modelDTO.Files;
                _db.Files.AddRange(files);
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
