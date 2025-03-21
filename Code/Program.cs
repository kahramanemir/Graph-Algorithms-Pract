using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra_Prim_DFS
{
    public class DFSGraph
    {
        public int[,] graph;
        public int vertices;

        public DFSGraph(int[,] adjacencyMatrix)
        {
            this.graph = adjacencyMatrix;
            this.vertices = graph.GetLength(0);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            DijkstraStart();
            PrimMSTStart();
            DFSStart();
        }
        

        public static void PrimMSTStart()
        {
            int[,] graph = {
        {0, 2, 0, 6, 0},
        {2, 0, 3, 8, 5},
        {0, 3, 0, 0, 7},
        {6, 8, 0, 0, 9},
        {0, 5, 7, 9, 0}};
            Console.WriteLine("\nMST");
            PrimMinimumSpanningTree(graph);
        }

        public static void DijkstraStart()
        {
            int[,] graph = {
            {0, 4, 0, 0, 0, 0, 0, 8, 0},
            {4, 0, 8, 0, 0, 0, 0, 11, 0},
            {0, 8, 0, 7, 0, 4, 0, 0, 2},
            {0, 0, 7, 0, 9, 14, 0, 0, 0},
            {0, 0, 0, 9, 0, 10, 0, 0, 0},
            {0, 0, 4, 14, 10, 0, 2, 0, 0},
            {0, 0, 0, 0, 0, 2, 0, 1, 6},
            {8, 11, 0, 0, 0, 0, 1, 0, 7},
            {0, 0, 2, 0, 0, 0, 6, 7, 0} };
            Console.WriteLine("Dijkstra");
            DijkstraShortestPath(graph, 0);
        }

        public static void DFSStart()
        {
            int[,] graph = {
            {0, 2, 0, 6, 0},
            {2, 0, 3, 8, 5},
            {0, 3, 0, 0, 7},
            {6, 8, 0, 0, 9},
            {0, 5, 7, 9, 0}
        };

            DFSGraph dfsGraph = new DFSGraph(graph);
            Console.WriteLine("\nDepth-First Traversal starting from vertex 0:");
            DFS(0, dfsGraph.vertices, dfsGraph.graph);
        }
        public static void DijkstraShortestPath(int[,] graph, int source)
        {
            int vertices = graph.GetLength(0);
            int[] distance = new int[vertices];
            bool[] visited = new bool[vertices];

            for (int i = 0; i < vertices; ++i)
            {
                distance[i] = int.MaxValue;
                visited[i] = false;
            }

            distance[source] = 0;

            for (int count = 0; count < vertices - 1; ++count)
            {
                int u = MinDistance(distance, visited);
                visited[u] = true;

                for (int v = 0; v < vertices; ++v)
                {
                    if (!visited[v] && graph[u, v] != 0 && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                    {
                        distance[v] = distance[u] + graph[u, v];
                    }
                }
            }

            Console.WriteLine("Vertex \t\t Distance from Source");
            for (int i = 0; i < vertices; ++i)
            {
                Console.WriteLine($"{i} \t\t {distance[i]}");
            }
        }

        public static int MinDistance(int[] distance, bool[] visited)
        {
            int min = int.MaxValue, minIndex = -1;
            int vertices = distance.Length;

            for (int v = 0; v < vertices; ++v)
            {
                if (visited[v] == false && distance[v] <= min)
                {
                    min = distance[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        public static void PrimMinimumSpanningTree(int[,] graph)
        {
            int vertices = graph.GetLength(0);
            int[] parent = new int[vertices];
            int[] key = new int[vertices];
            bool[] mstSet = new bool[vertices];

            for (int i = 0; i < vertices; ++i)
            {
                key[i] = int.MaxValue;
                mstSet[i] = false;
            }

            key[0] = 0;
            parent[0] = -1;

            for (int count = 0; count < vertices - 1; ++count)
            {
                int u = MinKey(key, mstSet, vertices);
                mstSet[u] = true;

                for (int v = 0; v < vertices; ++v)
                {
                    if (graph[u, v] != 0 && !mstSet[v] && graph[u, v] < key[v])
                    {
                        parent[v] = u;
                        key[v] = graph[u, v];
                    }
                }
            }

            PrintMST(parent, graph);
        }

        public static int MinKey(int[] key, bool[] mstSet, int vertices)
        {
            int min = int.MaxValue, minIndex = -1;

            for (int v = 0; v < vertices; ++v)
            {
                if (mstSet[v] == false && key[v] < min)
                {
                    min = key[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        public static void PrintMST(int[] parent, int[,] graph)
        {
            Console.WriteLine("Edge \tWeight");
            for (int i = 1; i < parent.Length; ++i)
            {
                Console.WriteLine($"{parent[i]} - {i} \t{graph[i, parent[i]]}");
            }
        }

        public static void DFS(int startVertex, int vertices, int[,] graph)
        {
            bool[] visited = new bool[vertices];
            DFSUtil(startVertex, visited, vertices, graph);
            Console.WriteLine();
        }

        public static void DFSUtil(int vertex, bool[] visited, int vertices, int[,] graph)
        {
            visited[vertex] = true;
            Console.Write(vertex + " ");

            for (int i = 0; i < vertices; i++)
            {
                if (graph[vertex, i] != 0 && !visited[i])
                {
                    DFSUtil(i, visited, vertices, graph);
                }
            }
        }
    }
}
