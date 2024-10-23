using App;

namespace Tests;

public class DirectoryNodeTests
{
    [Fact]
    public void New_DocumentNode_should_be_created_with_GUID_ID()
    {
        //Arrange
        var rootNode = new DirectoryNode("root");

        //Act
        var guidId = rootNode.Id;

        //Assert
        Assert.True(Guid.TryParse(rootNode.Id.ToString(), out _));
    }

    [Fact]
    public void Create_should_add_child_node_to_current_node()
    {
        //Arrange
        var rootNode = new DirectoryNode("root");
        
        //Act
        rootNode.CreateChildNode(new DirectoryNode("Child1"), rootNode);

        //Assert
        Assert.True(rootNode.Children.Count == 1);
    }

    [Fact]
    public void New_DocumentNode_should_be_created_with_the_provided_name()
    {
        //Arrange
        var rootNode = new DirectoryNode("root");
        
        //Act
        
        
        //Assert
        Assert.True(rootNode.Name == "root");
    }

    [Fact]
    public void DocumentNode_should_only_have_one_child_of_given_name()
    {
        //Arrange
        var rootNode = new DirectoryNode("root");
        
        //Act
        rootNode.CreateChildNode(new DirectoryNode("Child1"), rootNode);
        rootNode.CreateChildNode(new DirectoryNode("Child1"),rootNode);
        
        var childCount = rootNode.Children.Count(c => c.Name == "Child1");
        
        //Assert
        Assert.True(childCount == 1);
    }

    [Fact]
    public void New_DocumentNode_should_be_created_with_empty_children_list()
    {
        //Arrange
        var rootNode = new DirectoryNode("root");
        
        //Act
        
        //Assert
        Assert.True(rootNode.Children != null);
    }

    [Fact]
    public void Create_should_create_new_child_node_from_name_and_return_created_child_node()
    {
        //Arrange
        var rootNode = new DirectoryNode("root");
        
        //Act
        var childNode = rootNode.CreateChildNodeByNodePath("Child1");
        
        //Assert
        Assert.True(childNode is DirectoryNode);
        Assert.True(childNode.Name == "Child1");
    }

    [Fact]
    public void New_Child_DocumentNode_should_have_parent_node_reference()
    {
        //Arrange
        var rootNode = new DirectoryNode("root");
        
        //Act
        var childNode = rootNode.CreateChildNode(new DirectoryNode("Child1"));
        
        //Assert
        Assert.True(childNode.Parent == rootNode);
    }

    [Fact]
    public void Delete_should_delete_child_node_from_parent()
    {
        //Arrange
        var rootNode = new DirectoryNode("root");
        var childNode = rootNode.CreateChildNode(new DirectoryNode("Child1"));
        
        //Act
        rootNode.Delete("Child1");
        
        //Assert
        Assert.DoesNotContain(childNode, rootNode.Children);
        Assert.DoesNotContain(rootNode.Children, c => c.Name == "Child1");
    }

    [Fact]
    public void Delete_should_return_success_message_when_child_node_is_deleted()
    {
        //Arrange
        var rootNode = new DirectoryNode("root");
        var childNode = rootNode.CreateChildNode(new DirectoryNode("Child1"));
        
        //Act
        var message = rootNode.Delete("Child1");
        
        //Assert
        Assert.Equal("The node was deleted", message);
    }

    [Fact]
    public void Delete_should_return_failure_message_when_child_node_is_not_found()
    {
        //Arrange
        var rootNode = new DirectoryNode("root");
        var childNode = new DirectoryNode("Child1");
        
        //Act
        var message = rootNode.Delete("Child1");
        //Assert
        Assert.Equal("The node was not found", message);
    }

    [Fact]
    public void Create_should_create_node_hierarchy()
    {
        //Arrange
        var rootNode = new DirectoryNode("root");
        
        //Act
        rootNode.CreateChildNodeByNodePath("fruit/apples/fuji");
        
        
        //Assert
        Assert.Contains(rootNode.Children, c => c.Name == "fruit");
        
        var fruit = rootNode.Children.First(c => c.Name == "fruit");
        
        Assert.Contains(fruit.Children, c => c.Name == "apples");
        
        var apples = fruit.Children.First(c => c.Name == "apples");
        
        Assert.Contains(apples.Children, c => c.Name == "fuji");
    }

    [Fact]
    public void Move_should_move_node_from_source_to_destination()
    {
        //Arrange
        var rootNode = new DirectoryNode("root");
        var children = rootNode.CreateChildNodeByNodePath("fruit/apples/fuji");
        
        //Act
        rootNode.Move("fruit/apples/fuji", "foods/fruits");
        
        //Assert
        Assert.True(rootNode.GetNodeIfExists("foods/fruits")?.Children.Count(c => c.Name == "fuji") == 1);
    }
    
    [Fact]
    public void Move_should_remove_node_from_source()
    {
        //Arrange
        var rootNode = new DirectoryNode("root");
        var children = rootNode.CreateChildNodeByNodePath("fruit/apples/fuji");
        
        //Act
        rootNode.Move("fruit/apples/fuji", "foods/fruits");
        
        //Assert
        Assert.True(rootNode.GetNodeIfExists("fruit/apples")?.Children.Count(c => c.Name == "fuji") == 0);
    }
}