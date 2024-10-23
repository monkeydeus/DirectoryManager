namespace App;

internal static class Program
{
    static void Main()
    {
        var rootNode = new DirectoryNode("root");
        var currentAction = string.Empty;
        
        DefaultPrompt(string.Empty);
        currentAction = Console.ReadLine();

        while (currentAction?.ToUpper() != "QUIT")
        {
            var splitArgs = currentAction?.Split(" ");
            
            var commandName = (splitArgs ?? Array.Empty<string>()).First().ToUpper();

            string[] commandArgs = [];

            if (splitArgs?.Length > 1)
                commandArgs = splitArgs.Skip(1).ToArray();

            switch (commandName)
            {
                case "CREATE":
                    if (commandArgs.Length == 1)
                        rootNode.CreateChildNodeByNodePath(commandArgs[0]);
                    else
                        DefaultPrompt("PLease enter only one argument for the directory name or path for the Create command");
                    break;
                case "LIST":
                    rootNode.Children.ForEach(c => List(c));
                    break;
                case "MOVE":
                    if (commandArgs.Length == 2)
                        rootNode.Move(commandArgs[0], commandArgs[1]);
                    else
                        DefaultPrompt("Please enter exactly one source directory and one destination directory for the Move command");
                    break;
                case "DELETE":
                    if (commandArgs.Length == 1)
                        rootNode.Delete(commandArgs[0]);
                    else
                        DefaultPrompt("Please enter exactly one argument for the directory name or path for the Delete command");
                    break;
                default:
                    DefaultPrompt("You selected an unknown action");
                    break;
            }

            DefaultPrompt();
            currentAction = Console.ReadLine();
        }

        Console.WriteLine("Exiting..thank you");
    }

    static void List(DirectoryNode node, string prefix = "")
    {
        Console.WriteLine($"{prefix}{node.Name}");
        node.Children.ForEach(c => List(c, string.Concat("  ", prefix)));
    }

    static void DefaultPrompt(string message = "")
    {
        if (!string.IsNullOrEmpty(message))
            Console.WriteLine($"{message}.");
        else
        {
            var nextMessage =
                "What would you like to do next? Please select from 'CREATE', 'LIST', 'MOVE', 'DELETE'.  Enter 'quit' to exit.";
            Console.WriteLine(nextMessage);
        }
    }
}