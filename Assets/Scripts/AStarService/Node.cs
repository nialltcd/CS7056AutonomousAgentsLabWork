using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AStarService
{
    public class Node
    {
        public Tile Tile { get; set; }
        public Node ParentNode { get; set; }
        public float G { get; set; }
        public float H { get; set; }
        public float F { get { return G + H; } }

        public Node(Tile tile, Node parentNode, Vector2 locationCoordinates)
        {
            this.Tile = tile;
            this.ParentNode = parentNode;
            if (ParentNode == null)
                this.G = 0;
            else
                this.G = this.ParentNode.G * 
                    (Math.Abs(this.ParentNode.Tile.Coordinates.x - Tile.Coordinates.x)
                    + Math.Abs(this.ParentNode.Tile.Coordinates.y - Tile.Coordinates.y));

            H = Math.Abs(locationCoordinates.x - Tile.Coordinates.x) + Math.Abs(locationCoordinates.y - Tile.Coordinates.y);
            
        }
    }
}
