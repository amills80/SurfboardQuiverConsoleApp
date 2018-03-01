using SurfboardQuiverConsoleApp.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SurfboardQuiverConsoleApp.Helpers;

namespace SurfboardQuiverConsoleApp
{

    class Program
    {
        // console character commands to navigate through the application
        const string CommandListSurfBoards = "l";
        const string CommandListSurfBoard = "i";
        const string CommandListSurfProperties = "p";
        const string CommandAddSurfBoard = "a";
        const string CommandUpdateSurfBoard = "u";
        const string CommandDeleteSurfBoard = "d";
        const string CommandSave = "s";
        const string CommandCancel = "c";
        const string CommandQuit = "q";

        // Properties that can become readOnly, should be identified, here:
        readonly static List<string> EditableProperties = new List<string>()
        {

        };

        static void Main(string[] args)
        {
            // initialize menu / nav commands
            string command = CommandListSurfBoards;
            IList<int> surfboardIds = null;

            //handle menu/nav-IO here
            while (command != CommandQuit)
            {
                switch (command)
                {
                    case CommandListSurfBoards:
                        surfboardIds = ListSurfboards();
                        break;
                    case CommandAddSurfBoard:
                        AddSurfboard();
                        command = CommandListSurfBoards;
                        continue;
                    default:
                        if (AttemptDisplaySurfboard(command, surfboardIds))
                        {
                            command = CommandListSurfBoards;
                            continue;
                        }
                        else
                        {
                            ConsoleHelper.OutputLine("Sorry, but I did not understand");
                        }
                        break;
                }
                // List the available commands.
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");
                int surfboardCount = Repository.GetBoardCount();
                if (surfboardCount > 0)
                {
                    ConsoleHelper.Output("Enter a Number 1-{0}, ", surfboardCount);
                }
                ConsoleHelper.OutputLine("A - Add, Q - Quit", false);

                // Get the next command from the user.
                command = ConsoleHelper.ReadInput("Enter a Command: ", true);

            }
        }

        private static IList<int> ListSurfboards()
        {
            var surfboardIds = new List<int>();
            IList<Surfboard> surfboards = Repository.GetBoards();

            ConsoleHelper.ClearOutput();
            ConsoleHelper.OutputLine("SURFBOARDS");

            ConsoleHelper.OutputBlankLine();

            foreach (Surfboard surfboard in surfboards)
            {
                surfboardIds.Add(surfboard.Id);

                ConsoleHelper.OutputLine("{0}) {1}"
                    , surfboards.IndexOf(surfboard) + 1
                    , surfboard.DisplayText);
            }

            return surfboardIds;
            //throw new NotImplementedException();
        }

        private static bool AttemptDisplaySurfboard(string command, IList<int> surfboardIds)
        {
            var successful = false;
            int? surfboardId = null;

            // Only attempt to parse the command to a line number 
            // if we have a collection of board IDs available.
            if (surfboardIds != null)
            {
                // Attempt to parse the command to a line number.
                int lineNumber = 0;
                int.TryParse(command, out lineNumber);

                // If the number is within range then get that surfboard ID.
                if (lineNumber > 0 && lineNumber <= surfboardIds.Count)
                {
                    surfboardId = surfboardIds[lineNumber - 1];
                    successful = true;
                }
            }

            // If we have a surfboard ID, then display the surfboard.
            if (surfboardId != null)
            {
                DisplaySurfboard(surfboardId.Value);
            }
            return successful;
            //throw new NotImplementedException();
        }

        private static void DisplaySurfboard(int surfboardId)
        {
            string command = CommandListSurfBoard;

            // If the current command doesn't equal the "Cancel" command 
            // then evaluate and process the command.
            while (command != CommandCancel)
            {
                switch (command)
                {
                    case CommandListSurfBoard:
                        ListSurfboard(surfboardId);
                        break;
                    case CommandUpdateSurfBoard:
                        UpdateSurfboard(surfboardId);
                        command = CommandListSurfBoard;
                        continue;
                    case CommandDeleteSurfBoard:
                        if (DeleteSurfBoard(surfboardId))
                        {
                            command = CommandCancel;
                        }
                        else
                        {
                            command = CommandListSurfBoard;
                        }
                        continue;
                    default:
                        ConsoleHelper.OutputLine("Sorry, but I didn't understand that command.");
                        break;
                }

                // List the available commands.
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");
                ConsoleHelper.OutputLine("U - Update, D - Delete, C - Cancel", false);

                // Get the next command from the user.
                command = ConsoleHelper.ReadInput("Enter a Command: ", true);
            }
            //throw new NotImplementedException();
        }
        
        private static void ListSurfboard(int surfboardId)
        {
            Surfboard surfboard = Repository.GetBoard(surfboardId);

            ConsoleHelper.ClearOutput();
            ConsoleHelper.OutputLine("SURFBOARD DETAIL");

            ConsoleHelper.OutputLine(surfboard.DisplayText);

            //// TODO use as source for below section of console output content
            ////ConsoleHelper.OutputLine("Published On: {0}", comicBook.PublishedOn.ToShortDateString());
            ////ConsoleHelper.OutputLine("Average Rating: {0}",
            ////    comicBook.AverageRating != null ?
            ////    comicBook.AverageRating.Value.ToString("n2") : "N/A");

            ////Add boardStyle.name
            //ConsoleHelper.OutputBlankLine();
            ////Add rating
            //if (!string.IsNullOrWhiteSpace(surfboard.Notes))
            //{
            //    ConsoleHelper.OutputLine(surfboard.Notes);
            //}
                        
            ConsoleHelper.OutputLine("");
            //throw new NotImplementedException();
        }

        private static void AddSurfboard()
        {
            ConsoleHelper.ClearOutput();
            ConsoleHelper.OutputLine("ADD SURFBOARD");

            // List out existing quiver
            ConsoleHelper.OutputBlankLine();
            IList<Surfboard> surfBoards = Repository.GetBoards();
            foreach (Surfboard s in surfBoards)
            {
                ConsoleHelper.OutputLine("{0}) {1}", surfBoards.IndexOf(s) + 1, s.DisplayText);
            }
            
            //// TODO: get values input from user.

            var surfBoard = new Surfboard();
            //surfBoard.Id = GetBoardId();
            surfBoard.Builder = GetBuilder();
            surfBoard.Model = GetModel();
            surfBoard.Length = GetBoardLength();
            surfBoard.Style = GetBoardStyle();
            
            // Add the surfboard to the database.
            Repository.AddSurfboard(surfBoard);
        }

        private static BoardStyle GetBoardStyle()
        {
            ConsoleHelper.OutputBlankLine();
            Console.WriteLine("Enter Board Style:");
            string style = Console.Read().ToString();
            BoardStyle styleName = new BoardStyle();
            //// TODO: create a redundancy check for if boardstyle already exists.
            //// If exists, return matching style. If not, create new one.
            //if ()
            styleName.Name = style;
            return styleName;
        }

        private static float GetBoardLength()
        {
            ConsoleHelper.OutputBlankLine();
            Console.WriteLine("Enter Board Length:");
            float length = Console.Read();
            return length;
        }

        private static string GetModel()
        {
            ConsoleHelper.OutputBlankLine();
            Console.WriteLine("Enter Model Name:");
            string model = Console.Read().ToString();
            return model;
        }

        private static Builder GetBuilder()
        {
            ConsoleHelper.OutputBlankLine();
            Console.WriteLine("Enter Builder Name:");
            Builder builder = new Builder();
            //// TODO: create a redundancy check for if boardstyle already exists.
            //// If exists, return matching style. If not, create new one.
            //if ()

            string name = Console.Read().ToString();
            builder.Name = name;
            return builder;
        }

        private static void GetBoardId()
        {
            int? boardId = null;
        }

        private static void UpdateSurfboard(int surfboardId)
        {
            Surfboard surfboard = Repository.GetBoard(surfboardId);

            string command = CommandListSurfProperties;

            // If the current command doesn't equal the "Cancel" command 
            // then evaluate and process the command.
            while (command != CommandCancel)
            {
                switch (command)
                {
                    case CommandListSurfProperties:
                        ListSurfboardProperties(surfboard);
                        break;
                    case CommandSave:
                        Repository.UpdateSurfboard(surfboard);
                        command = CommandCancel;
                        continue;
                    default:
                        if (AttemptUpdateSurfboardProperty(command, surfboard))
                        {
                            command = CommandListSurfProperties;
                            continue;
                        }
                        else
                        {
                            ConsoleHelper.OutputLine("Sorry, but I didn't understand that command.");
                        }
                        break;
                }

                // List the available commands.
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");
                if (EditableProperties.Count > 0)
                {
                    ConsoleHelper.Output("Enter a Number 1-{0}, ", EditableProperties.Count);
                }
                ConsoleHelper.OutputLine("S - Save, C - Cancel", false);

                // Get the next command from the user.
                command = ConsoleHelper.ReadInput("Enter a Command: ", true);
            }
            ConsoleHelper.ClearOutput();
        }

        private static bool AttemptUpdateSurfboardProperty(string command, Surfboard surfboard)
        {
            throw new NotImplementedException();
        }

        private static void ListSurfboardProperties(Surfboard surfboard)
        {
            throw new NotImplementedException();
        }

        //TODO : This is next
        private static bool DeleteSurfBoard(int surfboardId)
        {
            //throw new NotImplementedException();
            var successful = false;

            // Prompt the user if they want to continue with deleting this comic book.
            string input = ConsoleHelper.ReadInput(
                "Are you sure you want to delete this comic book (Y/N)? ", true);

            // If the user entered "y", then delete the comic book.
            if (input == "y")
            {
                Repository.DeleteSurfboard(surfboardId);
                successful = true;
            }

            return successful;

        }
    }
}
