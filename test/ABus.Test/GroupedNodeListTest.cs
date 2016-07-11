using System.Linq;
using ABus.DataStructures;
using FluentAssertions;
using Xunit;

namespace ABus.Test
{
    public class GroupedNodeListTest
    {
        [Fact]

        public void When_calling_AddLast_with_no_groups_adds_node()
        {
            var nodeList = new GroupedNodeList<string>();
            nodeList.AddFirst("group1","item");

            ValidateOrdering(new[] { "item" }, nodeList);
        }

        [Fact]
        public void When_calling_Join_with_no_groups_adds_node()
        {
            var nodeList = new GroupedNodeList<string>();
            nodeList.AddFirst("group1", "item");
            nodeList.AddFirst("group1", "item3");
            nodeList.AddFirst("group2", "item4");
            nodeList.AddFirst("group3", "item5"); 
            nodeList.AddFirst("group4", "item7");
            nodeList.AddFirst("group3", "item6");
            nodeList.AddFirst("group4", "item8");
            nodeList.AddFirst("group1", "item2");

            ValidateOrdering(new[] { "item2", "item3", "item", "item4", "item6", "item5", "item8", "item7" }, nodeList);
        }

        [Fact]
        public void When_calling_Joind_with_no_groups_adds_node()
        {
            var nodeList = new GroupedNodeList<string>();
            nodeList.AddFirst("group1", "item");
            nodeList.AddFirst("group1", "item3");
            nodeList.AddFirst("group2", "item4");
            nodeList.AddFirst("group3", "item5");
            nodeList.AddFirst("group4", "item7");
            nodeList.AddFirst("group3", "item6");
            nodeList.AddFirst("group4", "item8");
            nodeList.AddFirst("group1", "item2");

            ValidateOrdering(new[] { "item2", "item3", "item", "item4", "item6", "item5", "item8", "item7" }, nodeList);
        }

        public static void ValidateOrdering(string[] array, GroupedNodeList<string> nodeList)
        {
            var nodeArray = nodeList.ToArray();
            var nodeArrayReverse = nodeList.Reverse();
            array.Should().BeSameAs(nodeArray);
            //array.Reverse().Should().BeSameAs(nodeArrayReverse);
        }
    }
}
