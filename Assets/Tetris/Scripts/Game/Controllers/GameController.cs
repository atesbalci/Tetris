using System;
using System.Collections;
using System.Collections.Generic;
using Tetris.Game.Models;
using Tetris.Game.Models.Blocks;
using Tetris.Game.Models.Blocks.Impl;
using Tetris.Game.Views;
using Tetris.Helpers.Collections;
using Tetris.Helpers.Coroutines;
using UnityEngine;

namespace Tetris.Scripts.Game.Controllers
{
    public class GameController : ITetrisGame
    {
        public event Action RoundStart;
        public event Action<int> LineClear;
        public event Action GameOver;
        public event Action GameStart;

        private readonly IBlockSetProvider _blockSetProvider;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IDictionary<Vector2Int, IBlock> _settledBlocks;

        private State _state;
        
        public IBlockSet Faller { get; private set; }
        public IBlockSet NextFaller { get; private set; }

        public GameController(IBlockSetProvider blockSetProvider, ICoroutineRunner coroutineRunner)
        {
            _blockSetProvider = blockSetProvider;
            _coroutineRunner = coroutineRunner;
            _settledBlocks = new Dictionary<Vector2Int, IBlock>();
            
            Begin();
        }

        private void SwitchToNextFaller()
        {
            if (NextFaller == null) // For first run
            {
                var randomType = GameRules.GetRandomBlockType();
                NextFaller = new BlockSet(Vector2Int.zero, randomType, _blockSetProvider.GetBlockSet(randomType));
            }

            Faller = NextFaller;
            Faller.CenterLocation = new Vector2Int(GameRules.Width / 2, GameRules.Height - 1);
            var newRandomType = GameRules.GetRandomBlockType();
            NextFaller = new BlockSet(Vector2Int.zero, newRandomType, _blockSetProvider.GetBlockSet(newRandomType));
        }

        public void Begin()
        {
            GameStart?.Invoke();
            _coroutineRunner.RunCoroutine(GameRoutine());
        }

        private IEnumerator GameRoutine()
        {
            _settledBlocks.Clear();
            yield return null;
            while (true)
            {
                SwitchToNextFaller();
                RoundStart?.Invoke();
                _state = State.Default;
                while (true)
                {
                    var fallInterval = _state == State.Drop ? GameRules.DropFallInterval : GameRules.FallInterval;
                    yield return new WaitForSeconds(fallInterval);
                    if (CheckFallerDirection(Vector2Int.down))
                    {
                        Faller.Shift(Vector2Int.down);
                    }
                    else
                    {
                        break;
                    }
                }

                _state = State.Fallen;
                foreach (var block in Faller.Blocks)
                {
                    _settledBlocks[block.Location] = block;
                }

                var delay = PerformLineOperations() ? AnimationTimings.ExplodeDelay : AnimationTimings.SettleDelay;

                if (IsGameOver())
                {
                    break;
                }
                
                yield return new WaitForSeconds(delay);
            }
            
            GameOver?.Invoke();
        }

        private bool PerformLineOperations()
        {
            ICollection<int> fullLines = new LinkedList<int>();
            for (int y = 0; y < GameRules.Height; y++)
            {
                int x;
                for (x = 0; x < GameRules.Width; x++)
                {
                    if (!_settledBlocks.ContainsKey(new Vector2Int(x, y)))
                    {
                        break;
                    }
                }

                if (x == GameRules.Width)
                {
                    fullLines.Add(y);
                }
            }
            
            if(fullLines.Count == 0) return false;

            // Clear stage...
            foreach (var y in fullLines)
            {
                for (int x = 0; x < GameRules.Width; x++)
                {
                    _settledBlocks.Remove(new Vector2Int(x, y));
                    LineClear?.Invoke(y);
                }
            }

            // Shift stage...
            for (int y = 0; y < GameRules.Height; y++)
            {
                var currentY = y;
                var freeSlotsAmountBelow = fullLines.Count(i => i < currentY);
                for (int x = 0; x < GameRules.Width; x++)
                {
                    var index = new Vector2Int(x, y);
                    if (_settledBlocks.TryGetValue(index, out var block))
                    {
                        _settledBlocks.Remove(index);
                        var newIndex = index + Vector2Int.down * freeSlotsAmountBelow;
                        block.Location = newIndex;
                        _settledBlocks[newIndex] = block;
                    }
                }
            }

            return true;
        }

        private bool IsGameOver()
        {
            var y = GameRules.Height - 1;
            for (int x = 0; x < GameRules.Width; x++)
            {
                if (_settledBlocks.ContainsKey(new Vector2Int(x, y)))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns true if free.
        /// </summary>
        private bool CheckLocation(Vector2Int blockLocation)
        {
            return blockLocation.x >= 0 && blockLocation.x < GameRules.Width && blockLocation.y >= 0 &&
                   blockLocation.y < GameRules.Height && !_settledBlocks.ContainsKey(blockLocation);
        }

        /// <summary>
        /// Returns true if free.
        /// </summary>
        private bool CheckFallerDirection(Vector2Int direction)
        {
            return Faller.Blocks.All(block => CheckLocation(block.Location + direction));
        }

        /// <summary>
        /// Returns true if free.
        /// </summary>
        private bool CheckFallerLocation() => CheckFallerDirection(Vector2Int.zero);
        
        public void LeftShift()
        {
            TryShift(Vector2Int.left);
        }

        public void RightShift()
        {
            TryShift(Vector2Int.right);
        }

        private void TryShift(Vector2Int direction)
        {
            if (_state == State.Fallen) return;
            if (CheckFallerDirection(direction))
            {
                Faller.Shift(direction);
            }
        }

        public void LeftRotate()
        {
            TryRotate(false);
        }

        public void RightRotate()
        {
            TryRotate(true);
        }

        private void TryRotate(bool clockwise)
        {
            if (_state == State.Fallen) return;
            Faller.Rotate(clockwise);
            if (!CheckFallerLocation())
            {
                // Reverts rotation
                Faller.Rotate(!clockwise);
            }
        }

        public void QuickFall()
        {
            if (_state == State.Default)
            {
                _state = State.Drop;
            }
        }
        
        private enum State
        {
            Default,
            Drop,
            Fallen
        }
    }
}