using System;
using System.Collections.Generic;

namespace TeamJustFour.MoveOneStep.Algorithm
{
    public class Astar
    {
        private Node[,] m_Grid;
        private List<Node> m_OpenList;
        private HashSet<Node> m_ClosedList;
        private int m_GridWidth;
        private int m_GridHeight;

        public Astar(int width, int height)
        {
            m_GridWidth = width;
            m_GridHeight = height;
            m_Grid = new Node[m_GridWidth, m_GridHeight];
            m_OpenList = new List<Node>();
            m_ClosedList = new HashSet<Node>();

            // 그리드 초기화
            for (int x = 0; x < m_GridWidth; x++)
            {
                for (int y = 0; y < m_GridHeight; y++)
                {
                    m_Grid[x, y] = new Node(x, y);
                }
            }
        }

        public List<Node> FindPath(int startX, int startY, int endX, int endY)
        {
            Node startNode = m_Grid[startX, startY];
            Node endNode = m_Grid[endX, endY];

            m_OpenList.Add(startNode);

            while (m_OpenList.Count > 0)
            {
                Node currentNode = m_OpenList[0];
                for (int i = 1; i < m_OpenList.Count; i++)
                {
                    if (m_OpenList[i].FCost < currentNode.FCost || m_OpenList[i].FCost == currentNode.FCost && m_OpenList[i].HCost < currentNode.HCost)
                    {
                        currentNode = m_OpenList[i];
                    }
                }

                m_OpenList.Remove(currentNode);
                m_ClosedList.Add(currentNode);

                if (currentNode == endNode)
                {
                    return RetracePath(startNode, endNode);
                }

                foreach (Node neighbour in GetNeighbours(currentNode))
                {
                    if (neighbour.IsWall || m_ClosedList.Contains(neighbour))
                    {
                        continue;
                    }

                    float newCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour);
                    if (newCostToNeighbour < neighbour.GCost || !m_OpenList.Contains(neighbour))
                    {
                        neighbour.GCost = newCostToNeighbour;
                        neighbour.HCost = GetDistance(neighbour, endNode);
                        neighbour.Parent = currentNode;

                        if (!m_OpenList.Contains(neighbour))
                        {
                            m_OpenList.Add(neighbour);
                        }
                    }
                }
            }

            return null; // 경로를 찾을 수 없는 경우
        }

        public void SetWall(int x, int y)
        {
            if (x >= 0 && x < m_GridWidth && y >= 0 && y < m_GridHeight)
            {
                m_Grid[x, y].IsWall = true;
            }
        }

        private List<Node> RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Reverse();
            return path;
        }

        private List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new();

            // 상, 하, 좌, 우 방향의 이웃만 고려
            int[] dx = new int[] { 0, 0, -1, 1 };
            int[] dy = new int[] { -1, 1, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                int checkX = node.X + dx[i];
                int checkY = node.Y + dy[i];

                if (checkX >= 0 && checkX < m_GridWidth && checkY >= 0 && checkY < m_GridHeight)
                {
                    neighbours.Add(m_Grid[checkX, checkY]);
                }
            }

            return neighbours;
        }

        private float GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = Math.Abs(nodeA.X - nodeB.X);
            int dstY = Math.Abs(nodeA.Y - nodeB.Y);
            return dstX + dstY;
        }
    }

    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsWall { get; set; }
        public float GCost { get; set; }
        public float HCost { get; set; }
        public float FCost { get { return GCost + HCost; } }
        public Node Parent { get; set; }

        public Node(int x, int y)
        {
            X = x;
            Y = y;
            IsWall = false;
        }
    }
}
