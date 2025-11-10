namespace MunicipalForms.DataStructures
{
    public class Graph
    {
        private readonly Dictionary<string, List<string>> adjacencyList = new();

        public void AddEdge(string from, string to)
        {
            if (!adjacencyList.ContainsKey(from))
                adjacencyList[from] = new List<string>();

            adjacencyList[from].Add(to);
        }

        public List<string> GetConnections(string node)
        {
            return adjacencyList.ContainsKey(node) ? adjacencyList[node] : new List<string>();
        }

        // BFS traversal
        public List<string> BreadthFirstTraversal(string start)
        {
            List<string> visited = new();
            Queue<string> queue = new();

            visited.Add(start);
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();

                foreach (var neighbor in GetConnections(current))
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return visited;
        }
    }
}
