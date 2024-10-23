# Summary
This .NET console app simulates creating a dynamic directory structure.  Users can create, move, delete, and list the 
'contents' of the directory tree as follows:

CREATE: Accepts a string argument.  If the string is delimited by '/', the program
will create each node in the path as required to create the full chain

`CREATE fruits` Creates a directory node named 'fruits' as a child of the top-level node

`CREATE fruits/citrus/orange` Creates a directory node named fruits, with a childe node named 'citrus', which has a child node named 'orange'

MOVE: Accepts two strings corresponding to the source path of the node to move, and the destination
path to move it to.  If the source path does not describe an existing node, the routine will exit. Otherwise,
the routine will create the specified node hierarchy and move the LAST node from the source node path to the specified
destination.  

`MOVE fruits food` Moves the 'fruits' directory - if it exists - to a child of the 'food' directory, and deletes 'fruits' as a child of the top-level directory.
Creates the 'food' directory if it does not exist.

`MOVE fruits/apple foods/fruit` Move the 'apple' directory - if it exists - to be a child of 'foods/fruit' (creating any of foods or fruit as required)
and deletes 'apple' from the 'fruits' directory

DELETE: Accepts a string argument.  If the string is delimited by '/', the routine will ONLY delete
the last node of the node path, if it is found.  Otherwise, if the string is just one node name, the routine will attempt
to delete the node having that name from the root level.

`DELETE fruits/apple/fuji` Deletes the 'fuji' directory from the children of the 'apple' node.

`DELETE fruits` Deletes 'fruits' - and ALL children - from the top-level node.

LIST: Outputs the entire directory hierarchy to the console.

# Requirements
- [.NET SDK for your environment](https://dotnet.microsoft.com/en-us/download)

# Usage
- Navigate to the ~/App directory of the downloaded project
- enter `dotnet run` at the command line to build and run the console app.
- Use CREATE, MOVE, LIST, DELETE commands as desired.