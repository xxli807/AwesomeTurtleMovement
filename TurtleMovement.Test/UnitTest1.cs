using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TurtleMovement.Constants;
using TurtleMovement.Enums;
using TurtleMovement.Services;

namespace TurtleMovement.Test
{
    [TestClass]
    public class UnitTest1
    {
        private readonly TurtleTable table = new TurtleTable(new TurtleService());


        [TestMethod]
        public void Turtle_InValid_Commands()
        {
            var displayMessage = table.ExecuteCommand(Command.Move.ToString());
            Assert.AreEqual(MessageConstants.InitialValueError, displayMessage);

            displayMessage = table.ExecuteCommand(Command.Left.ToString());
            Assert.AreEqual(MessageConstants.InitialValueError, displayMessage);
        }


        [TestMethod]
        public void Turtle_Place_Test1()
        {
            table.ExecuteCommand("PLACE 0,0,NORTH");
            table.ExecuteCommand("MOVE");
            var result = table.ExecuteCommand("REPORT");

            Assert.AreEqual("0,1,NORTH", result);
        }


        [TestMethod]
        public void Turtle_Place_Test2()
        {
            table.ExecuteCommand("PLACE 0,0,NORTH");
            table.ExecuteCommand("LEFT");
            var result = table.ExecuteCommand("REPORT");

            Assert.AreEqual("0,0,WEST", result);
        }


        [TestMethod]
        public void Turtle_Place_Test3()
        {
            table.ExecuteCommand("PLACE 1,2,EAST");
            table.ExecuteCommand("MOVE");
            table.ExecuteCommand("MOVE");
            table.ExecuteCommand("LEFT");
            table.ExecuteCommand("MOVE");
            var result = table.ExecuteCommand("REPORT");

            Assert.AreEqual("3,3,NORTH", result);
        }

    }
}
