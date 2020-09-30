using Tetris.Game.Models.Blocks.Impl;
using UnityEngine;
using Zenject;

namespace Tetris.Scripts.Game.Views
{
    [RequireComponent(typeof(Renderer))]
    public class BlockView : MonoBehaviour
    {
        private IBlockMaterialProvider _blockMaterialProvider;
        private Renderer _renderer;
        
        [Inject]
        public void Initialize(IBlockMaterialProvider blockMaterialProvider)
        {
            _blockMaterialProvider = blockMaterialProvider;
            _renderer = GetComponent<Renderer>();
        }

        public void Bind(Block block)
        {
            _renderer.sharedMaterial = _blockMaterialProvider.GetMaterial(block.Type);
        }
    }
}