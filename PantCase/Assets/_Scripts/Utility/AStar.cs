using System;
using System.Collections.Generic;
using UnityEngine;

class Node
{
    public int[] Position { get; }
    public Node Parent { get; set; }
    public int G { get; set; } // Cost from start to current node
    public int H { get; set; } // Heuristic (estimated cost from current node to goal)
    public int F => G + H; // Total cost (g + h)

    public Node(int[] position, Node parent = null)
    {
        Position = position;
        Parent = parent;
        G = 0;
        H = 0;
    }
}

class AStar
{
    public static List<int[]> FindPath(Cell[,] maze, Vector2Int start, Vector2Int goal)
    {
        var startPos = new int[] { start.x, start.y };
        var goalPos = new int[] { goal.x, goal.y };

        Node startNode = new Node(startPos);
        Node goalNode = new Node(goalPos);

        List<Node> openList = new List<Node>();
        HashSet<int[]> closedSet = new HashSet<int[]>(new PositionComparer());

        openList.Add(startNode);


        while (openList.Count > 0)
        {
            Node currentNode = GetLowestF(openList);
            openList.Remove(currentNode);
            closedSet.Add(currentNode.Position);

            if (ArrayEquals(currentNode.Position, goalNode.Position))
            {
                return ReconstructPath(currentNode);
            }

            List<int[]> neighbors = GetNeighbors(currentNode.Position, maze);

            foreach (var neighborPos in neighbors)
            {
                if (!IsPositionValid(neighborPos, maze) || closedSet.Contains(neighborPos))
                {
                    continue;
                }

                int tentativeG = currentNode.G + 1;
                Node neighborNode = openList.Find(node => ArrayEquals(node.Position, neighborPos));

                if (neighborNode == null || tentativeG < neighborNode.G)
                {
                    if (neighborNode == null)
                    {
                        neighborNode = new Node(neighborPos, currentNode);
                        openList.Add(neighborNode);
                    }
                    else
                    {
                        neighborNode.Parent = currentNode;
                    }

                    neighborNode.G = tentativeG;
                    neighborNode.H = Heuristic(neighborNode.Position, goalNode.Position);
                }
            }
        }


        return null; // No path found
    }

    static Node GetLowestF(List<Node> nodes)
    {
        Node lowest = nodes[0];
        foreach (var node in nodes)
        {
            if (node.F < lowest.F)
            {
                lowest = node;
            }
        }

        return lowest;
    }

    static List<int[]> ReconstructPath(Node node)
    {
        List<int[]> path = new List<int[]>();
        while (node != null)
        {
            path.Insert(0, node.Position);
            node = node.Parent;
        }

        return path;
    }

    static List<int[]> GetNeighbors(int[] position, Cell[,] maze)
    {
        List<int[]> neighbors = new List<int[]>
        {
            new int[] { position[0] - 1, position[1] }, // Up
            new int[] { position[0] + 1, position[1] }, // Down
            new int[] { position[0], position[1] - 1 }, // Left
            new int[] { position[0], position[1] + 1 }, // Right
            new int[] { position[0] - 1, position[1] - 1 }, // Up Left
            new int[] { position[0] - 1, position[1] + 1 }, // Up Right
            new int[] { position[0] + 1, position[1] - 1 }, // Down Left
            new int[] { position[0] + 1, position[1] + 1 } // Down Right
        };

        return neighbors;
    }

    static bool IsPositionValid(int[] position, Cell[,] maze)
    {
        int rows = maze.GetLength(0);
        int cols = maze.GetLength(1);
        return position[0] >= 0 && position[0] < rows && position[1] >= 0 && position[1] < cols && maze[position[0], position[1]].CanMove();
    }

    static int Heuristic(int[] a, int[] b)
    {
        return Math.Abs(a[0] - b[0]) + Math.Abs(a[1] - b[1]); // Manhattan distance as a heuristic
    }

    static bool ArrayEquals(int[] a, int[] b)
    {
        return a[0] == b[0] && a[1] == b[1];
    }
}

class PositionComparer : IEqualityComparer<int[]>
{
    public bool Equals(int[] x, int[] y)
    {
        return x[0] == y[0] && x[1] == y[1];
    }

    public int GetHashCode(int[] obj)
    {
        return obj[0].GetHashCode() ^ obj[1].GetHashCode();
    }
}