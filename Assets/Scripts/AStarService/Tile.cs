using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AStarService
{
    public class Tile
    {
        public bool IsWalkable { get; set; }
        public Vector2 Coordinates { get; set; }

        public Tile(bool isWalkable, Vector2 coordinates)
        {
            this.IsWalkable = isWalkable;
            this.Coordinates = coordinates;
        }
    }
}
