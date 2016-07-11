using System.Linq;
using ABus.DataStructures;
using Xunit;
using FluentAssertions;

namespace ABus.Test
{
    public class NodeListTest
    {
        [Fact]

        public void When_calling_AddFirst_with_empty_list_adds_node()
        {
            var nodeList = new NodeList<string>();
            nodeList.AddFirst("Hello");

            nodeList.Head.Data.Should().Be("Hello");
            nodeList.Tail.Data.Should().Be("Hello");
        }

        [Fact]

        public void When_calling_AddFirst_with_non_empty_list_adds_node_to_head()
        {
            var nodeList = new NodeList<string>();
            nodeList.AddFirst("Hello");
            nodeList.AddFirst("Hello2");
            nodeList.AddFirst("Hello3");

            nodeList.Head.Data.Should().Be("Hello3");
            nodeList.Tail.Data.Should().Be("Hello");
            ValidateOrdering(new[] { "Hello3", "Hello2", "Hello" }, nodeList);
        }

        [Fact]

        public void When_calling_AddLast_with_empty_list_adds_node()
        {
            var nodeList = new NodeList<string>();
            nodeList.AddLast("Hello");

            nodeList.Head.Data.Should().Be("Hello");
            nodeList.Tail.Data.Should().Be("Hello");
            ValidateOrdering(new[] { "Hello" }, nodeList);
        }

        [Fact]

        public void When_calling_AddLast_with_non_empty_list_adds_node_to_tail()
        {
            var nodeList = new NodeList<string>();
            nodeList.AddFirst("Hello");
            nodeList.AddLast("Hello2");
            nodeList.AddLast("Hello3");

            Assert.Equal("Hello", nodeList.Head.Data);
            Assert.Equal("Hello3", nodeList.Tail.Data);
            ValidateOrdering(new[] { "Hello", "Hello2", "Hello3" }, nodeList);
        }
        [Fact]
        public void When_calling_AddBefore_with_non_empty_list_adds_new_node_before_specified_node()
        {
            var nodeList = new NodeList<string>();
            nodeList.AddFirst("Hello");
            var targetNode = nodeList.AddLast("Hello2");
            nodeList.AddLast("Hello3");

            // Act
            nodeList.AddBefore(targetNode, "Hello4");

            // Assert
            ValidateOrdering(new[] { "Hello", "Hello4", "Hello2", "Hello3" }, nodeList);
        }
        [Fact]
        public void When_calling_AddAfter_with_non_empty_list_adds_new_node_after_specified_node()
        {
            var nodeList = new NodeList<string>();
            nodeList.AddFirst("Hello");
            var targetNode = nodeList.AddLast("Hello2");
            nodeList.AddLast("Hello3");

            // Act
            nodeList.AddAfter(targetNode, "Hello4");

            // Assert
            ValidateOrdering(new[] { "Hello", "Hello2", "Hello4", "Hello3" }, nodeList);
        }

        public static void ValidateOrdering(string[] array, NodeList<string> nodeList)
        {
            array.Should().BeSameAs(nodeList.ToArray());
            array.Reverse().Should().BeSameAs(nodeList.Reverse().ToArray());
        }
    }
}
