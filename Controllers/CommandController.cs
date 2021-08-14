using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PropertyManager.Data;
using PropertyManager.Interfaces;
using PropertyManager.Models;

namespace PropertyManager.Controllers
{
    // api/command - another option would be [Route("api/command")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly DBContext _repository;

        public CommandController(DBContext db)
        {
            _repository = db;
        }

        // GET api/command/GetAllCommands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commands = _repository.Commands.ToList();
            return Ok(commands);
        }

        // GET api/command/GetCommandById/2
        [HttpGet("{Id}")]
        public ActionResult<Command> GetCommandById(int Id)
        {
            var command = _repository.Commands.FirstOrDefault(x => x.Id == Id);
            return Ok(command);
        }
    }
}
