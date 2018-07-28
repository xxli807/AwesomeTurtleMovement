using TurtleMovement.Models;

namespace TurtleMovement.Services
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