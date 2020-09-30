using System;
using UnityEngine;

namespace Tetris.Game.Models.Blocks.Impl
{
    public class Block : IBlock
    {
        public event Action LocationChange;
        private Vector2Int _location;

        public Vector2Int Location
        {
            get => _location;
            set
            {
                if (_location != value)
                {
                    _location = value;
                    LocationChange?.Invoke();
                }
            }
        }

        public BlockType Type { get; }

        public Block(BlockType type, Vector2Int location)
        {
            Location = location;
            Type = type;
        }
    }
}