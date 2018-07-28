using System;
using System.Linq;
using TurtleMovement.Constants;
using TurtleMovement.Enums;
using TurtleMovement.Services;

namespace TurtleMovement
{
    public class TurtleTable
    {
        private readonly ITurtleService _turtleService;

        public TurtleTable(ITurtleService turtleService)
        {
            _turtleService = turtleService;
        }

        public string ExecuteCommand(string command)
        {
            // split the command
            var userInputs = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            //validate the command and arguments
            var displayMessage = string.Empty;

            //cannot be over 2
            if (userInputs.Length > 2)
            {
                return MessageConstants.InvalidInputs;
            }

            Command commandInput;

            // this should be command
            if (userInputs.Length == 1)
            {
                if (Enum.TryParse(userInputs[0], true, out commandInput))
                {
                    ExecutiveMovement(commandInput, null, ref displayMessage);
                }
                else
                {
                    return MessageConstants.InvalidCommand;
                }
            }

            if (userInputs.Length == 2)
            {
                ExecutiveMovement(Command.Place, userInputs, ref displayMessage);
            }

             
            return displayMessage;
              
        }

        private void ExecutiveMovement(Command command, string[] userInputs, ref string message)
        {
            //FIX ME: should use the strategy pattern, update it later
            switch (command)
            {
                case Command.Left:
                    if (!_turtleService.Left())
                    {
                        message = MessageConstants.InitialValueError;
                    }
                    break;
                case Command.Right:
                    if (!_turtleService.Right())
                    {
                        message = MessageConstants.InitialValueError;
                    }
                    break;
                case Command.Move:
                    if (!_turtleService.Move())
                    {
                        message = MessageConstants.InitialValueError;
                    }
                    break;
                case Command.Place:
                    if (!_turtleService.Place(userInputs))
                    {
                        message = MessageConstants.PlaceOutOfRange;
                    }
                    break;
                case Command.Report:
                    var report = _turtleService.ReportCurrentState();
                    if (report == null)
                    {
                        message = MessageConstants.InitialValueError;
                    }
                    else
                    {
                        message = $"{report.XPos},{report.YPos},{report.Facing.ToString().ToUpper()}";
                    }
                    break;
                default:
                    throw new Exception(MessageConstants.InvalidCommand);
            }

        }

    }
}