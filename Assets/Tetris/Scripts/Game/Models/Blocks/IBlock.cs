using System;
using UnityEngine;

namespace Tetris.Game.Models.Blocks
{
    public interface IBlock
    {
        event Action LocationChange;
        Vector2Int Location { get; set; }
        BlockType Type { get; }
    }
}