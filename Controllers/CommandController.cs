using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PropertyManager.Data;
using PropertyManager.DTOs;
using PropertyManager.Interfaces;
using PropertyManager.Models;

namespace PropertyManager.Controllers
{
    // api/command - another option would be [Route("api/command")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        public readonly IMapper _mapper;

        public CommandController(ICommanderRepo db, IMapper mapper)
        {
            _repository = db;
            _mapper = mapper;
        }

        // GET api/command/
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commands = _repository.GetAppCommands();
            return Ok(commands);
        }

        // GET api/command/{id}
        [HttpGet("{Id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDTO> GetCommandById(int Id)
        {
            var command = _repository.GetCommandById(Id);
            if (command == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CommandReadDTO>(command));
        }

        // POST api/command
        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommand(CommandCreateDTO command)
        {
            var model = _mapper.Map<Command>(command);
            _repository.CreateCommand(model);
            _repository.SaveChanges();
            var commandDto = _mapper.Map<CommandReadDTO>(model);
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandDto.Id }, commandDto);
        }
    }
}
