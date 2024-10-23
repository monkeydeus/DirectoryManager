namespace App;

public class DirectoryNode(string nodeName, List<DirectoryNode>? children = null)
{
    private readonly List<DirectoryNode> _children = children ?? [];

    public List<DirectoryNode> Children
    {
        get { return _children.Count > 0 ? _children.OrderBy(c => c.Name).ToList() : []; }
    }

    public string Name { get; } = nodeName;
    public DirectoryNode? Parent { get; private set; }

    public DirectoryNode? CreateChildNode(DirectoryNode newChildNode, DirectoryNode? parent = null)
    {
        try
        {
            if (Children.Any(c => c.Name == newChildNode.Name)) return null;
            
                newChildNode.Parent = parent ?? this;
                _children.Add(newChildNode);
                return newChildNode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public DirectoryNode? CreateChildNodeByNodePath(string directoryPath)
    {
        var currentNode = this;
        var segments = directoryPath.Split('/');

        foreach (var segment in segments)
        {
            var childFound = currentNode?.Children.FirstOrDefault(c => c.Name == segment);
            
            if (childFound == null)
            {
                try
                {
                    currentNode = currentNode?.CreateChildNode(new DirectoryNode(segment), currentNode);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
                currentNode = childFound;
        }

        return currentNode;
    }

    public DirectoryNode? GetNodeIfExists(string nodeName)
    {
        var currentNode = this;
        DirectoryNode? foundChildNode = null;
        var nodePathSegments = nodeName.Split('/');

        foreach (var segment in nodePathSegments)
        {
            foundChildNode = currentNode.Children.FirstOrDefault(c => c.Name == segment);

            if (foundChildNode == null)
            {
                return null;
            }
            currentNode = foundChildNode;
        }

        return foundChildNode;
    }

    public string Move(string source, string destination)
    {
        try
        {
            var sourceNode = GetNodeIfExists(source);
            if (sourceNode == null)
                return "Cannot move, source does not exist.";

            var destinationNode = CreateChildNodeByNodePath(destination);
            sourceNode.Parent?.Delete(source.Split("/").Last());
            destinationNode?.CreateChildNode(sourceNode, destinationNode);

            return "Node moved successfully";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public string Delete(string nodeName)
    {
        try
        {
            var split = nodeName.Split('/');
            
            if (split.Length == 1)
            {
                if (_children.Any(c => c.Name == split[0]))
                {
                    _children.RemoveAll(c => c.Name == split[0]);
                    return "The node was deleted";
                }
                else
                {
                    return "The node was not found";
                }
            }
            
            var sourceNode = GetNodeIfExists(nodeName);
            if (sourceNode == null)
                return "The node was not found.";

            sourceNode.Parent?.Delete(sourceNode.Name);
            {
                return "The node was deleted";
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public override string ToString()
    {
        return Name;
    }
}