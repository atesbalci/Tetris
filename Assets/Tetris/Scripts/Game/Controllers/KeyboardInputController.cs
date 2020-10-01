using Tetris.Game.Models;
using UnityEngine;
using Zenject;

namespace Tetris.Scripts.Game.Controllers
{
    public class KeyboardInputController : ITickable
    {
        private readonly ITetrisGame _game;

        public KeyboardInputController(ITetrisGame game)
        {
            _game = game;
        }
        
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _game.LeftShift();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _game.RightShift();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _game.QuickFall();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                _game.LeftRotate();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _game.RightRotate();
            }
        }
    }
}