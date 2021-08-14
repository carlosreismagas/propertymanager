using System;
using System.Collections.Generic;
using PropertyManager.Models;

namespace PropertyManager.Interfaces
{
    public interface ICommanderRepo
    {
        IEnumerable<Command> GetAppCommands();
        Command GetCommandById(int Id);
    }
}
