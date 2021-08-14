using System;
using System.Collections.Generic;
using System.Linq;
using PropertyManager.Interfaces;
using PropertyManager.Models;

namespace PropertyManager.Data
{
    public class SqlServerDatabaseRepo : ICommanderRepo
    {
        private readonly DBContext _context;

        public SqlServerDatabaseRepo(DBContext context)
        {
            _context = context;
        }

        public IEnumerable<Command> GetAppCommands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int Id)
        {
            return _context.Commands.FirstOrDefault(x => x.Id == Id);
        }
    }
}
