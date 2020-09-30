using System.Collections.Generic;
using UnityEngine;

namespace Tetris.Game.Models.Blocks
{
    public interface IBlockSetProvider
    {
        IEnumerable<Vector2Int> GetBlockSet(BlockType type);
    }
}