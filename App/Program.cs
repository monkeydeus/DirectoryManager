using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App;

internal class Program
{
    static void Main()
    {
        using var serviceProvider = LoggingConfig.ConfigureLogging();
        
        ILogger<Program> logger;
        try
        {
            logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while getting the logger service: {ex.Message}");
            return;
        }

        DirectoryNode rootNode;
        try
        {
            var dirLogger = serviceProvider.GetRequiredService<ILogger<DirectoryNode>>();
            rootNode = new DirectoryNode(dirLogger, "root");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        var currentAction = string.Empty;
        
        //MessagePrompt(string.Empty);
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
                    {
                        logger.LogInformation("Creating directory");
                        rootNode.CreateChildNodeByNodePath(commandArgs[0]);
                        Console.WriteLine($"CREATE {commandArgs[0]}");
                    }
                    else
                        MessagePrompt("Please enter only one argument for the directory name or path for the Create command");
                    break;
                case "LIST":
                {
                    logger.LogInformation("Listing directories");
                    Console.WriteLine("LIST:");
                    rootNode.Children.ForEach(c => List(c));
                    Console.WriteLine();
                    break;   
                }
                case "MOVE":
                    if (commandArgs.Length == 2)
                    {
                        logger.LogInformation("Moving directory");
                        rootNode.Move(commandArgs[0], commandArgs[1]);
                        Console.WriteLine($"MOVE {commandArgs[0]} {commandArgs[1]}");
                    }
                    else
                        MessagePrompt("Please enter exactly one source directory and one destination directory for the Move command");
                    break;
                case "DELETE":
                    if (commandArgs.Length == 1)
                    {
                        logger.LogInformation("Deleting directory");
                        rootNode.Delete(commandArgs[0]);
                        Console.WriteLine($"DELETE {commandArgs[0]}");
                    }
                    else
                        MessagePrompt("Please enter exactly one argument for the directory name or path for the Delete command");
                    break;
                default:
                    logger.LogInformation("Unknown command");
                    MessagePrompt("You selected an unknown action");
                    break;
            }

            currentAction = Console.ReadLine();
        }

        Console.WriteLine("Exiting..thank you");
    }

    static void List(DirectoryNode node, string prefix = "")
    {
        Console.WriteLine($"{prefix}{node.Name}");
        node.Children.ForEach(c => List(c, string.Concat("  ", prefix)));
    }

    static void MessagePrompt(string message = "")
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