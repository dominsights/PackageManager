using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DgSystems.Scoop
{
    internal class CommandHistory
    {
        private Stack<Command> commands;

        public CommandHistory()
        {
            commands = new Stack<Command>();
        }

        public void Push(Command command)
        {
            commands.Push(command);
        }

        public Command Pop()
        {
            return commands.Pop();
        }

        public bool IsEmpty()
        {
            return !commands.Any();
        }
    }
}
