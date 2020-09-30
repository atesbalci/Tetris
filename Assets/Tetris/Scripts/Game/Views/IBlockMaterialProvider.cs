using Tetris.Game.Models.Blocks;
using UnityEngine;

namespace Tetris.Scripts.Game.Views
{
    public interface IBlockMaterialProvider
    {
        Material GetMaterial(BlockType type);
    }
}