using TurtleCommand.Models;

namespace TurtleCommand.Services
{
    public interface ITurtleService
    {
         
        bool Place(string[] PlaceCommands);

        bool Move();

        bool Left();

        bool Right();

        TurtleState ReportCurrentState();
        
    }
}