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
            "Make",
            "Model",
            "Shape",
            "Length",
            "Notes"

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

            //// TODO: use as source for below section of console output content
            ////ConsoleHelper.OutputLine("Published On: {0}", comicBook.PublishedOn.ToShortDateString());
            ////ConsoleHelper.OutputLine("Average Rating: {0}",
            ////    comicBook.AverageRating != null ?
            ////    comicBook.AverageRating.Value.ToString("n2") : "N/A");

            //// TODO: Add boardStyle.name
            ConsoleHelper.OutputLine(surfboard.Style.Name);
            //// TODO: Add rating
            if (!string.IsNullOrWhiteSpace(surfboard.Notes))
            {
                ConsoleHelper.OutputLine("Summary: ");
                ConsoleHelper.OutputLine(surfboard.Notes);
            }

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
            //// Get values input from user.
            var surfBoard = new Surfboard();
            // TODO: create duplicate shaper verification <HERE>

            //surfBoard.Id = GetBoardId();
            surfBoard.Builder = GetBuilder();
            surfBoard.Model = GetModel();
            surfBoard.Length = GetBoardLength();
            surfBoard.Style = GetBoardStyle();
            BoardStyle shapeInput = GetBoardStyle();

            //// TODO: move duplicate boardshape verification <HERE>
            //// TODO: refactor this into its own interface (and recycle with Shaper validations)
            //bool newShape = true;
            //foreach (BoardStyle s in shapes)
            //{
            //    if (s.Name.ToLower() == shapeInput.ToLower())
            //    {
            //        newShape = false;
            //        surfBoard.Style = s;
            //    }
            //}
            //if (newShape)
            //{
            //    styleInput.Name = style;
            //    styleInput.Id = shapes.Count() + 1;
            //    Repository.AddBoardStyle(shapeInput);
            //}

            // TODO: add a GetBoardDescription() method; 

            // Add the surfboard to the database.
            Repository.AddSurfboard(surfBoard);
        }

        private static BoardStyle GetBoardStyle()
        {
            ConsoleHelper.OutputBlankLine();
            Console.WriteLine("Enter Board Style:");
            string style = Console.ReadLine();

            //// TODO: create a redundancy check for if boardstyle already exists.
            //// If exists, return matching style. If not, create new one.
            IList<BoardStyle> shapes = Repository.GetBoardStyles();
            BoardStyle styleInput = new BoardStyle();

            return styleInput;
        }


        private static float GetBoardLength()
        {
            ConsoleHelper.OutputBlankLine();
            Console.WriteLine("Enter Board Length:");
            float length = float.Parse(Console.ReadLine());
            return length;
        }

        private static string GetBoardNotes()
        {
            ConsoleHelper.OutputBlankLine();
            Console.Write("Enter Board Description:");
            string notes = Console.ReadLine();
            return notes;
        }

        private static string GetModel()
        {
            ConsoleHelper.OutputBlankLine();
            Console.WriteLine("Enter Model Name:");
            string model = Console.ReadLine();
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

            string name = Console.ReadLine();
            builder.Name = name;
            return builder;
        }

        //private static void GetBoardId()
        //{
        //    int? boardId = null;
        //}

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
            //throw new NotImplementedException();
            {
                var successful = false;

                // Attempt to parse the command to a line number.
                int lineNumber = 0;
                int.TryParse(command, out lineNumber);

                // If the number is within range then get that surfboard ID.
                if (lineNumber > 0 && lineNumber <= EditableProperties.Count)
                {
                    // Retrieve the property name for the provided line number.
                    string propertyName = EditableProperties[lineNumber - 1];

                    // Switch on the provided property name and call the 
                    // associated user input method for that property name.
                    switch (propertyName)
                    {
                        case "Make":
                            // TODO: GetBuilderID is incomplete
                            surfboard.BuilderId = GetBuilderId();
                            surfboard.Builder = Repository.GetBuilder(surfboard.BuilderId);
                            break;
                        case "Model":
                            surfboard.Model = GetModel();
                            break;
                        case "Shape":
                            // TODO: BoardShape/Style needs to replicate Builder methods
                            surfboard.BoardStyleId = GetBoardStyleId();
                            surfboard.Style = Repository.GetBoardStyle(surfboard.BoardStyleId);
                            break;
                        case "Length":
                            surfboard.Length = GetBoardLength();
                            break;
                        case "Notes":
                            surfboard.Notes = GetBoardNotes();
                            break;
                        default:
                            break;
                    }

                    successful = true;
                }

                return successful;
            }
        }

        // TODO: refactor this with GetBuilderId method
        private static int GetBoardStyleId()
        {
            int? styleId = null;
            IList<BoardStyle> shapes = Repository.GetBoardStyles();

            // While StyleId is null, prompt the user to select a shape from provided list.
            while (styleId == null)
            {
                ConsoleHelper.OutputBlankLine();
                foreach (BoardStyle s in shapes)
                {
                    ConsoleHelper.OutputLine("{0}) {1}", shapes.IndexOf(s) + 1, s.Name);
                }

                // Get line number of selected shape.
                string lineNumberInput = ConsoleHelper.ReadInput(
                    "Enter the line number of the board shape you choose: ");

                // Attempt to parse the user input into a line number.
                int lineNumber = 0;
                if (int.TryParse(lineNumberInput, out lineNumber))
                {
                    if (lineNumber > 0 && lineNumber <= shapes.Count)
                    {
                        styleId = shapes[lineNumber - 1].Id;
                    }
                }

                // If we weren't able to parse the line number, then throw error
                if (styleId == null)
                {
                    ConsoleHelper.OutputLine("Sorry, but that wasn't a valid line number.");
                }
            }
            return (int)styleId;
        }

        /// <summary>
        /// Gets the builder ID from the user.
        /// </summary>
        /// <returns>Returns an integer for the selected builder ID.</returns>
        private static int GetBuilderId()
        {
            int? builderId = null;
            IList<Builder> builders = Repository.GetBuilders();

            // While the builder ID is null, prompt the user to select a builder 
            // from the provided list.
            while (builderId == null)
            {
                ConsoleHelper.OutputBlankLine();
                foreach (Builder b in builders)
                {
                    ConsoleHelper.OutputLine("{0}) {1}", builders.IndexOf(b) + 1, b.Name);
                }

                // Get the line number for the selected builder.
                string lineNumberInput = ConsoleHelper.ReadInput(
                    "Enter the line number of the builder that you want to add a surfboard to: ");

                // Attempt to parse the user's input to a line number.
                int lineNumber = 0;
                if (int.TryParse(lineNumberInput, out lineNumber))
                {
                    if (lineNumber > 0 && lineNumber <= builders.Count)
                    {
                        builderId = builders[lineNumber - 1].Id;
                    }
                }

                // If we weren't able to parse the provided line number 
                // to a builder ID then display an error message.
                if (builderId == null)
                {
                    ConsoleHelper.OutputLine("Sorry, but that wasn't a valid line number.");
                }
            }
            return builderId.Value;
        }

        private static void ListSurfboardProperties(Surfboard surfboard)
        {
            //throw new NotImplementedException();
            ConsoleHelper.ClearOutput();
            ConsoleHelper.OutputLine("UPDATE SURFBOARD");

            // NOTE: This list of surfboard property values 
            // needs to match the collection of editable properties 
            // declared at the top of this file.
            ConsoleHelper.OutputBlankLine();
            ConsoleHelper.OutputLine("1) Make: {0}", surfboard.Builder.Name);
            ConsoleHelper.OutputLine("2) Model: {0}", surfboard.Model);
            ConsoleHelper.OutputLine("3) Shape: {0}", surfboard.Style.Name);
            ConsoleHelper.OutputLine("4) Length: {0}", surfboard.Length);
            ConsoleHelper.OutputLine("5) Description: {0}", surfboard.Notes);
        }


        private static bool DeleteSurfBoard(int surfboardId)
        {
            var successful = false;

            // Prompt the user if they want to continue with deleting this surfboard.
            string input = ConsoleHelper.ReadInput(
                "Are you sure you want to delete this surfboard(Y/N)? ", true);

            // If the user entered "y", then delete the surfboard.
            if (input == "y")
            {
                Repository.DeleteSurfboard(surfboardId);
                successful = true;
            }

            return successful;

        }
    }
}
