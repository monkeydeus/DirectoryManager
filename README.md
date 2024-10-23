# Summary
This .NET console app simulates creating a dynamic directory structure.  Users can create, move, delete, and list the 
'contents' of the directory tree as follows:

CREATE: Accepts a string argument.  If the string is delimited by '/', the program
will create each node in the path as required to create the full chain

MOVE: Accepts two strings corresponding to the source path of the node to move, and the destination
path to move it to.  If the source path does not describe an existing node, the routine will exit. Otherwise,
the routine will create the specified node hierarchy and move the LAST node from the source node path to the specified
destination.  

DELETE: Accepts a string argument.  If the string is delimited by '/', the routine will ONLY delete
the last node of the node path, if it is found.  Otherwise, if the string is just one node name, the routine will attempt
to delete the node having that name from the root level.

LIST: Outputs the entire directory hierarchy to the console.

# Requirements
- [.NET SDK for your environment](https://dotnet.microsoft.com/en-us/download)

# Usage
- Navigate to the ~/App directory of the downloaded project
- enter `dotnet run` at the command line to build and run the console app.
- Use CREATE, MOVE, LIST, DELETE commands as desired.