namespace ABus.DataStructures
{
    public class NodeGroup<T>
    {
        public NodeGroup(string name)
        {
            this.Name = name;
            this.Nodes = new NodeList<T>();
        }

        public NodeList<T> Nodes { get; set; }

        public string Name { get; }
    }
}