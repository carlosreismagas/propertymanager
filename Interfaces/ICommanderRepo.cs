using System;
using System.Collections.Generic;
using PropertyManager.Models;

namespace PropertyManager.Interfaces
{
    public interface ICommanderRepo
    {
        bool SaveChanges();
        IEnumerable<Command> GetAppCommands();
        Command GetCommandById(int Id);
        void CreateCommand(Command command);
        void UpdatedCommand(Command command);
        void DeleteCommand(Command command);

    }
}
