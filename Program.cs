using System;
using System.IO;
using System.Linq;
using TurtleCommand.Services;

namespace TTurtleCommand
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the turtle command service.");
            Console.WriteLine("Please enter F for file input or C for console input");

            TurtleTable table = new TurtleTable(new TurtleService());

            var inputFormat = Console.ReadLine();
            if (inputFormat.ToLower() == "f")
            {
                ReadFromFile(table);
            }
            else if (inputFormat.ToLower() == "c")
            {
                ReadFromConsole(table);
            }
            else
            {
                Console.WriteLine($"Invalid {inputFormat} detect, please enter only F or C");
            }

            Console.WriteLine("--- please enter exit to close the application ---");
        }

        private static void ReadFromConsole(TurtleTable table)
        {
            Console.WriteLine("--- Input as console ---");
            while (true)
            {
                // Get user command from console
                var command = Console.ReadLine();

                // if need to quite the app
                if (command.ToLower() == "exit")
                {
                    Environment.Exit(0);
                }

                // Try process the command
                var output = table.ExecuteCommand(command);
                Console.WriteLine(string.Empty);
                if (!string.IsNullOrEmpty(output))
                {
                    Console.WriteLine("--- Output ---");
                    Console.WriteLine(output);
                }

            }
        }


        private static void ReadFromFile(TurtleTable table)
        {
            Console.WriteLine("--- Input as file ---");
            Console.WriteLine("--- Please enter the file path ---");

            var filePath = Console.ReadLine();
            if (File.Exists(filePath))
            {
                var commands = File.ReadAllLines(filePath);
                var validCommands = commands.Where(d => !string.IsNullOrWhiteSpace(d)).ToList();

                validCommands.ForEach(d =>
                {
                    // if need to quite the app
                    if (d == "exit")
                    {
                        Environment.Exit(0);
                    }

                    // Try process the command
                    var output = table.ExecuteCommand(d);
                    if (!string.IsNullOrEmpty(output))
                    {
                        Console.WriteLine("--- Output ---");
                        Console.WriteLine(output);
                    }
                });

                Console.WriteLine("enter exit to close the app.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Cannot find the file in the below path: {filePath}");
            }
        }


    }
}
