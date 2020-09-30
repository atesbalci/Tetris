using System.Collections.Generic;
using UnityEngine;

namespace Tetris.Game.Models.Blocks
{
    public interface IBlockSet
    {
        ICollection<IBlock> Blocks { get; }
        Vector2Int CenterLocation { get; set; }
        void Shift(Vector2Int shift);
        void Rotate(bool clockwise);
    }
}