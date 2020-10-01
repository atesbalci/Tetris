using System;
using System.Collections.Generic;
using Tetris.Game.Models;
using Tetris.Game.Views.Blocks;
using UnityEngine;

namespace Tetris.Scripts.Game.Controllers
{
    public class PreviewController : IDisposable
    {
        private readonly BlockPool _pool;
        private readonly Transform _parent;
        private readonly ITetrisGame _game;
        private readonly ICollection<BlockView> _spawnedBlocks;

        public PreviewController(BlockPool pool, Transform parent, ITetrisGame game)
        {
            _pool = pool;
            _parent = parent;
            _game = game;
            _spawnedBlocks = new LinkedList<BlockView>();
            
            _game.RoundStart += OnRoundStart;
            _game.GameStart += OnGameStart;
        }

        public void Dispose()
        {
            _game.RoundStart -= OnRoundStart;
            _game.GameStart -= OnGameStart;
        }

        private void OnRoundStart()
        {
            ClearBlocks();
            foreach (var block in _game.NextFaller.Blocks)
            {
                var blockView = _pool.Spawn();
                blockView.Transform.SetParent(_parent);
                blockView.Bind(block);
                _spawnedBlocks.Add(blockView);
            }
        }

        private void ClearBlocks()
        {
            foreach (var block in _spawnedBlocks)
            {
                _pool.Despawn(block);
            }
            
            _spawnedBlocks.Clear();
        }

        private void OnGameStart()
        {
            ClearBlocks();
        }
    }
}