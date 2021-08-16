using System;
using System.Collections.Generic;
using PropertyManager.Interfaces;
using PropertyManager.Models;

namespace PropertyManager.Data
{
    // A mocker just creates a class repository where you can quickly set hardcoded values for testing
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAppCommands()
        {
            return new List<Command>() {
                new Command
                {
                    Id = 1,
                    HowTo = "Boil an Egg",
                    Line = "Boil Water with Egg in it",
                    Platform = "Kettle & Pan"
                },
                new Command
                {
                    Id = 2,
                    HowTo = "Boil an Egg",
                    Line = "Boil Water with Egg in it",
                    Platform = "Kettle & Pan"
                },
                new Command
                {
                    Id = 3,
                    HowTo = "Boil an Egg",
                    Line = "Boil Water with Egg in it",
                    Platform = "Kettle & Pan"
                }
            };
        }

        public Command GetCommandById(int Id)
        {
            return new Command
            {
                Id = 1,
                HowTo = "Boil an Egg",
                Line = "Boil Water with Egg in it",
                Platform = "Kettle & Pan"
            };
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdatedCommand(Command command)
        {
            throw new NotImplementedException();
        }
    }
}
