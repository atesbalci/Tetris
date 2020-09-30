using System;
using Tetris.Game.Models.Blocks;

namespace Tetris.Game.Models
{
    public interface ITetrisGame
    {
        IBlockSet Faller { get; }
        IBlockSet NextFaller { get; }

        void LeftShift();
        void RightShift();
        void LeftRotate();
        void RightRotate();
        void QuickFall();
    }
}