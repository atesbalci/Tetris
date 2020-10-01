using Tetris.Game.Models.Blocks;
using UnityEngine;

namespace Tetris.Game.Views.Blocks
{
    public abstract class BlockView : MonoBehaviour
    {
        private Transform _transform;
        
        public Transform Transform => _transform ? _transform : (_transform = transform);

        public abstract void Bind(IBlock block);
    }
}