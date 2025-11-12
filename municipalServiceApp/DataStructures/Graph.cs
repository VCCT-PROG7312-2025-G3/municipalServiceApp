//NEEDED FOR PART 3 OF POE
using System;
using System.Collections.Generic;
using System.Linq;

namespace municipalServiceApp.DataStructures
{
    public class Edge
    {
        public int From, To;
        public double Weight;
        public Edge(int f, int t, double w = 1) { From = f; To = t; Weight = w; }
    }

    public class Graph
    {
        private readonly Dictionary<int, List<Edge>> _adj = new();

        public void AddVertex(int v) { if (!_adj.ContainsKey(v)) _adj[v] = new List<Edge>(); }

        public void AddEdge(int u, int v, double weight = 1, bool undirected = true)
        {
            AddVertex(u); AddVertex(v);
            _adj[u].Add(new Edge(u, v, weight));
            if (undirected) _adj[v].Add(new Edge(v, u, weight));
        }

        public IEnumerable<int> BFS(int start)
        {
            var visited = new HashSet<int>();
            var q = new Queue<int>();
            q.Enqueue(start); visited.Add(start);
            while (q.Count > 0)
            {
                var cur = q.Dequeue();
                yield return cur;
                foreach (var e in _adj[cur])
                {
                    if (!visited.Contains(e.To))
                    {
                        visited.Add(e.To);
                        q.Enqueue(e.To);
                    }
                }
            }
        }

        public IEnumerable<int> DFS(int start)
        {
            var visited = new HashSet<int>();
            var stack = new Stack<int>();
            stack.Push(start);
            while (stack.Count > 0)
            {
                var cur = stack.Pop();
                if (visited.Contains(cur)) continue;
                visited.Add(cur);
                yield return cur;
                foreach (var e in _adj[cur])
                {
                    if (!visited.Contains(e.To)) stack.Push(e.To);
                }
            }
        }

        public List<Edge> PrimMST()
        {
            var result = new List<Edge>();
            if (_adj.Count == 0) return result;

            var start = _adj.Keys.First();
            var visited = new HashSet<int> { start };
            var pq = new MinHeap<Edge>((a, b) => a.Weight.CompareTo(b.Weight));
            foreach (var e in _adj[start]) pq.Push(e);

            while (pq.Count > 0 && visited.Count < _adj.Count)
            {
                var e = pq.Pop();
                if (visited.Contains(e.To)) continue;
                visited.Add(e.To);
                result.Add(e);
                foreach (var ne in _adj[e.To])
                    if (!visited.Contains(ne.To)) pq.Push(ne);
            }

            return result;
        }

        public Dictionary<int, List<Edge>> RequestGraph { get; private set; }
    }
}
