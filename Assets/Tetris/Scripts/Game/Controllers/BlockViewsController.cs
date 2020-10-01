using System;
using System.Collections.Generic;
using Tetris.Game.Models;
using Tetris.Game.Views.Blocks;
using UnityEngine;

namespace Tetris.Scripts.Game.Controllers
{
    public class BlockViewsController : IDisposable
    {
        private readonly BlockPool _pool;
        private readonly ITetrisGame _game;
        private readonly Transform _blocksParent;
        private readonly ICollection<BlockView> _blockViews;

        public BlockViewsController(BlockPool pool, ITetrisGame game, Transform blocksParent)
        {
            _pool = pool;
            _game = game;
            _blocksParent = blocksParent;
            _blockViews = new LinkedList<BlockView>();

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
            foreach (var block in _game.Faller.Blocks)
            {
                var blockView = _pool.Spawn();
                blockView.Transform.SetParent(_blocksParent);
                blockView.Bind(block);
                _blockViews.Add(blockView);
            }
        }

        private void OnGameStart()
        {
            foreach (var blockView in _blockViews)
            {
                _pool.Despawn(blockView);
            }
            
            _blockViews.Clear();
        }
    }
}