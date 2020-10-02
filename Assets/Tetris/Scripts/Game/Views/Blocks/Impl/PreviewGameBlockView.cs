using Tetris.Game.Models.Blocks;
using UnityEngine;
using Zenject;

namespace Tetris.Game.Views.Blocks.Impl
{
    [RequireComponent(typeof(Renderer))]
    public class PreviewGameBlockView : GameBlockView
    {
        private IBlockMaterialProvider _blockMaterialProvider;
        private Renderer _renderer;

        [Inject]
        public void Initialize(IBlockMaterialProvider blockMaterialProvider)
        {
            _blockMaterialProvider = blockMaterialProvider;
            _renderer = GetComponent<Renderer>();
        }
        
        public override void Bind(IBlock block)
        {
            _renderer.sharedMaterial = _blockMaterialProvider.GetMaterial(block.Type);
            Transform.localScale = Vector3.one;
            Transform.localPosition = (Vector2) block.Location;
        }
    }
}