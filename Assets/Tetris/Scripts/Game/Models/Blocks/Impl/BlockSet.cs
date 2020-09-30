using System.Collections.Generic;
using UnityEngine;

namespace Tetris.Game.Models.Blocks.Impl
{
    public class BlockSet : IBlockSet
    {
        private Vector2Int _centerLocation;
        
        public ICollection<IBlock> Blocks { get; }

        public BlockSet(Vector2Int centerLocation, BlockType type, IEnumerable<Vector2Int> blockLocations)
        {
            _centerLocation = centerLocation;
            Blocks = new LinkedList<IBlock>();
            foreach (var location in blockLocations)
            {
                Blocks.Add(new Block(type, centerLocation + location));
            }
        }

        public Vector2Int CenterLocation
        {
            get => _centerLocation;
            set
            {
                var diff = value - _centerLocation;
                if (diff != Vector2Int.zero)
                {
                    _centerLocation = value;
                    foreach (var block in Blocks)
                    {
                        block.Location += diff;
                    }
                }
            }
        }

        public void Shift(Vector2Int shift)
        {
            CenterLocation += shift;
        }

        public void Rotate(bool clockwise)
        {
            var angle = clockwise ? 90f : -90f;
            var sin = Mathf.Sin(angle);
            var cos = Mathf.Cos(angle);
            
            foreach (var block in Blocks)
            {
                Vector2 normalizedLocation = block.Location - CenterLocation;
                var newPointNormalized = new Vector2(normalizedLocation.x * cos - normalizedLocation.y * sin,
                    normalizedLocation.y * cos + normalizedLocation.x * sin);
                block.Location = CenterLocation + 
                    new Vector2Int(Mathf.RoundToInt(newPointNormalized.x), Mathf.RoundToInt(newPointNormalized.y));
            }
        }
    }
}