using System;
using System.Linq;
using TurtleCommand.Enums;
using TurtleCommand.Models;

namespace TurtleCommand.Services
{
    public class TurtleService : ITurtleService
    {

        private readonly int XPos = 5;

        private readonly int YPos = 5;

        private TurtleState State;


        public bool Place(string[] PlaceCommands)
        {

            if (PlaceCommands == null)
            {
                return false;
            }

            if (Enum.TryParse<Command>(PlaceCommands[0], true, out var command))
            {
                string[] place_args = PlaceCommands[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                if (place_args.Length != 3)
                {
                    return false;
                }

                if (command != Command.Place)
                {
                    return false;
                }

                if (!int.TryParse(place_args[0], out var x) || !int.TryParse(place_args[1], out var y))
                {
                    return false;
                }

                if (x < 0 || x >= XPos || y < 0 || y >= YPos)
                {
                    return false;
                }
                
                if (!Enum.TryParse<Facing>(place_args[2], true, out var direction))
                {
                    return false;
                }

                var isStillInTable = Place(new TurtleState()
                {
                    XPos = x,
                    YPos = y,
                    Facing = direction
                });

                if (isStillInTable)
                {
                    return true;
                }
            }
            
            return false;         
        }


        private bool Place(TurtleState currentState)
        {
            if (IsInsideRange(currentState.XPos, currentState.YPos))
            {
                this.State = currentState;
                return true;
            }

            return false;
        }


        public bool Left()
        {
            if(!IsStateExist())
            {
                return false;
            }

            TurnDirection(Turn.Left);
            return true;
        }

        public bool Move()
        {
            if (!IsStateExist())
            {
                return false;
            }

            State.XPos = GetNewXPos();
            State.YPos = GetNewYPos();

            return true;
        }

        public bool Right()
        {
            if (!IsStateExist())
            {
                return false;
            }

            TurnDirection(Turn.Right);

            return true;
        }

        public TurtleState ReportCurrentState()
        {
            return State;
        }
            

        private bool IsInsideRange(int x, int y)
        {

            if (x < 0 || y < 0 || x > XPos || y > YPos)
            {
                return false;
            }

            return true;

        }


        // the key part is if turtle facing East meaning it
        // moves +1 on x, face north meaning +1 on y

        private int GetNewXPos()
        {

            if (State.Facing == Facing.East)
            {
                return State.XPos + 1;
            }

            if (State.Facing == Facing.West)
            {
                return State.XPos - 1;
            }

            return State.XPos;

        }
        
        private int GetNewYPos()
        {
            if (State.Facing == Facing.North)
            {
                return State.YPos + 1;
            }

            if (State.Facing == Facing.South)
            {
                return State.YPos - 1;
            }

            return State.YPos;

        }

        // this is used to decide if it is a right/left turn
        // and the turn 90 degree should be like clock direction
        private void TurnDirection(Turn turn)
        {
            var currentFacing = (int)State.Facing;

            // if right turn then enum + 1 as it always north->east->north->west
            if (turn == Turn.Right)
            {
                currentFacing = currentFacing + 1;
            }
            else
            {
                currentFacing = currentFacing - 1;
            }

            // if 5 then north and 0 meaning west
            if (currentFacing == 5)
            {
                State.Facing = Facing.North;
            }
            else if (currentFacing == 0)
            {
                State.Facing = Facing.West;
            }
            else
            {
                State.Facing = (Facing)currentFacing;
            }

        }


        private bool IsStateExist()
        {
            return this.State != null;
        }
    }
}