using System.Collections;
using Tetris.Game.Models;
using Tetris.Game.Models.Blocks;

namespace Tetris.Scripts.Game.Controllers
{
    public class GameController : ITetrisGame
    {
        private readonly IBlockSetProvider _blockSetProvider;
        public IBlockSet Faller { get; private set; }
        public IBlockSet NextFaller { get; private set; }

        public GameController(IBlockSetProvider blockSetProvider)
        {
            _blockSetProvider = blockSetProvider;
        }

        private IEnumerator GameRoutine()
        {
            yield return null;
        }
        
        public void LeftShift()
        {
            throw new System.NotImplementedException();
        }

        public void RightShift()
        {
            throw new System.NotImplementedException();
        }

        public void LeftRotate()
        {
            throw new System.NotImplementedException();
        }

        public void RightRotate()
        {
            throw new System.NotImplementedException();
        }

        public void QuickFall()
        {
            throw new System.NotImplementedException();
        }
    }
}