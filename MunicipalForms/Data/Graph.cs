using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalForms.Data
{
    public class Edge { public string U, V; public int Weight; public Edge(string u, string v, int w) { U = u; V = v; Weight = w; } }
    public class Graph
    {
        private readonly Dictionary<string, List<(string neighbor, int weight)>> adj = new();
        public void AddNode(string id) { if (!adj.ContainsKey(id)) adj[id] = new List<(string, int)>(); }
        public void AddEdge(string u, string v, int weight = 1, bool undirected = true)
        {
            AddNode(u); AddNode(v);
            adj[u].Add((v, weight));
            if (undirected) adj[v].Add((u, weight));
        }
        public List<string> BFS(string start)
        {
            var result = new List<string>();
            if (!adj.ContainsKey(start)) return result;
            var q = new Queue<string>(); var visited = new HashSet<string>();
            q.Enqueue(start); visited.Add(start);
            while (q.Count > 0)
            {
                var cur = q.Dequeue();
                result.Add(cur);
                foreach (var (nbr, _) in adj[cur])
                {
                    if (!visited.Contains(nbr)) { visited.Add(nbr); q.Enqueue(nbr); }
                }
            }
            return result;
        }
        public List<string> DFS(string start)
        {
            var result = new List<string>();
            if (!adj.ContainsKey(start)) return result;
            var visited = new HashSet<string>();
            DFSRec(start, visited, result);
            return result;
        }
        private void DFSRec(string u, HashSet<string> visited, List<string> result)
        {
            visited.Add(u); result.Add(u);
            foreach (var (v, _) in adj[u])
                if (!visited.Contains(v)) DFSRec(v, visited, result);
        }
        // Prim MST (returns edges)
        public List<Edge> PrimMST(string start)
        {
            var mst = new List<Edge>();
            if (!adj.ContainsKey(start)) return mst;
            var visited = new HashSet<string> { start };
            var pq = new SortedSet<(int weight, string u, string v)>();
            foreach (var (v, w) in adj[start]) pq.Add((w, start, v));
            while (pq.Count > 0)
            {
                var top = pq.First(); pq.Remove(top);
                var wgt = top.weight; var u = top.u; var vtx = top.v;
                if (visited.Contains(vtx)) continue;
                visited.Add(vtx);
                mst.Add(new Edge(u, vtx, wgt));
                foreach (var (nbr, w2) in adj[vtx])
                {
                    if (!visited.Contains(nbr)) pq.Add((w2, vtx, nbr));
                }
            }
            return mst;
        }
        public IEnumerable<string> Nodes() => adj.Keys;

        // New: Kruskal MST
        public List<Edge> KruskalMST()
        {
            // Collect unique edges (since undirected, avoid duplicates)
            var edges = new List<Edge>();
            var seen = new HashSet<(string, string)>();
            foreach (var u in adj.Keys)
            {
                foreach (var (v, w) in adj[u])
                {
                    var key = string.Compare(u, v) < 0 ? (u, v) : (v, u);
                    if (!seen.Contains(key))
                    {
                        seen.Add(key);
                        edges.Add(new Edge(u, v, w));
                    }
                }
            }

            // Sort edges by weight
            edges.Sort((a, b) => a.Weight.CompareTo(b.Weight));

            // Union-Find
            var uf = new UnionFind(adj.Keys.ToList());

            var mst = new List<Edge>();
            foreach (var edge in edges)
            {
                if (uf.Union(edge.U, edge.V))
                {
                    mst.Add(edge);
                }
            }

            return mst;
        }
    }

    // New: UnionFind class for strings
    public class UnionFind
    {
        private Dictionary<string, string> parent;
        private Dictionary<string, int> rank;

        public UnionFind(List<string> nodes)
        {
            parent = new Dictionary<string, string>();
            rank = new Dictionary<string, int>();
            foreach (var node in nodes)
            {
                parent[node] = node;
                rank[node] = 0;
            }
        }

        public string Find(string p)
        {
            if (parent[p] != p)
                parent[p] = Find(parent[p]); // Path compression
            return parent[p];
        }

        public bool Union(string p, string q)
        {
            string rootP = Find(p);
            string rootQ = Find(q);
            if (rootP == rootQ)
                return false; // Cycle

            // Union by rank
            if (rank[rootP] < rank[rootQ])
                parent[rootP] = rootQ;
            else if (rank[rootP] > rank[rootQ])
                parent[rootQ] = rootP;
            else
            {
                parent[rootQ] = rootP;
                rank[rootP]++;
            }
            return true;
        }
    }
}