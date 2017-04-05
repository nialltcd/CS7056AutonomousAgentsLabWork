using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AStarService
{
    public class AStar
    {
        public static Node[,] NodeGrid { get; set; }
        public static Tile[,] TileGrid { get; set; }

        private List<Node> _openList;
        private List<Node> _closedList;

        public AStar()
        {
            _openList = new List<Node>();
            _closedList = new List<Node>();
        }


        // Finds the shortest path with respect to the given tile costs
        public List<Tile> FindPath(Vector2 startPosition, Vector2 targetPosition)
        {
            Tile startTile = TileGrid[(int)Math.Round(startPosition.x,0), (int)Math.Round(startPosition.y,0)];
            Debug.Log("Starting Position: (" + startPosition.x + ", " + startPosition.y + ")");
            Debug.Log("Ending Position: (" + targetPosition.x + ", " + targetPosition.y + ")");
            Debug.Log("Starting Tile Position: (" + startTile.Coordinates.x + ", " + startTile.Coordinates.y + ")");
            Node startNode = new Node(startTile, null, targetPosition);

            _openList.Clear();
            _closedList.Clear();

            _openList.Add(startNode);
            while (_openList.Count > 0)
            {
                Node currentNode = null;
                float minF = float.MaxValue;
                for (int i = 0; i < _openList.Count; ++i)
                {
                    float F = ((Node)_openList[i]).F;
                    if (F < minF)
                    {
                        currentNode = (Node)_openList[i];
                        minF = F;
                    }
                }

                _closedList.Add(currentNode);
                _openList.Remove(currentNode);

                // Target reached?
                if ((int)currentNode.Tile.Coordinates.x == (int)targetPosition.x && (int)currentNode.Tile.Coordinates.y == (int)targetPosition.y)
                {
                    Debug.Log("FOUND ROUTE");
                    return GetPathPositions(currentNode);
                }
                else if (_closedList.Count > 500) // just in case (avoid infinite loop)
                    break;

                // Check adjacent nodes
                Tile adjTile;
                Vector2 adjPosition;

                adjPosition = new Vector2(currentNode.Tile.Coordinates.x - 1, currentNode.Tile.Coordinates.y);
                if (IsGridReachable(adjPosition))
                {
                    adjTile = TileGrid[(int)Math.Round(adjPosition.x,0), (int)Math.Round(adjPosition.y)];
                    CheckAdjacentNode(new Node(adjTile, currentNode, targetPosition));
                }

                adjPosition = new Vector2(currentNode.Tile.Coordinates.x + 1, currentNode.Tile.Coordinates.y);
                if (IsGridReachable(adjPosition))
                {
                    adjTile = TileGrid[(int)Math.Round(adjPosition.x, 0), (int)Math.Round(adjPosition.y)];
                    CheckAdjacentNode(new Node(adjTile, currentNode, targetPosition));
                }

                adjPosition = new Vector2(currentNode.Tile.Coordinates.x, currentNode.Tile.Coordinates.y - 1);
                if (IsGridReachable(adjPosition))
                {
                    adjTile = TileGrid[(int)Math.Round(adjPosition.x, 0), (int)Math.Round(adjPosition.y)];
                    CheckAdjacentNode(new Node(adjTile, currentNode, targetPosition));
                }

                adjPosition = new Vector2(currentNode.Tile.Coordinates.x, currentNode.Tile.Coordinates.y + 1);
                if (IsGridReachable(adjPosition))
                {
                    adjTile = TileGrid[(int)Math.Round(adjPosition.x, 0), (int)Math.Round(adjPosition.y)];
                    CheckAdjacentNode(new Node(adjTile, currentNode, targetPosition));
                }
            }

            return GetPathPositions(_closedList[_closedList.Count - 1]);
        }

        bool IsGridReachable(Vector2 p)
        {
            int x = (int)p.x;
            int y = (int)p.y;

            if (x >= 0 && x < TileGrid.Length && y >= 0 && y < TileGrid.Length)
            {
                //Debug.Log("(" + x + ", " + y + ") - Grid size: "+TileGrid.Length);
                return TileGrid[x, y].IsWalkable;
            }
            return false;
        }

        void CheckAdjacentNode(Node adjNode)
        {
            bool flag = true;

            for (int i = 0; i < _openList.Count; ++i)
            {
                if (_openList[i].Tile.Coordinates.x == adjNode.Tile.Coordinates.x 
                    && _openList[i].Tile.Coordinates.y == adjNode.Tile.Coordinates.y)
                {
                    if (adjNode.G < _openList[i].G)
                    {
                        _openList[i] = adjNode;
                    }

                    flag = false;
                    break;
                }
            }
            foreach (Node node in _closedList)
            {
                if (adjNode.Tile.Coordinates.x == node.Tile.Coordinates.x
                    && adjNode.Tile.Coordinates.y == node.Tile.Coordinates.y)
                {
                    flag = false;
                    break;
                }
            }

            if (flag)
            {
                _openList.Add(adjNode);
            }
        }

        List<Tile> GetPathPositions(Node node)
        {
            List<Tile> positions = new List<Tile>();

            while (node.ParentNode != null)
            {
                positions.Insert(0, node.Tile);
                node = node.ParentNode;
            }
            string s = "";
            foreach (var pos in positions)
                s += "(" +pos.Coordinates.x +", "+ pos.Coordinates.y + ") -> ";
            s += "END";
            Debug.Log(s);
            //positions.Reverse();
            return positions;
        }


        

    }
}
