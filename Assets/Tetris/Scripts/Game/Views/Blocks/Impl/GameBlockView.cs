using System;
using DG.Tweening;
using Tetris.Game.Models;
using Tetris.Game.Models.Blocks;
using Tetris.Helpers.Tweens;
using UnityEngine;
using Zenject;

namespace Tetris.Game.Views.Blocks.Impl
{
    [RequireComponent(typeof(Renderer))]
    public class GameBlockView : BlockView
    {
        private IBlockMaterialProvider _blockMaterialProvider;
        private BlockPool _pool;
        private ITetrisGame _game;
        private Renderer _renderer;
        private IBlock _block;
        
        // Tweens
        private Tween _movementTween;
        private Tween _clearTween;
        
        [Inject]
        public void Initialize(IBlockMaterialProvider blockMaterialProvider, BlockPool pool, ITetrisGame game)
        {
            _blockMaterialProvider = blockMaterialProvider;
            _pool = pool;
            _game = game;
            _renderer = GetComponent<Renderer>();
            
            _game.LineClear += OnLineClear;
        }

        public override void Bind(IBlock block)
        {
            _block = block;
            _renderer.sharedMaterial = _blockMaterialProvider.GetMaterial(_block.Type);
            _block.LocationChange += OnLocationChange;
            Transform.localScale = Vector3.one;
            OnLocationChange();
            _movementTween.Kill(true);
        }

        private void OnDisable()
        {
            if (_block != null)
            {
                _block.LocationChange -= OnLocationChange;
                _block = null;
            }
            
            _movementTween.KillIfActive();
            _clearTween.KillIfActive();
        }

        private void OnDestroy()
        {
            _game.LineClear -= OnLineClear;
        }

        private void OnLocationChange()
        {
            _movementTween.KillIfActive();
            _movementTween = Transform.DOLocalMove((Vector2) _block.Location, AnimationTimings.BlockShiftDuration);
        }

        private void OnLineClear(int line)
        {
            if (_block != null && _block.Location.y == line)
            {
                _clearTween.KillIfActive();
                _clearTween = DOTween.Sequence()
                    .Append(Transform.DOScale(Vector3.one * 2, AnimationTimings.ExplodeDelay))
                    .Join(_renderer.material.DOFade(0f, AnimationTimings.ExplodeDelay))
                    .OnComplete(() => _pool.Despawn(this));
            }
        }
    }
}