using System;
using Tetris.Game.Models.Blocks;

namespace Tetris.Game.Models
{
    public interface ITetrisGame
    {
        event Action RoundStart;
        event Action<int> LineClear;
        event Action GameOver;
        event Action GameStart;
        
        IBlockSet Faller { get; }
        IBlockSet NextFaller { get; }

        void Begin();

        void LeftShift();
        void RightShift();
        void LeftRotate();
        void RightRotate();
        void QuickFall();
    }
}